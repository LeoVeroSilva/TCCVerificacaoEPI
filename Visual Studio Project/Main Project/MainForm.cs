using Amazon.Rekognition.Model;
using Amazon.Rekognition;
using Amazon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;



namespace TCCVerificacaoEPI
{
    public partial class MainForm : Form
    {
        private MachineState programState;
        public MainForm()
        {
            InitializeComponent();
            programState = new MachineState();
        }

        private static async Task<DetectProtectiveEquipmentResponse> DetectPPE(AmazonRekognitionClient client, Amazon.Rekognition.Model.Image image)
        {
            DetectProtectiveEquipmentRequest DetectPPERequest = new DetectProtectiveEquipmentRequest()
            {
                SummarizationAttributes = new ProtectiveEquipmentSummarizationAttributes
                {
                    MinConfidence = 0,
                    RequiredEquipmentTypes = { "FACE_COVER", "HAND_COVER", "HEAD_COVER" },
                },
                Image = image
            };
            return client.DetectProtectiveEquipment(DetectPPERequest);
        }

        private static AmazonRekognitionClient CreateAWSRekognitionClient()
        {
            AWSConfigs.AWSProfileName = "Developer";
            AmazonRekognitionClient client = new AmazonRekognitionClient();
            return client;
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

        private static void PrintReturn(DetectProtectiveEquipmentResponse DetectPPEResponse)
        {
            foreach (ProtectiveEquipmentPerson Persons in DetectPPEResponse.Persons)
                foreach (ProtectiveEquipmentBodyPart PPE in Persons.BodyParts)
                    Console.WriteLine("Person {0} - BodyPart {1} : Confidence: {2}", Persons.Id, PPE.Name, PPE.Confidence);
        }

        private static void PrintConsole(string msg)
        {
            //print in console
        }

        private void rb_camera_CheckedChanged(object sender, EventArgs e)
        {

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
            
        }

        private void rbFile_Click(object sender, EventArgs e)
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

        private void rbCamera_Click(object sender, EventArgs e)
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
                    try
                    {
                        Amazon.Rekognition.Model.Image image = GetImageFromFile(tbFilePath.Text);
                        rtConsole.Text = "Image loaded";
                    }
                    catch
                    {
                        rtConsole.Text = "Not possible to load image";
                    }
                    break;
                case "Capture":
                    break;
                case "Reset":
                    break;
            }
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
