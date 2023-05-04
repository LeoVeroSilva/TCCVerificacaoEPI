# Descritivo da Aplicação Final
![Aplicação Final](../../imgs/Aplica%C3%A7%C3%A3o%20Final.png "Aplicação Final")

> Nota: Está aplicação só funciona adequadamente se o um dispositivo de captura (por exemplo, uma câmera) já se encontra disponível para uso.

> Muitas configurações podém ser feitas no programa, basta manipular as **Variaveis de ajuste** no topo do programa principal. É possível configurar parâmetros como:
> - Confiança Mínima (padrão é 80%);
> - Intervalo para captura (padrão é 3 segundos);
> - Quais EPIs devem ser validados (padrão é validar capacete, máscara e luvas).

A seção denominada como **Imagem** é onde será mostrado o vídeo ou frame capturado do dispositivo de captura. O botão abaixo da seção **Imagem** é responsável por capturar um frame do dispositivo de captura, ou reiniciar o vídeo do dispositivo de captura para uma nova captura. Em um primeiro momento, o botão vai estar com o texto "*Capturar*" e clicando no mesmo, uma contagem de 3 segundos (configurável) iniciára, no final da contagem o programa irá capturar um frame do dispositivo de captura e enviar para o serviço de validação. O retorno será mostrado na seção **Imagem** e o botão passará a mostrar o texto "*Resetar*. Pressionando o botão neste estágio fará o descarte da validação atual e reiniciar o vídeo do dispositivo de captura para uma nova captura de frame.

Uma vez que uma imagem é enviada para o serviços de detecção de EPIs, o retorno da validação é mostrado da seguinte forma:

- A seção **Imagem** mostra a imagem enviada com os delimitadores (ou quadrantes) da pessoa e EPIs detectados. Sendo quadrantes amarelo para pessoa, quadrantes verdes para EPIs detectados e sendo vestidos pela pessoa, e quadrantes vermelhos para EPIs detectados, mas não sendo usados corretamente pela pessoa. Dentro dos quadrantes também haverá a impressão de um número decimal que é a confiança da predição do serviço para cada quadrante.
- No canto inferior direito temos um campo de texto e uma caixa de imagem que alertam o resultado da validação. Os possiveis resultados são:
   - "Autorizado" - Indicação visual verde
   - "NÃO Autorizado" - Indicação visual vermelha
   - "Apenas uma pessoa por validação" - Indicação visual amarela
   - "Indeterminável" - Indicação visual amarela

> Notas:
> - Caso mais de uma pessoa for identificada na foto o programa denomina a situação como indeteminável, requisitando uma imagem adequada ou interveção externa.
> - A aplicação não cobre todo e qualquer uso errone, existem algumas combinações de ações que podém gerar efeitos negativos ou quebra do programa.