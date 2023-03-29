using System;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;

class AWSClass
{
    static async Task Main(string[] args)
    {
        var region = RegionEndpoint.USWest2; // Replace with the region where your Rekognition service is located
        var client = new AmazonRekognitionClient(region); // Create a new Rekognition client

        // Replace with the name of the image file you want to analyze
        var imagePath = "example.jpg";
        var imageBytes = File.ReadAllBytes(imagePath);  

        var detectProtectiveEquipmentRequest = new DetectProtectiveEquipmentRequest
        {
            Image = new Image { Bytes = new MemoryStream(imageBytes) },
            SummarizationAttributes = new ProtectiveEquipmentSummarizationAttributes
            {
                MinConfidence = 80F // Set the minimum confidence level required for a PPE detection to be returned
            }
        };

        var detectProtectiveEquipmentResponse = await client.DetectProtectiveEquipmentAsync(detectProtectiveEquipmentRequest);

        Console.WriteLine("Detected PPE:");
        foreach (var bodyPart in detectProtectiveEquipmentResponse.Persons[0].BodyParts)
        {
            Console.WriteLine($"  {bodyPart.Name}:");

            foreach (var equipment in bodyPart.EquipmentDetections)
            {
                Console.WriteLine($"    {equipment.Type} (confidence: {equipment.Confidence})");
            }
        }
    }
}