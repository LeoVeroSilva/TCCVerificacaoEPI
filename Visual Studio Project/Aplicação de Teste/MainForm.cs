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
using System.Management;
using AForge.Video;
using AForge.Video.DirectShow;

namespace TCCVerificacaoEPI
{
    public partial class MainForm : Form
    {
        private AmazonRekognitionClient client;
        FilterInfoCollection filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        VideoCaptureDevice videoCaptureDevice;

        public MainForm()
        {
            InitializeComponent();
        }

        #region General Methods

        private void MainForm_Load(object sender, EventArgs e)
        {
            PrintConsole("Inicializando");
            client = CreateAWSRekognitionClient();
            ListCameras();
            PrintConsole("Inicializado");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopCamera();
        }

        private void StopCamera()
        {
            try
            {
                if (videoCaptureDevice.IsRunning)
                    videoCaptureDevice.Stop();
            }
            catch { }
        }

        private void StartCamera()
        {
            videoCaptureDevice.Start();
        }

        private void ConnectCamera()
        {
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[cbCameraDevices.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += CameraNewFrame;
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

        private Amazon.Rekognition.Model.Image GetImageFromPictureBox()
        {
            Amazon.Rekognition.Model.Image image = new Amazon.Rekognition.Model.Image();
            MemoryStream ms = new MemoryStream();
            pbImage.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            image.Bytes = ms;
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

        private void MyDrawString(System.Drawing.Graphics graphics, BoundingBox boundingBox, Size imageSize, String msg)
        {
            int imageWidth = imageSize.Width;
            int imageHeight = imageSize.Height;
            float fontRatio = 0.05f; // 5% of the image size
            int fontSize = (int)(Math.Min(imageWidth, imageHeight) * fontRatio);

            Font font = new Font(FontFamily.GenericSansSerif, fontSize);
            Brush brush = new SolidBrush(Color.Green);
           
            RealBoudingBox realBoundingBox = new RealBoudingBox(imageSize, boundingBox);
            System.Drawing.PointF point = new System.Drawing.PointF(realBoundingBox.left, realBoundingBox.top);
            graphics.DrawString(msg, font, brush, point);
        }

        private void ListCameras()
        {
            foreach(FilterInfo filterInfo in filterInfoCollection)
            {
                cbCameraDevices.Items.Add(filterInfo.Name);
            }
        }

        private void CameraNewFrame(object sender, NewFrameEventArgs e)
        {
            pbImage.Image = (Bitmap) e.Frame.Clone();
        }

        #endregion

        #region Methods for Print Text/Images
        private void printReturnValidatin(DetectProtectiveEquipmentResponse DetectPPEResponse) 
        {
            string validationMsg = "";
            foreach (int person in DetectPPEResponse.Summary.PersonsWithRequiredEquipment)
            {
                validationMsg += string.Format("Pessoa: {0} Autorizadad\n", person);
            }
            foreach (int person in DetectPPEResponse.Summary.PersonsWithoutRequiredEquipment)
            {
                validationMsg += string.Format("Pessoa: {0} NÃO Autorizadad\n", person);
            }
            foreach (int person in DetectPPEResponse.Summary.PersonsIndeterminate)
            {
                validationMsg += string.Format("Pessoa: {0} é indeterminável\n", person);
            }
            rtbResponseValidation.Text = validationMsg;
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
                MyDrawString(graphics, person.BoundingBox, bitmap.Size,  person.Id.ToString());
                foreach (ProtectiveEquipmentBodyPart bodyPart in person.BodyParts)
                {
                    //MyDrawRectangle(bodyPartPen, graphics, bodyPart., bitmap.Size);
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
                RawReturnMsg += string.Format("Pessoa: {0} | Confiânça: {1}%\n", person.Id, person.Confidence);
                foreach (ProtectiveEquipmentBodyPart bodyPart in person.BodyParts)
                {
                    string bodyPartName = "";
                    switch (bodyPart.Name)
                    {
                        case "HEAD":
                            bodyPartName = "CABEÇA";
                            break;
                        case "LEFT_HAND":
                            bodyPartName = "M.ESQUERDA";
                            break;
                        case "RIGHT_HAND":
                            bodyPartName = "M.DIREITA";
                            break;
                        case "FACE":
                            bodyPartName = "ROSTO";
                            break;
                    }
                    RawReturnMsg += string.Format("   > Parte do corpo: {0} | Confiânça: {1}% \n", bodyPartName, bodyPart.Confidence);
                    if (bodyPart.EquipmentDetections.Count > 0)
                    {
                        foreach (EquipmentDetection equipmentDetection in bodyPart.EquipmentDetections)
                        {
                            RawReturnMsg += string.Format("      - Equipamento Detectado | Confiânça: {0}% \n", equipmentDetection.Confidence); ;
                            if (equipmentDetection.CoversBodyPart.Value)
                                RawReturnMsg += string.Format("         * Cobrindo | Confiânça: {0}% \n", equipmentDetection.CoversBodyPart.Confidence);
                            else
                                RawReturnMsg += string.Format("         * NÃO Cobrindo | Confiânça: {0}% \n", equipmentDetection.CoversBodyPart.Confidence);
                        }
                    }
                    else RawReturnMsg += string.Format("      - Nenhum Equipamento Detectado\n");
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

                cbCameraDevices.Enabled = true;
                cbCameraDevices.Visible = true;
                bConnectDisconnect.Enabled = true;
                bConnectDisconnect.Visible = true;
                lCameraSource.Enabled = true;
                lCameraSource.Visible = true;

                pbImage.Image = null;

                bImageAction.Text = "Capturar";
            }
        }

        private void rbFile_CheckedChanged(object sender, EventArgs e)
        {
            if(rbFile.Checked == true)
            {
                cbCameraDevices.Enabled = false;
                cbCameraDevices.Visible = false;
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

                StopCamera();
                bConnectDisconnect.Text = "Conectar";

                pbImage.Image = null;

                bImageAction.Text = "Carregar";
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
            if (bConnectDisconnect.Text == "Conectar")
            {
                bConnectDisconnect.Text = "Disconectar";
                ConnectCamera();
                StartCamera();
            }
            else
            {
                bConnectDisconnect.Text = "Conectar";
                bConnectDisconnect.Text = "Capturar";
                StopCamera();
                pbImage.Image = null;
            }
        }

        private void bImageAction_Click(object sender, EventArgs e)
        {
            switch (bImageAction.Text)
            {
                case "Carregar":
                    PrintConsole("Carregando Imagem");
                    pbImage.Image = System.Drawing.Image.FromFile(tbFilePath.Text);
                    PrintConsole("Imagem Carregada");
                    break;
                case "Capturar":
                    if (videoCaptureDevice != null)
                    {
                        System.Drawing.Image image = pbImage.Image;
                        StopCamera();
                        pbImage.Image = image;
                        bImageAction.Text = "Resetar";
                    }
                    break;
                case "Resetar":
                    pbImage.Image = null;
                    StartCamera();
                    bImageAction.Text = "Capturar";
                    break;
            }
        }

        private async void bSend_Click(object sender, EventArgs e)
        {
            PrintConsole("Enviando");
            Amazon.Rekognition.Model.Image image;
            if (rbFile.Checked)
                image = GetImageFromFile(tbFilePath.Text);
            else
                image = GetImageFromPictureBox();
            DetectProtectiveEquipmentResponse DetectPPEResponse = await DetectPPE(client, image);
            PrintConsole("Processando Retorno");
            ProcessReturn(DetectPPEResponse);
            PrintConsole("Retorno Processado");
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
