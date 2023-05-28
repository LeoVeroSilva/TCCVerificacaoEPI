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
using System.Diagnostics.Eventing.Reader;
using System.Linq;

namespace TCCVerificacaoEPI
{
    public partial class MainForm : Form
    {
        // Variaveis de ajuste - Essas variaiveis podém ser modificadas para alterar o funcionamento do programa
        float minConfiance = 80; // Confiança mínima aceitável, de 50 a 100 (%)
        int captureInterval = 3; // Tempo de espera após pressionamento do botão de captura (em segundo)
        bool validateHelmet = true; // Se o programa deve verificar capacete
        bool validateMask = true; // Se o programa deve verificar máscara
        bool validateGloves = true; // Se o programa deve verificar luvas

        // Imagens usadas para a sinalização da validação - Localizado na pasta bin/debug
        System.Drawing.Image greenLight = Bitmap.FromFile("GREEN.JPG");
        System.Drawing.Image yellowLight = Bitmap.FromFile("YELLOW.JPG");
        System.Drawing.Image redLight = Bitmap.FromFile("RED.JPG");

        private AmazonRekognitionClient client;

        FilterInfoCollection filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        VideoCaptureDevice videoCaptureDevice;

        System.Timers.Timer timer = new System.Timers.Timer();
        int remainingTime; // Indica quanto tempo em segundos ainda resta até a captura
        bool lastFrame = false; // Indica quando o processo da câmera deve pegar um último quadro

        public MainForm()
        {
            InitializeComponent();
        }

        #region Delegates

        delegate void LastFrameTakenCallback();

        private async void LastFrameTaken()
        {
            // Como o processo da câmera não tem direito de manipular objetos da UI que são gerenciados no processo principal/UI, vamos utilizar o modelamento Invoke para gerenciar essa limitação

            // Em resumo, o modelamento Invoke permite que um método possa ser utilizado por mais de uma thread, porém me permite chamar o méotodo em um processo mas delegar a sua execução para outro processo

            if (this.InvokeRequired)
            {
                LastFrameTakenCallback callBack = new LastFrameTakenCallback(LastFrameTaken);
                this.Invoke(callBack);
            }
            else
            {
                StopCamera();

                DetectProtectiveEquipmentResponse DetectPPEResponse = DetectPPE(client, GetImageFromPictureBox());
                ProcessReturn(DetectPPEResponse);
                
                bImageAction.Enabled = true;
                bImageAction.Text = "Resetar";
            }
        }

        #endregion

        #region General Methods

        private void MainForm_Load(object sender, EventArgs e)
        {
            setTimer(); 
            client = CreateAWSRekognitionClient(); // Conecta a instância do serviço AWS baseado nas configurações do projeto e/ou da IDE
            InitCamera();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopCamera();
        }

        private void InitCamera()
        {
            if (filterInfoCollection.Count > 0)
            {
                ConnectCamera();
                StartCamera();
            }
            else
            {
                // No camera detected
            }
        }

        private void StopCamera()
        {
            try
            {
                videoCaptureDevice.SignalToStop();
            }
            catch { }
        }

        private void StartCamera()
        {
            videoCaptureDevice.Start();
        }

        private void ConnectCamera()
        {
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[0].MonikerString);
            videoCaptureDevice.NewFrame += new NewFrameEventHandler(CameraNewFrame);
        }

        private void CameraNewFrame(object sender, NewFrameEventArgs e)
        {
            Bitmap frame = (Bitmap)e.Frame.Clone();
            if (timer.Enabled)
            {
                System.Drawing.Graphics graphics = Graphics.FromImage(frame);
                MyDrawString(graphics, frame.Size, Color.White, remainingTime.ToString());
            }
            pbImage.Image = frame;
            if (lastFrame)
            {
                lastFrame = false;
                LastFrameTaken();
            }
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
            remainingTime = captureInterval;
            timer.Start();
        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            remainingTime--;
            if (remainingTime == 0)
            {
                timer.Stop();
                lastFrame = true;
            }
        }

        private AmazonRekognitionClient CreateAWSRekognitionClient()
        {
            AWSConfigs.AWSProfileName = "Developer"; // Aqui trocamos o nome do usuário a se conectar aos serviços da AWS (Por padrão a instância sempre tenta se conectar ao usuário Default)
            AmazonRekognitionClient client = new AmazonRekognitionClient();
            return client;
        }

        private DetectProtectiveEquipmentResponse DetectPPE(AmazonRekognitionClient client, Amazon.Rekognition.Model.Image image)
        {
            // Monta a requisição pro serviço do AWS com a imagem capturada e definindo quais EPIs devem ser verificados
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
            Validation(DetectPPEResponse);
            DrawReturnBoudingBox(DetectPPEResponse);
        }

        private void MyDrawRectangle(System.Drawing.Pen pen, System.Drawing.Graphics graphics, BoundingBox boundingBox, Size imageSize)
        {
            // Desenha um retângulo baseado no BoudingBox retornado pelo serviço
            RealBoudingBox realBoundingBox = new RealBoudingBox(imageSize, boundingBox);
            graphics.DrawRectangle(pen, realBoundingBox.left, realBoundingBox.top, realBoundingBox.width, realBoundingBox.height);
        }

