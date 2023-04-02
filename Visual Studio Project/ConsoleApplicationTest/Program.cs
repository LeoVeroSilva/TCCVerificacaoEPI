using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;

namespace ConsoleApplicationTest
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            AWSConfigs.AWSProfileName = "Developer";

            //String path = "";
            String photo = "00004.jpg"; // Local file -> folder bin/debug/

            Amazon.Rekognition.Model.Image image = new Amazon.Rekognition.Model.Image();

            try
            {
                using (FileStream fs = new FileStream(photo, FileMode.Open, FileAccess.Read))
                {
                    byte[] data = null;
                    data = new byte[fs.Length];
                    fs.Read(data, 0, (int)fs.Length);
                    image.Bytes = new MemoryStream(data);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load file " + photo);
                return;
            }

            AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient();

            DetectProtectiveEquipmentRequest DetectPPERequest = new DetectProtectiveEquipmentRequest()
            {
                SummarizationAttributes = new ProtectiveEquipmentSummarizationAttributes
                {
                    MinConfidence = 0,
                    RequiredEquipmentTypes = { "FACE_COVER" , "HAND_COVER" , "HEAD_COVER" },
                },
                Image = image
            };

            try
            {
                DetectProtectiveEquipmentResponse DetectPPEResponse = rekognitionClient.DetectProtectiveEquipment(DetectPPERequest);
                Console.WriteLine("Detected PPE for " + photo);
                foreach (ProtectiveEquipmentPerson Persons in DetectPPEResponse.Persons)
                    foreach(ProtectiveEquipmentBodyPart PPE in Persons.BodyParts)
                        Console.WriteLine("Person {0} - BodyPart {1} : Confidence: {2}", Persons.Id, PPE.Name, PPE.Confidence);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
