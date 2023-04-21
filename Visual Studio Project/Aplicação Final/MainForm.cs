using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using System;
using System.Collections.Generic;
using System.Timers;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Management;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace TCCVerificacaoEPI
{
    public partial class MainForm : Form
    {
        // Variaveis de controle - Essas variaiveis podém ser modificadas para alterar o funcionamento do programa
        float minConfiance = 80; // Confiança mínima aceitável, de 50 a 100 (%)
        int captureInterval = 3; // Tempo de espera após pressionamento do botão de captura (em segundo)
        bool validateHelmet = true; // Se o programa deve verificar capacete
        bool validateMask = true; // Se o programa deve verificar máscara
        bool validateGloves = true; // Se o programa deve verificar luvas

        private AmazonRekognitionClient client;
        FilterInfoCollection filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        VideoCaptureDevice videoCaptureDevice;
        System.Timers.Timer timer = new System.Timers.Timer();
        int interval;
        bool lastFrame = false;

        public MainForm()
        {
            InitializeComponent();
        }

        #region General Methods

        private void MainForm_Load(object sender, EventArgs e)
        {
            setTimer();
            client = CreateAWSRekognitionClient();
            ListCameras();
            if(cbCameraDevices.Items.Count > 0) cbCameraDevices.SelectedIndex = 0;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopCamera();
        }

        private void StopCamera()
        {
            try
            {
                //videoCaptureDevice.Stop();
                videoCaptureDevice.SignalToStop();
                //videoCaptureDevice.WaitForStop();
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

        private void setTimer()
        {
            timer.Interval = 1000; // 1 segundo
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
        }

        private void startTimer()
        {
            bImageAction.Enabled = false;
            bSend.Enabled = false;
            bConnectDisconnect.Enabled = false;
            interval = captureInterval;
            timer.Start();
        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            interval--;
            if (interval == 0)
            {
                timer.Stop();
                lastFrame = true;
            }
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
            if (validateHelmet) RequiredEquipmentTypes.Add("HEAD_COVER");
            if (validateMask) RequiredEquipmentTypes.Add("FACE_COVER");
            if (validateGloves) RequiredEquipmentTypes.Add("HAND_COVER");
            DetectProtectiveEquipmentRequest DetectPPERequest = new DetectProtectiveEquipmentRequest()
            {
                SummarizationAttributes = new ProtectiveEquipmentSummarizationAttributes
                {
                    MinConfidence = minConfiance,
                    RequiredEquipmentTypes = RequiredEquipmentTypes,
                },
                Image = image
            };
            return client.DetectProtectiveEquipment(DetectPPERequest);
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
            printReturnValidatin(DetectPPEResponse);
            DrawReturnBoudingBox(DetectPPEResponse);
        }

        private void MyDrawRectangle(System.Drawing.Pen pen, System.Drawing.Graphics graphics, BoundingBox boundingBox, Size imageSize)
        {
            RealBoudingBox realBoundingBox = new RealBoudingBox(imageSize, boundingBox);
            graphics.DrawRectangle(pen, realBoundingBox.left, realBoundingBox.top, realBoundingBox.width, realBoundingBox.height);
        }
        
        private void DrawConfidence(System.Drawing.Graphics graphics, BoundingBox boundingBox, Size imageSize, float confidence, Color color)
        {
            int fontSize = FontSizeFromRatio(imageSize, 0.05f);

            Font font = new Font(FontFamily.GenericSansSerif, fontSize);
            Brush brush = new SolidBrush(color);

            RealBoudingBox realBoundingBox = new RealBoudingBox(imageSize, boundingBox);
            System.Drawing.PointF point = new System.Drawing.PointF(realBoundingBox.left, realBoundingBox.top);
            graphics.DrawString(confidence.ToString(), font, brush, point);
        }

        private void MyDrawString(System.Drawing.Graphics graphics, Size imageSize, Color color, string msg)
        {
            int fontSize = FontSizeFromRatio(imageSize, 0.25f);

            Font font = new Font(FontFamily.GenericSansSerif, fontSize);
            Brush brush = new SolidBrush(color);
            SizeF textSize = graphics.MeasureString(msg, font);

            float x = (imageSize.Width/2) - (textSize.Width / 2);
            float y = (imageSize.Height/2) - (textSize.Height / 2);

            System.Drawing.PointF point = new System.Drawing.PointF(x,y);
            graphics.DrawString(msg, font, brush, point);
        }

        private int FontSizeFromRatio(Size imageSize, float ratio)
        {
            int imageWidth = imageSize.Width;
            int imageHeight = imageSize.Height;
            float fontRatio = ratio; // 5% of the image size
            int fontSize = (int)(Math.Min(imageWidth, imageHeight) * fontRatio);
            return fontSize;
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
            Bitmap frame = (Bitmap)e.Frame.Clone();
            if (timer.Enabled)
            {
                System.Drawing.Graphics graphics = Graphics.FromImage(frame);
                MyDrawString(graphics, frame.Size, Color.White, interval.ToString());
            }
            else if (lastFrame)
            {
                lastFrame = false;
                System.Drawing.Image image = pbImage.Image;
                StopCamera();
                pbImage.Image = image;
                bImageAction.Enabled = true;
                bSend.Enabled = true;
                bConnectDisconnect.Enabled = true;
                bImageAction.Text = "Resetar";
            }
            pbImage.Image = frame;
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
                validationMsg += string.Format("Pessoa: {0} é ideterminável\n", person);
            }
            rtbResponseValidation.Text = validationMsg;
        }

        private void DrawReturnBoudingBox(DetectProtectiveEquipmentResponse DetectPPEResponse) 
        {
            Bitmap bitmap = new Bitmap(pbImage.Image);
            Graphics graphics = Graphics.FromImage(bitmap);

            Pen personPen = new Pen(Color.Yellow, 3);
            Pen coveredBodyPartPen = new Pen(Color.Green, 3);
            Pen equipamentPen = new Pen(Color.Red, 3);

            foreach (ProtectiveEquipmentPerson person in DetectPPEResponse.Persons)
            {
                MyDrawRectangle(personPen, graphics, person.BoundingBox, bitmap.Size);
                DrawConfidence(graphics, person.BoundingBox, bitmap.Size, person.Confidence, personPen.Color);
                foreach (ProtectiveEquipmentBodyPart bodyPart in person.BodyParts)
                {
                    //MyDrawRectangle(bodyPartPen, graphics, bodyPart., bitmap.Size);
                    if (bodyPart.EquipmentDetections.Count > 0)
                    {
                        foreach (EquipmentDetection equipmentDetection in bodyPart.EquipmentDetections)
                        {
                            // Body Part Equipament
                            if (equipmentDetection.CoversBodyPart.Value)
                            {
                                MyDrawRectangle(coveredBodyPartPen, graphics, equipmentDetection.BoundingBox, bitmap.Size);
                                DrawConfidence(graphics, equipmentDetection.BoundingBox, bitmap.Size, equipmentDetection.Confidence, coveredBodyPartPen.Color);
                            }
                            else
                            {
                                MyDrawRectangle(equipamentPen, graphics, equipmentDetection.BoundingBox, bitmap.Size);
                                DrawConfidence(graphics, equipmentDetection.BoundingBox, bitmap.Size, equipmentDetection.Confidence, equipamentPen.Color);
                            } 
                        }
                    }
                }
            }
            pbBoudingBox.Image = bitmap;
        }

        #endregion

        #region Methods for Buttons Events

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
                bImageAction.Text = "Capturar";
                StopCamera();
                pbImage.Image = null;
            }
        }

        private void bImageAction_Click(object sender, EventArgs e)
        {
            switch (bImageAction.Text)
            {
                case "Capturar":
                    if (videoCaptureDevice != null)
                    {
                        startTimer();
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
            Amazon.Rekognition.Model.Image image;
            image = GetImageFromPictureBox();
            DetectProtectiveEquipmentResponse DetectPPEResponse = await DetectPPE(client, image);
            ProcessReturn(DetectPPEResponse);
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
