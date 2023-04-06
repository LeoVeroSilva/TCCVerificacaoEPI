using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace TCCVerificacaoEPI
{
    public partial class MainForm : Form
    {
        private MachineState programState;
        private AmazonRekognitionClient client;
        public MainForm()
        {
            InitializeComponent();
            programState = new MachineState();
            client = CreateAWSRekognitionClient();
            PrintConsole("Program Initialized");
        }

        private static AmazonRekognitionClient CreateAWSRekognitionClient()
        {
            AWSConfigs.AWSProfileName = "Developer";
            AmazonRekognitionClient client = new AmazonRekognitionClient();
            return client;
        }

        private async Task<DetectProtectiveEquipmentResponse> DetectPPE(AmazonRekognitionClient client, Amazon.Rekognition.Model.Image image)
        {
            List<string> RequiredEquipmentTypes = new List<string>();
            if (cbHelmet.Checked) RequiredEquipmentTypes.Add("HEAD_COVER");
            if (cbMask.Checked) RequiredEquipmentTypes.Add("FACE_COVER");
            if (cbGloves.Checked) RequiredEquipmentTypes.Add("HAND_COVER");
            DetectProtectiveEquipmentRequest DetectPPERequest = new DetectProtectiveEquipmentRequest()
            {
                SummarizationAttributes = new ProtectiveEquipmentSummarizationAttributes
                {
                    MinConfidence = float.Parse(tbMinConfLvl.Text),
                    RequiredEquipmentTypes = RequiredEquipmentTypes,
                },
                Image = image
            };
            return client.DetectProtectiveEquipment(DetectPPERequest); 
        }

        private static Amazon.Rekognition.Model.Image GetImageFromFile(string file)
        {
            Amazon.Rekognition.Model.Image image = new Amazon.Rekognition.Model.Image();
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                byte[] data = null;
                data = new byte[fs.Length];
                fs.Read(data, 0, (int)fs.Length);
                image.Bytes = new MemoryStream(data);
            }
            return image;
        }

        private void PrintRawReturn(DetectProtectiveEquipmentResponse DetectPPEResponse)
        {
            string returnmsg = "";
            foreach (ProtectiveEquipmentPerson person in DetectPPEResponse.Persons)
            {
                returnmsg += string.Format("Person: {0} | Confidence: {1}\n", person.Id, person.Confidence);
                foreach (ProtectiveEquipmentBodyPart bodyPart in person.BodyParts)
                { 
                    returnmsg += string.Format(" -> BodyPart: {0} | Confidence: {1} \n", bodyPart.Name, bodyPart.Confidence);
                    if(bodyPart.EquipmentDetections.Count > 0)
                    {
                        foreach (EquipmentDetection equipmentDetection in bodyPart.EquipmentDetections)
                            returnmsg += string.Format(" - - > {0} | Confidence: {1} \n", equipmentDetection.CoversBodyPart.Value);
                    }
                    else returnmsg += string.Format(" - - > Not Convered\n");

                }
            }
            rtResponse.Text = returnmsg;
        }

        private void PrintConsole(string msg)
        {
            rtConsole.Text = msg;
        }

        private void rb_camera_CheckedChanged(object sender, EventArgs e)
        {
            tbFilePath.Enabled = false;
            tbFilePath.Visible = false;
            tbFilePath.Text = null;
            bBrowse.Enabled = false;
            bBrowse.Visible = false;

            cbCOMPort.Enabled = true;
            cbCOMPort.Visible = true;
            bConnectDisconnect.Enabled = true;
            bConnectDisconnect.Visible = true;

            bImageAction.Text = "Capture";
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void bBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbFilePath.Text = openFileDialog.FileName;
            }
        }

        private void rbServiceResponse_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void rbFile_CheckedChanged(object sender, EventArgs e)
        {
            cbCOMPort.Enabled = false;
            cbCOMPort.Visible = false;
            bConnectDisconnect.Enabled = false;
            bConnectDisconnect.Visible = false;

            tbFilePath.Enabled = true;
            tbFilePath.Visible = true;
            bBrowse.Enabled = true;
            bBrowse.Visible = true;

            bImageAction.Text = "Load";
        }

        private void bConnectDisconnect_Click(object sender, EventArgs e)
        {
            if (bConnectDisconnect.Text == "Connect") 
            {
                // try to connect to COM Port
                // If successfull, bring video to PictureBox and change label
                // If fails, raise error message
            }
            else
            {
                // Disconnect from COM Port
            }
        }

        private void bImageAction_Click(object sender, EventArgs e)
        {
            switch (bImageAction.Text) 
            {
                case "Load":
                    pbImage.Image = System.Drawing.Image.FromFile(tbFilePath.Text);
                    PrintConsole("Image loaded");
                    break;
                case "Capture":
                    break;
                case "Reset":
                    break;
            }
        }

        private async void bSend_Click(object sender, EventArgs e)
        {
            PrintConsole("Response Sent");
            Amazon.Rekognition.Model.Image image = GetImageFromFile(tbFilePath.Text);
            DetectProtectiveEquipmentResponse DetectPPEResponse = await DetectPPE(client, image);
            PrintRawReturn(DetectPPEResponse);
            PrintConsole("Response Received");
        }

        private void tbMinConfLvl_Leave(object sender, EventArgs e)
        {
            if (float.Parse(tbMinConfLvl.Text) < 50) tbMinConfLvl.Text = "50";
            else if (float.Parse(tbMinConfLvl.Text) > 100) tbMinConfLvl.Text = "100";
        }
    }
    class MachineState
    {

        public enum enumState
        {
            Start
        }

        public enum enumImageSourceType
        {
            File,
            Camera
        }

        public enum enumResponseDisplayType
        {
            Raw,
            Resume
        }

        public int state { get; set; }
        public enumImageSourceType ImageSourceType { get; set; }
        public enumResponseDisplayType ResponseDisplayType { get; set; }
    }

}
