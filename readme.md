# General Annotation Document

## Useful Links and Information

- General
  - [AWS SDKs and Tools Reference Guide](https://docs.aws.amazon.com/sdkref/latest/guide/overview.html) (Overall documentation about SDKs and Tools)
- .NET
  - [AWS SDK for .NET](https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/welcome.html)(Main page for AWS SDK for .NET)
  - [AWS SDK for .NET Version 3 API Reference](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/Index.html)(THE BIBLE)
  - [AWS SDK for .NET - GitHub](https://github.com/aws/aws-sdk-net)(General information and examples about Services with .NET)
  - [Examples for AWS SDK for .NET 3.x - GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3)(Generic examples for .NET)
- Amazon Web Services (AWS)
  - [Amazon Rekognition](https://docs.aws.amazon.com/rekognition/latest/dg/what-is.html)
    - [Developer Guide - PDF](..\docs\rekognition-dg.pdf)
    - [PPE Detection](https://docs.aws.amazon.com/rekognition/latest/dg/ppe-detection.html)
  


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
