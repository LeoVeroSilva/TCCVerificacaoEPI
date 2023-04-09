using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace TCCVerificacaoEPI
{
    public partial class MainForm : Form
    {
        private AmazonRekognitionClient client;

        public MainForm()
        {
            InitializeComponent();
            MyInitilize();
        }

        #region General Methods
        private void MyInitilize()
        {
            PrintConsole("Initializing Program");
            client = CreateAWSRekognitionClient();
            PrintConsole("Program Initialized");
        }

        private AmazonRekognitionClient CreateAWSRekognitionClient()
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

        private Amazon.Rekognition.Model.Image GetImageFromFile(string file)
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

        private void ProcessReturn(DetectProtectiveEquipmentResponse DetectPPEResponse)
        {
            PrintRawReturn(DetectPPEResponse);
            printReturnValidatin(DetectPPEResponse);
            DrawReturnBoudingBox(DetectPPEResponse);
        }

        private void MyDrawRectangle(System.Drawing.Pen pen, System.Drawing.Graphics graphics, BoundingBox boundingBox, Size imageSize)
        {
            RealBoudingBox realBoundingBox = new RealBoudingBox(imageSize, boundingBox);
            graphics.DrawRectangle(pen, realBoundingBox.left, realBoundingBox.top, realBoundingBox.width, realBoundingBox.height);
        }

        #endregion

        #region Methods for Print Text/Images
        private void printReturnValidatin(DetectProtectiveEquipmentResponse DetectPPEResponse) 
        { 

        }

        private void DrawReturnBoudingBox(DetectProtectiveEquipmentResponse DetectPPEResponse) 
        {
            Bitmap bitmap = new Bitmap(pbImage.Image);
            Graphics graphics = Graphics.FromImage(bitmap);

            Pen personPen = new Pen(Color.Green, 3);
            Pen coveredBodyPartPen = new Pen(Color.Yellow, 3);
            Pen equipamentPen = new Pen(Color.Red, 3);
            
            foreach (ProtectiveEquipmentPerson person in DetectPPEResponse.Persons)
            {
                MyDrawRectangle(personPen, graphics, person.BoundingBox, bitmap.Size);
                foreach (ProtectiveEquipmentBodyPart bodyPart in person.BodyParts)
                {
                    if (bodyPart.EquipmentDetections.Count > 0)
                    {
                        foreach (EquipmentDetection equipmentDetection in bodyPart.EquipmentDetections)
                        {
                            // Body Part Equipament
                            if (equipmentDetection.CoversBodyPart.Value) 
                                MyDrawRectangle(coveredBodyPartPen, graphics, equipmentDetection.BoundingBox, bitmap.Size);
                            else
                                MyDrawRectangle(equipamentPen, graphics, equipmentDetection.BoundingBox, bitmap.Size);
                        }
                    }
                }
            }
            pbBoudingBox.Image = bitmap;
        }

        private void PrintRawReturn(DetectProtectiveEquipmentResponse DetectPPEResponse)
        {
            string RawReturnMsg = "";
            foreach (ProtectiveEquipmentPerson person in DetectPPEResponse.Persons)
            {
                RawReturnMsg += string.Format("Person: {0} | Confidence: {1}\n", person.Id, person.Confidence);
                foreach (ProtectiveEquipmentBodyPart bodyPart in person.BodyParts)
                {
                    RawReturnMsg += string.Format("   > BodyPart: {0} | Confidence: {1} \n", bodyPart.Name, bodyPart.Confidence);
                    if (bodyPart.EquipmentDetections.Count > 0)
                    {
                        foreach (EquipmentDetection equipmentDetection in bodyPart.EquipmentDetections)
                        {
                            string msg = "";
                            if (equipmentDetection.CoversBodyPart.Value)
                                msg = string.Format("      - Equipament detected & Covering | Confidence: {0} \n", equipmentDetection.CoversBodyPart.Confidence);
                            else
                                msg = string.Format("      - Equipament detected & NOT Covering | Confidence: {0} \n", equipmentDetection.CoversBodyPart.Confidence);
                            RawReturnMsg += msg;
                        }
                    }
                    else RawReturnMsg += string.Format("      - No Equipament detected\n");
                }
                RawReturnMsg += "----------------------------------------------------\n";
            }
            rtbRawReturn.Text = RawReturnMsg;
        }

        private void PrintConsole(string msg)
        {
            rtbConsole.Text = msg;
        }

        #endregion

        #region Methods for Image Source Radion Button
        private void rb_camera_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCamera.Checked == true)
            {
                tbFilePath.Enabled = false;
                tbFilePath.Visible = false;
                tbFilePath.Text = null;
                bBrowse.Enabled = false;
                bBrowse.Visible = false;
                lFilePath.Enabled = false;
                lFilePath.Visible = false;

                cbCOMPort.Enabled = true;
                cbCOMPort.Visible = true;
                bConnectDisconnect.Enabled = true;
                bConnectDisconnect.Visible = true;
                lCameraSource.Enabled = true;
                lCameraSource.Visible = true;

                bImageAction.Text = "Capture";
            }
        }

        private void rbFile_CheckedChanged(object sender, EventArgs e)
        {
            if(rbFile.Checked == true)
            {
                cbCOMPort.Enabled = false;
                cbCOMPort.Visible = false;
                bConnectDisconnect.Enabled = false;
                bConnectDisconnect.Visible = false;
                lCameraSource.Enabled = false;
                lCameraSource.Visible = false;

                tbFilePath.Enabled = true;
                tbFilePath.Visible = true;
                bBrowse.Enabled = true;
                bBrowse.Visible = true;
                lFilePath.Enabled = true;
                lFilePath.Visible = true;

                bImageAction.Text = "Load";
            }
        }

        #endregion

        #region Methods for Response Presentation Radion Button
        private void rbResponseValidation_CheckedChanged(object sender, EventArgs e)
        {
            if(rbResponseValidation.Checked)
            {
                rtbRawReturn.Enabled = false;
                rtbRawReturn.Visible = false;

                rtbResponseValidation.Enabled = true;
                rtbResponseValidation.Visible = true;

                pbBoudingBox.Enabled = false;
                pbBoudingBox.Visible = false;
            }
        }

        private void rbResponseBoundingBox_CheckedChanged(object sender, EventArgs e)
        {
            if (rbResponseBoundingBox.Checked)
            {
                rtbRawReturn.Enabled = false;
                rtbRawReturn.Visible = false;

                rtbResponseValidation.Enabled = false;
                rtbResponseValidation.Visible = false;

                pbBoudingBox.Enabled = true;
                pbBoudingBox.Visible = true;
            }
        }

        private void rbRawResponse_CheckedChanged(object sender, EventArgs e)
        {
            if (rbRawResponse.Checked)
            {
                rtbRawReturn.Enabled = true;
                rtbRawReturn.Visible = true;

                rtbResponseValidation.Enabled = false;
                rtbResponseValidation.Visible = false;

                pbBoudingBox.Enabled = false;
                pbBoudingBox.Visible = false;
            }
        }
        #endregion

        #region Methods for Buttons Events

        private void bBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tbFilePath.Text = openFileDialog.FileName;
            }
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
                    PrintConsole("Loading Image");
                    pbImage.Image = System.Drawing.Image.FromFile(tbFilePath.Text);
                    PrintConsole("Image Loaded");
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
            PrintConsole("Processing Return");
            ProcessReturn(DetectPPEResponse);
            PrintConsole("Return Processed");
        }

        #endregion

        #region Methods for Text Box Events
        private void tbMinConfLvl_Leave(object sender, EventArgs e)
        {
            if (float.Parse(tbMinConfLvl.Text) < 50) tbMinConfLvl.Text = "50";
            else if (float.Parse(tbMinConfLvl.Text) > 100) tbMinConfLvl.Text = "100";
        }

        #endregion
    }

    public class RealBoudingBox 
    {
        public int width,
                   height,
                   top,
                   left;
        public RealBoudingBox(System.Drawing.Size imageSize, BoundingBox boundingBox) 
        {
            width = (int)(boundingBox.Width * imageSize.Width);
            height = (int)(boundingBox.Height * imageSize.Height);
            top = (int)(boundingBox.Top * imageSize.Height);
            left = (int)(boundingBox.Left * imageSize.Width);
        }
    }
}
