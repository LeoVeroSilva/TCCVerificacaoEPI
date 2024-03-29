# Verificação do uso de EPIs com uso de inteligência artificial

## TO-DOS

- Link/anexo do trabalho final
- Revisar descrição das aplicações
- Melhorar imagem da aplicação final

## Resumo

Este projeto em .NET (C#) foi desenvolvido como parte do trabalho de conclusão da graduação em Engenharia da Computação do aluno Leonardo Verona da Silva, na Universidade do Vale do Rio dos Sinos Unisinos. A ideia do projeto surgiu da união de pontos de interesse comum entre a professora orientadora e do graduando, junto dos interesses do graduando em aprofundar o conhecimento em aprendizado de máquina. O projeto utiliza dos serviços de [Detecção de equipamentos de proteção individual] do pacote [Rekognition] da [Amazon Web Services (AWS)] para fazer a verificação e validação do uso de EPI por pessoas. O projeto permite validar se um trabalhador está apto a entrar em um ambiente que oferece risco a saúde trabalhadores, auxiliar na prevenção de acidentes e combater a negligência de funcionários e empregadores. Para mais detalhes é possível conferir o trabalho [Verificação do uso de EPIs com uso de inteligência artificial].

## Aplicações

Foram desenvolvidas duas aplicações similares mas com propostas diferentes, que podem ser encontradas na pasta [Visual Studio Project].

- O projeto [Aplicação de Teste] é uma versão com foco em facilitar o desenvolvimento, teste e leitura de fluxo de dados. Aqui temos vários elementos de tela que ajudam a acompanhar o fluxo da lógica, validação das imagens e retornos do serviço de detecção de EPIs.
- O projeto [Aplicação Final] é a versão simplificada, mais sucinta e com foco somente nas informações que são realmente necessárias para utilização de um usuário final.

É possível ver mais detalhes sobre o funcionamento e uso das aplicações nas respectivas pastas dos projetos.

> Notas: 
> - Por conta das simplificações, o fluxo de passos na tela teve que ser otimizado e com isso mudanças no código tiveram que ser feitas, comportamentos ajustados e funcionalidades adicionadas. Dito isso, entenda que as aplicações possuem comportamentos diferentes e são usadas de forma diferente também.
> - Devido a complexidade e não ser o foco do projeto, não foi possível gerar uma interface responsiva e adaptável com diferentes proporções e resoluções de tela. O projeto foi desenvolvido com base em um monitor com proporção 16:9 e resolução 1920x1080, logo em algumas telas com proporções ou resoluções menores, pode acabar cortando ou escondendo alguns elementos da tela.

## Pré-Requisitos

Para rodar e testar essas aplicações existem alguns pré-requisitos como:

- Criar uma conta na AWS e criar um usuário passível de consumo dos serviços do Rekognition;
- Instalação do SDK do AWS para .NET no Visual Studio;
- Baixar algumas bibliotecas para .NET.

É possível consumir essas informações direto do guia do desenvolvedor do [AWS SDK for .NET].

### Conta AWS e Usuário

Primeiramente temos que criar nossa conta no AWS, podemos seguir o guia [Como criar e ativar uma nova conta da AWS?] da AWS, que basicamente indica como são os primeiros passos para criação e configuração da conta AWS.

>Notas: 
> - Como AWS é uma plataforma de serviços paga, a criação da conta pedirá informações como endereço, telefone e um cartão de crédito para debitar os consumos. Importante ressaltar que a AWS oferece vários planos, incluindo, um gratuito que permite usufruir de alguns serviços de graça em troca de algumas limitações de uso mensal ou período máximo de uso. Mais detalhes podem ser conferidos em [Amazon Web Services (AWS)].
> - As contas AWS são divididas por regiões e cada região possuí suas regras de cálculo de custo e disponibilidade de serviços. Para este trabalho foi criado a conta na região recomendada, *US East (N. Virginia)* que comportava a criação de uma conta do tipo *Free-Tier* e todos os serviços de interesse estavam disponiveis para consumo. 

Uma vez com a conta criada e um as credenciais do usuário root, a AWS recomenda que seja criado um usuário a parte do root que será responsável por consumir e utilizar dos serviços. Isso garante que as super autorizações do root não interfiram na gestão do uso dos serviços e aumentar a segurança contra o vazamento dessas credenciais. Nos guias da AWS, se recomenda utilização do *IAM Identity Center*, mas ela é uma aplicação muito complexa de gestão de usuários, que faz mais sentido para empresas com múltiplos empregados e diferentes níveis de acesso. Como o objetivo desde projeto era usufruir dos serviços sem grandes necessidades de gestão de múltiplos usuários, foi utilizado uma ferramenta mais simples. Os passos tomados foram os seguintes:

1. Logar na instância AWS com o usuário Root;
2. Acessar o serviço *Identity and Access Management (IAM)*;
3. Na aba *Gerenciamento de acesso*, selecionar a opção *Usuários*;
4. Clicar no botão *Adicionar Usuário*;
5. Seguir o passo-a-passo de criação do usuário até chegar na etapa de definições de permissões;
6. Na etapa de definições de permissões, selecionar a opção *Anexar políticas diretamente*, na seção *Políticas de permissões* pesquisar por "Rekognition", selecionar a permissão *AmazonRekognitionFullAccess* e seguir com a criação do usuário até o final.

Tendo o usuário criado agora precisamos criar as chaves de acesso remoto que permitem que aplicações externas possam consumir os serviços do Rekognition em nome do usuário criado. 

1. Voltando para o IAM é possível ver o novo usuário criado na lista de usuários do sistema, ao clicar no nome do mesmo, navegamos para os detalhes desse usuário. 
2. Na aba *Credenciais de segurança*, na seção *Chaves de acesso*, clicar no botão criar *Criar chave de Acesso*;
3. Selecionar a opção "Código Local" e prossiga até a confirmação da criação da chave de acesso;
4. Importante baixar o arquivo .csv gerado pois ele contém o a chave de acesso e a chave de acesso secreta que vamos utilizar posteriormente (Outra opção válida é anotar os números para uso posterior).

> Nota: 
> - A chave de acesso secreta NÃO pode ser recuperada posteriormente, por isso a importância de anotar essa informação logo na criação. No caso da perca da chave, recomendo regerar uma chave de acesso.
> - Mesmo sendo inconviniente é recomendável que o arquivo .csv ou anotações das chaves sejam deletadas uma vez que elas forem registradas na aplicação final/IDE. Isso diminuí os riscos do vazamento dessas informações.

### SDK do AWS para .NET no Visual Studio

No projeto foi utilizado o Toolkit da AWS para o Visual Studio que permite o gerenciamento do consumo do serviço direto da IDE de desenvolvimento e excluindo a necessidade de uso do interface de linha de comando (CLI).

Primeiramente devemos instalar o Visual Studio e depois instalar a extensão *AWS Toolkit For Visual Studio*. Uma vez com a extensão instalada, a janela *AWS Explorer* vai estar disponível no Visual Studio e nessa janela é possível fornecer as chaves de acesso criadas anteriormente para o usuário e fazer login da instância AWS na IDE. Isso permite o que as bibliotecas da AWS implicitamente busquem as informações de credenciais, região e chave de acesso direto do metadata da IDE.

Para mais detalhes do toolkit, conferir em [AWS Toolkit For Visual Studio]

### Baixar Bibliotecas Adicionais

Importante ressaltar que o AWS Toolkit For Visual Studio somente prepara a IDE para uso simplificado dos serviços da AWS. A extensão não instala nenhum biblioteca específica dos serviços disponíveis. Para baixar as bibliotecas podemos manual baixar os arquivos e incluir no projeto, ou podemos utilizar o NuGet. NuGet é um gerenciador de pacotes e bibliotecas do .NET que cuida da instalação, da atualização e, da incoporação das bibliotecas e pacotes nos projetos .NET. Neste projeto foi utilizado o NuGet dado a sua praticidade.

Convenientemente o Visual Studio oferece funções que integram diretamente com a plataforma do NuGet sem necessidade de instalação de programas externos. Basta procurar pela opção *Gerenciar Pacotes NuGet* no Visual Studio e seleciona-la. Ao selecionar esta opção vai abrir uma nova janela específica do NuGet que nos permite procurar e instalar as seguintes bibliotecas necessárias:

- **AWSSDK.Core**
- **AWSSDK.Rekognition**

Para gestão dos dispositivos de captura (por exemplo, câmeras) e captura de frames, foi utilizado algumas bibliotecas da AForge. AForge é um framework desenvolvido para C# que busca facilitar a gestão de dispositivos de captura e manipulamento de imagens/frames. Segue a lista de bibliotecas necessárias:

- **AForge**
- **AForge.Video**
- **AForge.Video.DirectShow**

Para mais detalhes de como instalar o AWSSDK com NuGet, conferir em [Instale pacotes AWSSDK com o NuGet].

## Bancos de Imagens

Foram utilizados bancos de imagens disponíveis na plataforma Kaggle e Roboflow. Segue o link dos bancos utilizados:

### Kaggle
- [COVID-19 PPE Dataset for Object Detection](https://www.kaggle.com/datasets/ialimustufa/object-detection-for-ppe-covid19-dataset)
- [Gloves Dataset | Covid Safety Wear](https://www.kaggle.com/datasets/dataclusterlabs/gloves-dataset-covid-safety-wear)
- [Construction Site Safety Image Dataset](https://www.kaggle.com/datasets/snehilsanyal/construction-site-safety-image-dataset-roboflow)
- [PPE Detection](https://www.kaggle.com/datasets/mustafatayyipbayram/ppe-detection)
- [Detecting Helmets](https://www.kaggle.com/datasets/sakshamjn/detecting-helmets4-types-cisf-normal-person)
- [Safety Helmet Detection](https://www.kaggle.com/datasets/andrewmvd/hard-hat-detection)

### Roboflow
- [Hard Hat Workers Dataset](https://public.roboflow.com/object-detection/hard-hat-workers)
- [Mask Wearing Dataset](https://public.roboflow.com/object-detection/mask-wearing)
- [Construction Safety](https://universe.roboflow.com/roboflow-100/construction-safety-gsnvb)

## Informações Adicionais

- Uma imagem/vídeo pode ser analisada pelo AWS Rekognition a partir de um repositório de arquivos online da AWS (conhecido como S3) ou pelo envio da imagem em bytes na chamada da API do serviço. A imagem pode ter no máximo 5MBs (4MBs para o serviço *Detect Custom Labels*). Fonte: [Como trabalhar com imagens];
- O serviço de deteção de EPI consegue analisar até 15 pessoas na imagem;
- A grande maioria das APIs do AWS Rekognition utilizam do [Padrão assíncrono baseado em tarefas (TAP)] que consiste na arquitetura de chamadas assincronas para melhor gerenciamento de recursos e processos, como apontado em [APIs assíncronas da AWS para .NET].

## Links Uteis

- [O que é o Amazon Rekognition?](https://docs.aws.amazon.com/pt_br/rekognition/latest/dg/what-is.html)
- [AWS Guia de referência de SDKs e ferramentas](https://docs.aws.amazon.com/pt_br/sdkref/latest/guide/overview.html)
- [AWS Referência para API do SDK para .NET (Versão 3)](https://docs.aws.amazon.com/sdkfornet/v3/apidocs/Index.html)
- [AWS Rekognition - Detect Protective Equipment](https://docs.aws.amazon.com/pt_br/rekognition/latest/APIReference/API_DetectProtectiveEquipment.html)
- [GitHub - AWS SDK para .NET](https://github.com/aws/aws-sdk-net)
- [GitHub - Exemplos com AWS SDK for .NET](https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3)
- [Guia do Desenvolvedor - Detecção de equipamentos de proteção individual](https://docs.aws.amazon.com/pt_br/rekognition/latest/dg/ppe-detection.html)
- [AWS - Segurança no local de trabalho com o Amazon Rekognition](https://aws.amazon.com/pt/rekognition/workplace-safety/)

[Verificação do uso de EPIs com uso de inteligência artificial]: https://
[Amazon Web Services (AWS)]:https://aws.amazon.com/pt/free/?trk=2ee11bb2-bc40-4546-9852-2c4ad8e8f646&sc_channel=ps&ef_id=CjwKCAjwuqiiBhBtEiwATgvixJNd7n1cjYiXkyCLx7UttRTN0KtZCvQ1dq6sXtKCdT40JnqP7HRXphoCZ0sQAvD_BwE:G:s&s_kwcid=AL!4422!3!561843094929!e!!g!!aws!15278604629!130587771740&all-free-tier.sort-by=item.additionalFields.SortRank&all-free-tier.sort-order=asc&awsf.Free%20Tier%20Types=*all&awsf.Free%20Tier%20Categories=*all
[Detecção de equipamentos de proteção individual]:https://docs.aws.amazon.com/pt_br/rekognition/latest/dg/ppe-detection.html
[Rekognition]:https://aws.amazon.com/pt/rekognition/?nc1=h_ls
[Visual Studio Project]:/Visual%20Studio%20Project/
[Aplicação Final]:/Visual%20Studio%20Project/Aplicação%20Final/
[Aplicação de Teste]:/Visual%20Studio%20Project/Aplicação%20de%20Teste/
[AWS SDK for .NET]:https://docs.aws.amazon.com/pt_br/sdk-for-net/v3/developer-guide/welcome.html
[Instalar e configurar sua cadeia de ferramentas]:https://docs.aws.amazon.com/pt_br/sdk-for-net/v3/developer-guide/net-dg-dev-env.html
[AWS Toolkit For Visual Studio]:https://docs.aws.amazon.com/pt_br/toolkit-for-visual-studio/latest/user-guide/welcome.html
[Instale pacotes AWSSDK com o NuGet]:https://docs.aws.amazon.com/pt_br/sdk-for-net/v3/developer-guide/net-dg-install-assemblies.html
[Padrão assíncrono baseado em tarefas (TAP)]:https://learn.microsoft.com/pt-br/dotnet/standard/asynchronous-programming-patterns/task-based-asynchronous-pattern-tap
[APIs assíncronas da AWS para .NET]:https://docs.aws.amazon.com/pt_br/sdk-for-net/v3/developer-guide/sdk-net-async-api.html
[Como trabalhar com imagens]:https://docs.aws.amazon.com/pt_br/rekognition/latest/dg/images.html
