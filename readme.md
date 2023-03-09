# General Annotation Document

## Useful Links and Information

- [Detecting personal protective equipment](https://docs.aws.amazon.com/rekognition/latest/dg/ppe-detection.html)

- [General API Documentation](https://docs.aws.amazon.com/rekognition/latest/APIReference/API_DetectProtectiveEquipment.html)

- [Amazon Rekognition SDK for .NET](https://aws.amazon.com/sdk-for-net/)

## Definition of Requirements

### Functional Requirements

- The application must allow the selection between the services of PPE Detection and Custom Labels;
- It should have authentication controls and possibility to input the API Key and necessary credentials;
- It should remember the API Key, when asked, within a external file;

### Technical Requirements

- Application must consume the API from AWS Rekognition:
  - PPE Detection Services;
  - Custom Labels Services;
- Application must have a image capture device for instantanous and continuously image sampling;

## Amazon Rekognition

### Personal Protective Equipment (PPE) 

#### Known Limitation

- Up to 15 persons max per image
