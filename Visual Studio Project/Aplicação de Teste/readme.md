### Descritivo da Aplicação de Teste

![Aplicação de Teste](/imgs/Aplicação%20de%20Teste.png "Aplicação de Teste")

O conjunto dos dois *radio button* serve para selecionar a origem da imagem a ser analisada. Sendo ou originada de um arquivo local ou de algum dispositivo de captura (como por exemplo uma câmera ou webcam).

> Os elementos de tela exclusivo para o gerenciamento de arquivos e exclusivos para gerenciamento do dispositivo de captura possuem comportament dinâmico na tela. Ou seja, se for selecionado a opção de fonte da imagem vinda de arquivo, então somente os campos focados na gestão do arquivo irão aparecer. Assim como selecionar a opção de fonte da imagem vinda do dispositivo de captura irá fazer somente os campos focados na gestão do dispositivo de captura aparecer.

O campo **Arquivo** permite a informação do caminho do arquivo a ser carregado. Junto ao campo existe um botão que permite utilizar o sistema de busca de arquivos nativo do Windows. O campo **Câmera** que permite a seleção do dispositivo de captura e o botão ao lado permite conectar e desconectar do dispositivo de captura.

> Uma vez que o dispositivo de captura for conectado com sucesso, o vídeo será visivel na sessão **Imagem**.

A seção denominada como **Imagem** é onde será mostrado a imagem carregada a partir de um arquivo, ou vídeo/frame do dispositivo de captura. O botão abaixo da seção **Imagem** responsável é por carregar a imagem do arquivo local, ou capturar um frame do dispositivo de captura, ou reinicia o vídeo do dispositivo de captura.O comportamento dinâmico do botão é afetado pela a opção da origem da imagem e estado da aplicação. Por exemplo:
- Ao selecionar <ins>Arquivo</ins> como origem da imagem, o botão vai estar com o texto "*Carregar*", permite o carregamento da imagem local para vizualização da mesma e conferência antes do envio para verificação;
- Ao selecionar <ins>Câmera</ins> como origem da imagem, o botão vai estar com o texto "*Capturar*", permite captura do último frame retornado do dispositivo de captura para vizualizar da mesma e conferência antes do envio para verificação. Uma vez que um frame é capturado, o botão vai estar com o texto "*Resetar*", permite descartar o frame capturado e reiniciar o vídeo do dispositivo de captura.

O botão **Enviar** é responsável por enviar a imagem ou frame para validação no serviços de detecção de EPIs da AWS Rekognition. 

Uma vez que uma imagem é enviada para o serviços de detecção de EPIs, o retorno da validação é dividido em seções:

- A seção **Delimitadores** mostra a imagem enviada com os delimitadores (ou quadrantes) das pessoas e EPIs detectados. Sendo quadrantes amarelo para pessoas, quadrantes verdes para EPIs detectados e sendo vestidos pela pessoa, e quadrantes vermelhos para EPIs detectados, mas não sendo usados corretamente pela pessoa;
- A seção **Validação** mostra um retorno resumido do serviço, listado quais pessoas estão autorizadas, quais não estão autorizadas e quais pessoas/casos foram indeterminaveis;
- A seção **Retorno Bruto** mostra o valor bruto retornado pelo serviço, com mais detalhes técnicos e informações como identificadores, marcadores e as porcentagens de confiança.

> Nota: Aplicação de Teste mostra até 15 pessoas
> Para fins de exemplificação de mal uso de EPIs, podemos dar dois exemplos práticos. Uma pessoa utilizando máscara, mas a máscara não cobre o nariz, apenas a boca. A outra situação seria a presença de um capacete na imagem, mas o capacete está na mão da pessoa.

- Campo de texto para logs