# Descritivo da Aplicação de Teste

![Aplicação de Teste](../../imgs/Aplica%C3%A7%C3%A3o%20de%20Teste.png "Aplicação de Teste")

O conjunto de *radio button* no topo superior esquerdo serve para selecionar a origem da imagem a ser analisada, sendo ou originada de um arquivo local ou de algum dispositivo de captura (como por exemplo uma câmera).

> Nota: Os elementos de tela exclusivo para o gerenciamento de arquivos e exclusivos para gerenciamento do dispositivo de captura possuem comportamento dinâmico na tela. Ou seja, se for selecionado a opção de fonte da imagem vinda de arquivo, somente os campos focados na gestão do arquivo irão aparecer. Da mesma forma que ao selecionar a opção de fonte da imagem vinda de um dispositivo de captura, irá fazer somente os campos focados na gestão do dispositivo de captura aparecer.

O campo **Arquivo** permite a informação do caminho do arquivo a ser carregado. Junto ao campo existe um botão que permite utilizar o sistema de busca de arquivos nativo do Windows. O campo **Câmera** que permite a seleção do dispositivo de captura e o botão ao lado permite conectar e desconectar do dispositivo de captura.

> Notas: Uma vez que o dispositivo de captura for conectado com sucesso, o vídeo será visível na sessão **Imagem**;

A seção denominada como **Imagem** é onde será mostrado a imagem carregada a partir do arquivo, ou vídeo/frame do dispositivo de captura. O botão abaixo da seção **Imagem** responsável é por carregar a imagem do arquivo local, ou capturar um frame do dispositivo de captura, ou reinicia o vídeo do dispositivo de captura. O comportamento dinâmico do botão é afetado pela a opção da origem da imagem e estado da aplicação. Por exemplo:
- Ao selecionar <ins>Arquivo</ins> como origem da imagem, o botão vai estar com o texto "*Carregar*", permite o carregamento da imagem local para visualização da mesma e conferência antes do envio para verificação;
- Ao selecionar <ins>Câmera</ins> como origem da imagem, o botão vai estar com o texto "*Capturar*", permite captura do último frame retornado do dispositivo de captura para visualizar da mesma e conferência antes do envio para verificação. Uma vez que um frame é capturado, o botão vai estar com o texto "*Resetar*", permite descartar o frame capturado e reiniciar o vídeo do dispositivo de captura.

Na parte superior central da tela existe uma seção com 3 *checkbox* e um campo de texto "Confiança Mínima". Os checkbox permitem habilitar e desabilitar quais EPIs devem ser verificados e validados pelo serviço e o campo "Confiança Mínima" permite ajustar a porcentagem de confiança mínima do serviço, podendo varia de 50 a 100 (%).

O botão **Enviar** é responsável por enviar a imagem ou frame para validação no serviços de detecção de EPIs da AWS Rekognition. 

Uma vez que uma imagem é enviada para o serviços de detecção de EPIs, o retorno da validação é dividido em 3 seções:

- A seção **Delimitadores** mostra a imagem enviada com os delimitadores (ou quadrantes) das pessoas e EPIs detectados. Sendo quadrantes amarelo para pessoas, quadrantes verdes para EPIs detectados e sendo vestidos pela pessoa, e quadrantes vermelhos para EPIs detectados, mas não sendo usados corretamente pela pessoa. Dentro dos quadrantes amarelos também será impresso um número que serve como identificador da pessoa, que pode ser utilizado para avaliar melhor os retornos na seção **Validação** e na seção **Retorno Bruto**;
- A seção **Validação** mostra um retorno resumido do serviço, listado quais pessoas estão autorizadas, quais não estão autorizadas e quais pessoas/casos foram indetermináveis. Está validação resumida segue como base a confiaça mínima requisitada e quais EPIs foram selecionados como requeridos;
- A seção **Retorno Bruto** mostra uma versão simplificada e mais legivel do JSON de retorno, com mais detalhes técnicos e, informações como identificadores, marcadores e as porcentagens de confiança.

> Nota: 
> - Aplicação de Teste consegue processar até 15 pessoas na imagem.
> - Para fins de exemplificação de mal uso de EPIs, podemos dar dois exemplos práticos: Uma pessoa utilizando máscara, mas a máscara não cobre o nariz, apenas a boca. A outra situação seria a presença de um capacete na imagem, mas o capacete está na mão da pessoa.

No canto superior direito existe um campo de texto para escrever logs da aplicação. Aqui algumas informações curtas são expostas para indicar qual ação o programa está executando, se foi bem sucedida ou se algum erro aconteceu.

> Nota: A aplicação possivelmente não cobre todo e qualquer uso erroneo do usuário, logo existem algumas combinações de ações que podém gerar efeitos negativos ou quebra do programa.