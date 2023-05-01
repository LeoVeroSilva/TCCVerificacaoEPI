# Verificação do uso de EPIs com uso de inteligência artificial

## TO-DOS

- Link/anexo do trabalho final
- UI não responsiva
- Falar sobre Invoke
- Falar sobre regiões

## Resumo

Este projeto em C# (.NET) foi desenvolvido como parte do trabalho de conclusão da graduação em Engenharia da Computação do aluno Leonardo Verona da Silva, na Universidade do Vale do Rio dos Sinos Unisinos. A ideia do projeto surgiu da união entre pontos de interesse do professor orientador e do graduando, e junto dos interesses do graduando em aprofundar o conhecimento no aprendizado de máquina. O projeto utiliza dos serviços de [Detecção de equipamentos de proteção individual] do pacote [Rekognition] da [Amazon Web Services (AWS)] para fazer a verificação e validação do uso de EPI por pessoas. O uso dessa aplicação para permitir ou impedir a entrada de ambientes insalubres ou que oferecem riscos aos trabalhadores pode ajudar na prevenção de acidentes e combater a negligência de funcionários e empregadores. Para mais detalhes é possível conferir o trabalho [Verificação do uso de EPIs com uso de inteligência artificial].

## Aplicações

Forma desenvolvidas duas aplicações desenvolvidas similares mas com propostas similares que podem ser encontradas na pasta [Visual Studio Project].

- O projeto C# [Aplicação de Teste] é a primeira versão do aplicação que foca em facilitar o desenvolvimento, teste e leitura de fluxo de dados. Aqui temos vários elementos de tela que ajudam a acompanhar o fluxo das imagens, validações e retornos do serviço de detecção de EPIs.
- O projeto C# [Aplicação Final] é a versão simplificada, mais sucinta e com foco somente nas informações que são realmente necessárias para utilização de um usuário final.

É possível ver mais detalhes sobre o funcionamento e uso das aplicações nas respectivas pastas dos projetos.

> Notas: 
> - Por conta das simplificações, o fluxo de passos na tela teve que ser otimizado e com isso mudanças no código tiveram que ser feitas, comportamentos ajustados e funcionalidades adicionadas. Dito isso, espere que a Aplicação Final esta mais refinada do que a Aplicação de Teste.*
> - Devido a complexidade e não ser o foco do projeto, não foi possível gerar uma interface responsiva e adaptável com os diferentes proporções e resoluções de tela. O projeto foi desenvolvido com base em um monitor com proporção 16:9 e resolução 1920x1080, logo em algumas telas com proporções ou resoluções menores pode acabar cortando ou escondendo alguns elementos da aplicação.

## Pré-Requisitos

Para rodar e testar essas aplicações existem algumas pré-requisitos como:

- Criar uma conta na AWS e criar um usuário passível de consumo dos serviços do Rekognition;
- Instalação do SDK do AWS para .NET no Visual Studio;
- Baixar algumas bibliotecas para .NET.

É possível consumir essas informações direto do guia do desenvolvedor do [AWS SDK for .NET].

### Conta AWS e Usuário

Primeiramente temos que criar nossa conta no AWS, podemos seguir o seguinte guia da AWS, que basicamente indica como são os primeiros passos para criação e configuração da conta AWS.

[Como criar e ativar uma nova conta da AWS?] da AWS presente no capítulo  que basicamente consiste em criar uma conta 

>Nota: Como AWS é uma plataforma de serviços paga, a criação da conta pedirá informações como endereço, telefone e um cartão de crédito para debitar os consumos. Importante ressaltar que a AWS oferece vários planos, incluindo, um gratuito que permite usufruir de alguns serviços de graça em troca de algumas limitações de uso mensal ou período máximo de uso. Mais detalhes podem ser conferidos em [Amazon Web Services (AWS)].

Uma vez com a conta criada e um as credenciais do usuário root, a AWS recomenda que seja criado um usuário a parte do root que será responsável por consumir e utilizar dos serviços. Isso garante que as super autorizações do root interfiram na gestão do uso dos serviços e aumentar a segurança do vazamento de suas credenciais. Nos guias da AWS, se recomenda utilização do *IAM Identity Center*, mas ela é uma aplicação muito complexa de gestão de usuários e login dos mesmos, que faz mais sentido para empresas que múltiplos empregados, desenvolvedores e contextos diferentes. Como o objetivo desde projeto era usufruir dos serviços sem grandes necessidades de gestão de múltiplos usuários, foi usado uma maneira mais simples para criar o usuário e configurar suas autorizações. Os passos tomados foram os seguintes:

1. Logar na instância AWS com o usuário Root;
2. Acessar o serviço *Identity and Access Management (IAM)*;
3. Na aba *Gerenciamento de acesso*, selecionar a opção *Usuários*;
4. Clicar no botão *Adicionar Usuário*;
5. Seguir o passo-a-passo de criação do usuário até chegar na etapa de definições de permissões;
6. Na etapa de definições de permissões, selecionar a opção *Anexar políticas diretamente*, na seção *Políticas de permissões* pesquisar por "Rekognition", selecionar a permissão *AmazonRekognitionFullAccess* e seguir com a criação do usuário até o final.

Tendo o usuário criado agora precisamos criar as chaves de acesso remoto, permitindo que aplicações externas possam consumir os serviços do Rekognition em nome do user criado. 

1. Voltando para o IAM é possível ver o novo usuário criado listado no sistema, ao clicar no nome do mesmo, navegamos para os detalhes desse usuário. 
2. Na tab *Credenciais de segurança*, temos a seção *Chaves de acesso*, clicar no botão criar *Criar chave de Acesso*;
3. Selecionar a opção "Código Local" e prossiga até confirmação da criação da chave de acesso;
4. Importante baixar o arquivo .csv gerado pois ele contém as credenciais da chave de acesso. Ou você pode anotar os valores da Chave de acesso e d Chave de acesso secreta

### SDK do AWS para .NET no Visual Studio

- Baixar e instalar
- Logar com user do AWS

### Baixar Bibliotecas Adicionais

- NuGet
- AWS
- AForge

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
- Application must have a image capture device for instantaneous and continuously image sampling;

## Amazon Rekognition

### *Image* Object

- Bytes: Blob of image bytes up to 5 MBs. Note that the maximum image size you can pass to DetectCustomLabels is 4MB.
- Type: Base64-encoded binary data object
- Length Constraints: Minimum length of 1. Maximum length of 5242880.
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
[AWS SDK for .NET]:https://docs.aws.amazon.com/pt_br/sdk-for-net/v3/developer-guide/welcome.html
[Como criar e ativar uma nova conta da AWS?](https://repost.aws/pt/knowledge-center/create-and-activate-aws-account)