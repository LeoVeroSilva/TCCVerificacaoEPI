# Descritivo da Aplicação Final
![Aplicação Final](../../imgs/Aplica%C3%A7%C3%A3o%20Final.png "Aplicação Final")

> Nota: Está aplicação só funciona adequadamente se o um dispositivo de captura já se encontra disponível para uso.

> Muitas configurações podém ser feitas no programa, basta manipular as **Variaveis de ajuste** no topo do programa principal. É possível configurar parâmetros como:
> - Confiança Mínima (padrão é 80%);
> - Intervalo para captura (padrão é 3 segundos);
> - Quais EPIs devem ser validados (padrão é validar capacete, máscara e luvas)

A seção denominada como **Imagem** é onde será mostrado o vídeo/frame do dispositivo de captura. O botão abaixo da seção **Imagem** responsável é por capturar um frame do dispositivo de captura, ou reinicia o vídeo do dispositivo de captura para uma nova captura. Em um primeiro momento, o botão vai estar com o texto "*Capturar*" e clicando no mesmo, uma contagem de 3 segundos iniciára, no final da contagem o programa irá capturar um frame do dispositivo de captura e enviar para o serviço de validação. O retorno será mostrado na seção **Imagem** e o botão passará a mostra o texto "*Resetar*. Pressionando o botão neste estágio fará o descarte da validação atual e reiniciar o vídeo do dispositivo de captura para uma nova captura de frame.

Uma vez que uma imagem é enviada para o serviços de detecção de EPIs, o retorno da validação é mostrado da seguinte forma:

- A seção **Imagem** mostra a imagem enviada com os delimitadores (ou quadrantes) das pessoas e EPIs detectados. Sendo quadrantes amarelo para pessoas, quadrantes verdes para EPIs detectados e sendo vestidos pela pessoa, e quadrantes vermelhos para EPIs detectados, mas não sendo usados corretamente pela pessoa. Dentro dos quadrantes amarelos também será impresso um número que serve como identificador da pessoa que pode ser utilizado para avaliar melhor o retorno na na seção **Validação** e na seção **Retorno Bruto**;
- A seção **Validação** mostra um retorno resumido do serviço, listado quais pessoas estão autorizadas, quais não estão autorizadas e quais pessoas/casos foram indetermináveis;
- A seção **Retorno Bruto** mostra o valor bruto retornado pelo serviço, com mais detalhes técnicos e informações como identificadores, marcadores e as porcentagens de confiança.

> Caso mais de uma pessoa for identificada na foto o programa denomina a situação como indeteminável, requisitando uma imagem adequada ou interveção externa.

No canto superior direito existe um campo de texto para escrever logs da aplicação. Aqui algumas informações curtas são expostas para indicar qual ação o programa está executando, se foi bem sucedida ou se algum erro aconteceu.

> Nota: A aplicação não cobre todo e qualquer uso erroneou, existem algumas combinações de ações que podém gerar efeitos negativos ou quebra do programa.
