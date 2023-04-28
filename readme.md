# Verificação do uso de EPIs com uso de inteligência artificial
## TO-DOS
- Link/anexo do trabalho final

## Resumo

Este projeto em C# (.NET) foi desenvolvido como parte do trabalho de conclusão da graduação em Engenharia da Computação do aluno Leonardo Verona da Silva, na Universidade do Vale do Rio dos Sinos Unisinos. A ideia do projeto surgiu da união entre pontos de interesse do professor orientador e do graduando, e junto dos interesses do graduando em aprofundar o conhecimento no aprendizado de máquina. O projeto utiliza dos serviços de [Detecção de equipamentos de proteção individual] do pacote [Rekognition] da [Amazon Web Services (AWS)] para fazer a verificação e validação do uso de EPI por pessoas. O uso dessa aplicação para permitir ou impedir a entrada de ambientes ensalubres ou que oferem riscos aos trabalhadores pode ajudar na prevenção de acidentes e combater a negligência de funcionários e empregadores. Para mais detalhes é possível conferir o trabalho [Verificação do uso de EPIs com uso de inteligência artificial].

---

## Aplicações

Forma desenvolvidas duas aplicaçes desenvolvidas similares mas com propostas similares que podem ser encontradas na pasta [Visual Studio Project].

- O projeto C# [Aplicação de Teste] é a primeira versão do aplicação que foca em facilitar o desenvolvimento, teste e leitura de fluxo de dados. Aqui temos vários elementos de tela que ajudam a acompanhar o fluxo das imagens, validações e retornos do serviço de detecção de EPIs.
- O projeto C# [Aplicação Final] é a versão simplificada, mais sussinta e com foco somente nas informações que são realmente necessárias para utilização de um usuário final.

> *Nota: Por conta das simplificações, o fluxo de passos na tela teve que ser otimizado e com isso mudanças no código tiveram que ser feitas, comportamentos ajustados e funcionalidades adicionadas. Dito isso, espere que a Aplicação Final estja mais refinada do que a Aplicação de Teste.*

### Descritivo da Aplicação de Teste

![Aplicação de Teste](/imgs/Aplicação%20de%20Teste.png "Aplicação de Teste")

Listando os elementos da tela do canto esquerdo superior até o canto inferior direito, temos:

- Opção de selecionara origem da imagem a ser analisada, sendo ou originada de um arquivo local ou da captura de alguma câmera local;
- Um campo que permite a informação do caminho do arquivo a ser carregado e junto um botão que permite utilizar o gerenciador de arquivos nativo do Windows;
- Um campo que permite a seleção do dispositivo de captura (camerâ) e, um botão para conectar e desconectar com o dispositivo de captura;

> Os elementos de tela exclusivo para o gerenciamento de arquivos e exclusivos para gerenciamento do dispositivo de captura possuem comportament dinâmico na tela. Ou seja, se for selecionado a opção de fonte da imagem vinda de arquivo, então somente os campos focados na gestão do arquivo irão aparecer. Assim como selecionar a opção de fonte da imagem vinda do dispositivo de captura irá fazer somente os campos focados na gestão do dispositivo de captura aparecer.

> Uma vez que o dispositivo de captura for conectado com sucesso, o vídeo será visivel na sessão **Imagem**

- A seção denominada como **Imagem** é onde será mostrado a imagem carregada, ou vídeo/imagem do dispositivo de captura;
- Um botão responsável por carregar a imagem do arquivo local, ou capturar um frame do dispositivo de captura, ou reinicia o vídeo do dispositivo de captura;

> Novamente temos elementos de tela com comportamento dinâmico de acordo com a opção da origem selecionada e estado da aplicação. Por exemplo:
>> Ao selecionar <ins>arquivo local</ins> como origem da imagem, o botão vai estar com o texto "*Carregar*", permite o carregamento da imagem local para vizualização da mesma e conferência antes do envio para verificação.
>
>> Ao selecionar <ins>dispositivo de captura</ins> como origem da imagem, o botão vai estar com o texto "*Capturar*", permite captura do último frame retornado do dispositivo de captura para vizualizar da mesma e conferência antes do envio para verificação. Uma vez comum frame capturado, o botão vai estar com o texto "*Resetar*", permite descartar o frame capturado e reiniciar o vídeo do dispositivo de captura.