        private void DrawConfidence(System.Drawing.Graphics graphics, BoundingBox boundingBox, Size imageSize, float confidence, Color color)
        {
            // Escreve a confiança da predição no canto superior esquerdo do Bouding Box de forma dinâmica ao tamanho da imagem
            int fontSize = FontSizeFromRatio(imageSize, 0.05f);

            Font font = new Font(FontFamily.GenericSansSerif, fontSize);
            Brush brush = new SolidBrush(color);

            RealBoudingBox realBoundingBox = new RealBoudingBox(imageSize, boundingBox);
            System.Drawing.PointF point = new System.Drawing.PointF(realBoundingBox.left, realBoundingBox.top);
            graphics.DrawString(confidence.ToString(), font, brush, point);
        }

        private void MyDrawString(System.Drawing.Graphics graphics, Size imageSize, Color color, string msg)
        {
            // Escreve um texto no meio da tela de forma dinâmica ao tamanho da imagem
            int fontSize = FontSizeFromRatio(imageSize, 0.25f);

            Font font = new Font(FontFamily.GenericSansSerif, fontSize);
            Brush brush = new SolidBrush(color);
            SizeF textSize = graphics.MeasureString(msg, font);

            float x = (imageSize.Width / 2) - (textSize.Width / 2);
            float y = (imageSize.Height / 2) - (textSize.Height / 2);

            System.Drawing.PointF point = new System.Drawing.PointF(x, y);
            graphics.DrawString(msg, font, brush, point);
        }

        private int FontSizeFromRatio(Size imageSize, float ratio)
        {
            // Define um tamanho de fonte baseado no tamanho da imagem
            int imageWidth = imageSize.Width;
            int imageHeight = imageSize.Height;
            float fontRatio = ratio; // 5% of the image size
            int fontSize = (int)(Math.Min(imageWidth, imageHeight) * fontRatio);
            return fontSize;
        }

        private float LowestConfidence(ProtectiveEquipmentBodyPart bodyPart)
        {
            float bodyPartConfidence = bodyPart.Confidence;
            float equipamentDetectedConfidence = bodyPart.EquipmentDetections.First().Confidence;
            float coversBodyPartConfidence = bodyPart.EquipmentDetections.First().CoversBodyPart.Confidence;
            float lowestConfidence = bodyPartConfidence;

            if (lowestConfidence > equipamentDetectedConfidence) lowestConfidence = equipamentDetectedConfidence;
            if (lowestConfidence > coversBodyPartConfidence) lowestConfidence = coversBodyPartConfidence;

            return lowestConfidence;
        }

        #endregion

        #region Methods for Print Text/Images
        private void Validation(DetectProtectiveEquipmentResponse DetectPPEResponse) 
        {
            string validationMsg = "";

            if (DetectPPEResponse.Persons.Count > 1) // Mais de uma pessoa identificada na captura, o programa apenas valida uma pessoa por vez
            {
                validationMsg = "Apenas uma pessoa por validação";
                pbSemaphore.Image = redLight;
            }
            else if (DetectPPEResponse.Summary.PersonsWithRequiredEquipment.Count > 0) // Pessoa detectada possuí todos os EPIs requeridos e atende a confiança mínima
            {
                validationMsg = "Autorizado";
                pbSemaphore.Image = greenLight;
            }
            else if (DetectPPEResponse.Summary.PersonsWithoutRequiredEquipment.Count > 0) // Pessoa detectada NÃO possuí todos os EPIs requeridos e atende a confiança mínima
            {
                validationMsg = "NÃO Autorizado";
                pbSemaphore.Image = redLight;
            }
            else // Nenhuma pessoa detectada; Nível de confiança muito baixo para determinar uma pessoa ou EPI
            {
                validationMsg = "Indeterminável";
                pbSemaphore.Image = yellowLight;
            }
            tbValidation.Text = validationMsg;
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
                // Desenha o BoudingBox da pessoa
                MyDrawRectangle(personPen, graphics, person.BoundingBox, bitmap.Size);
                DrawConfidence(graphics, person.BoundingBox, bitmap.Size, person.Confidence, personPen.Color);
                foreach (ProtectiveEquipmentBodyPart bodyPart in person.BodyParts)
                {
                    if (bodyPart.EquipmentDetections.Count > 0)
                    {
                        foreach (EquipmentDetection equipmentDetection in bodyPart.EquipmentDetections)
                        {
                            float confidence = LowestConfidence(bodyPart);
                            // Desenha o BoudingBox da EPI
                            if (equipmentDetection.CoversBodyPart.Value)
                            {
                                // EPI vestido pela pessoa
                                MyDrawRectangle(coveredBodyPartPen, graphics, equipmentDetection.BoundingBox, bitmap.Size);
                                DrawConfidence(graphics, equipmentDetection.BoundingBox, bitmap.Size, confidence, coveredBodyPartPen.Color);
                            }
                            else
                            {
                                // EPI NÃO vestido pela pessoa
                                MyDrawRectangle(equipamentPen, graphics, equipmentDetection.BoundingBox, bitmap.Size);
                                DrawConfidence(graphics, equipmentDetection.BoundingBox, bitmap.Size, confidence, equipamentPen.Color);
                            } 
                        }
                    }
                }
            }
            pbImage.Image = bitmap;
        }

        #endregion

        #region Methods for Buttons Events

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
                    pbSemaphore.Image = null;
                    tbValidation.Text = null;
                    break;
            }
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