### Descritivo da Aplicação Final

---

##

## Useful Links and Information

- General
  - [AWS SDKs and Tools Reference Guide](https://docs.aws.amazon.com/sdkref/latest/guide/overview.html) (Overall documentation about SDKs and Tools)
- .NET
  - [AWS SDK for .NET](https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/welcome.html)(Main page for AWS SDK for .NET)
    - [AWS asynchronous APIs for .NET](https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/sdk-net-async-api.html)
    - 
  - [AWS SDK for .NET Version 3 API Reference](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/Index.html)(THE BIBLE)
    - [DetectProtectiveEquipment Method](https://docs.aws.amazon.com/rekognition/latest/APIReference/API_DetectProtectiveEquipment.html)
  - [AWS SDK for .NET - GitHub](https://github.com/aws/aws-sdk-net)(General information and examples about Services with .NET)
  - [Examples for AWS SDK for .NET 3.x - GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3)(Generic examples for .NET)
- Amazon Web Services (AWS)
  - [Amazon Rekognition](https://docs.aws.amazon.com/rekognition/latest/dg/what-is.html)
    - [Developer Guide - PDF](/docs/rekognition-dg.pdf)
    - [Rekognition Custom Labels](/docs/Rekognition%20Custom%20Labels.pdf)
    - [PPE Detection](https://docs.aws.amazon.com/rekognition/latest/dg/ppe-detection.html)
      - [Example in Java - GitHub](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/javav2/usecases/creating_lambda_ppe)
- Nice to Know/Refer
  - [Amazon Rekognition Workplace Safety](https://aws.amazon.com/rekognition/workplace-safety/)
  - [Task-based asynchronous pattern (TAP) in .NET: Introduction and overview](https://learn.microsoft.com/en-us/dotnet/standard/asynchronous-programming-patterns/task-based-asynchronous-pattern-tap)


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

### *Image* Object

- Bytes: Blob of image bytes up to 5 MBs. Note that the maximum image size you can pass to DetectCustomLabels is 4MB.
- Type: Base64-encoded binary data object
- Lenth Constraints: Minimum length of 1. Maximum length of 5242880.
- Required: No

### Personal Protective Equipment (PPE)

API Method: DetectProtectiveEquipment 

#### Known Limitation

- Up to 15 persons max per image

[Verificação do uso de EPIs com uso de inteligência artificial]:

[Amazon Web Services (AWS)]:https://aws.amazon.com/pt/free/?trk=2ee11bb2-bc40-4546-9852-2c4ad8e8f646&sc_channel=ps&ef_id=CjwKCAjwuqiiBhBtEiwATgvixJNd7n1cjYiXkyCLx7UttRTN0KtZCvQ1dq6sXtKCdT40JnqP7HRXphoCZ0sQAvD_BwE:G:s&s_kwcid=AL!4422!3!561843094929!e!!g!!aws!15278604629!130587771740&all-free-tier.sort-by=item.additionalFields.SortRank&all-free-tier.sort-order=asc&awsf.Free%20Tier%20Types=*all&awsf.Free%20Tier%20Categories=*all
[Detecção de equipamentos de proteção individual]:https://docs.aws.amazon.com/pt_br/rekognition/latest/dg/ppe-detection.html
[Rekognition]:https://aws.amazon.com/pt/rekognition/?nc1=h_ls
[Visual Studio Project]:/Visual%20Studio%20Project/
[Aplicação Final]:/Visual%20Studio%20Project/Aplicação%20Final/
[Aplicação de Teste]:/Visual%20Studio%20Project/Aplicação%20de%20Teste/