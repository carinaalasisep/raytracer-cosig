namespace RayTracer.Service
{
    using System.Drawing;
    using System.Numerics;
    using RayTracer.Model;

    public class RaysService
    {
        public void CalculatePrimaryRays(Camera cameraScene, RayTracer.Model.Image imageScene, double projectionHeight, double projectionWidth, double pixelDimension)
        {
            var origin = new Vector3
            {
                X = (float)0.0,
                Y = (float)0.0,
                Z = (float)cameraScene.Distance
            };
            var recursiveLevel = 1;

            var pixel = new Color3[imageScene.Vertical, imageScene.Horizontal];
            // Apoio ao debugging - raios primários
            //var directionArray = new Vector3[imageScene.Vertical, imageScene.Horizontal];
            var bitmap = new Bitmap(imageScene.Vertical, imageScene.Horizontal);
            var value = (700 / (int)projectionWidth) * 10;

            // ciclo para percorrer todas as linhas da imagem
            for (var verticalPos = 0; verticalPos < imageScene.Vertical; verticalPos++)
            {
                // ciclo para percorrer todas as colunas (píxeis) da linha j 
                for (var horizontalPos = 0; horizontalPos < imageScene.Horizontal; horizontalPos++)
                {
                    pixel[verticalPos, horizontalPos] = new Color3();

                    // calculem as coordenadas P.x, P.y e P.z do centro do píxel[i][j]
                    var px = (float)((horizontalPos + 0.5) * pixelDimension - projectionWidth / 2.0); // a origem do sistema de eixos coordenados estálocalizada no centro do plano de projecção, mas o píxel[0][0] está localizado no cantosuperior esquerdo da imagem; daí a subtracção de width / 2.0 à coordenada x do píxel
                    var py = (float)(-(verticalPos + 0.5) * pixelDimension + projectionHeight / 2.0); // a origem do sistema de eixos coordenados está            localizada no centro do plano de projecção, mas o píxel[0][0] está localizado no cantosuperior esquerdo da imagem; daí a adição de height / 2.0 à coordenada y do píxel
                    var pz = 0.0f; // o plano de projecção é o plano z = 0.0;sabendo que o raio que passa pelo centro do píxel[i][j] (o ponto (P.x, P.y, P.z)) tem            origem na posição da câmara(o ponto(0.0, 0.0, distance)), calculem o vector directionque define a direcção do referido raiodirection = new Vector3(P.x - 0.0, P.y - 0.0, P.z - distance); // ou seja, direction = new
                    var direction = new Vector3
                    {
                        X = px, Y = py, Z = pz - (float)cameraScene.Distance
                    };

                    // normalizem o vector direcção do raio (é importante que este vector seja mantido
                    //sempre normalizado)
                    var directionNormal = Vector3.Normalize(direction);

                    // Apoio ao debugging - raios primários
                    //directionArray[verticalPos, horizontalPos] = directionNormal; 

                    // agora que já conhecem quer a origem, quer a direcção do raio, deverão construí-lo
                    var ray = new Ray
                    {
                        Origin = origin,
                        Direction = directionNormal
                    };
                    // uma vez construído o raio, deverão invocar a função traceRay(), a qual irá            acompanhar recursivamente o percurso do referido raio; quando regressar, esta
                    //função deverá retornar uma cor color
                    Color3 color = this.TraceRay(ray, recursiveLevel); // em que ray designa o raio a ser acompanhado e rec um  inteiro que contém o nível máximo de recursividade
                                                                       // limitem as componentes primárias (R, G e B) da cor color. Se alguma delas for < 0.0,            façam - na = 0.0(isto nunca deverá acontecer); se alguma delas for > 1.0, façam - na =             1.0(isto poderá e irá acontecer, pois alguns dos materiais definidos no ficheiro             descritivo da cena 3D reflectem(e / ou refractam) mais luz do que a luz que sobre eles              incide
                    color.checkRange();

                    // convertam as componentes primárias da cor color num formato compatível com o
                    //do contentor da imagem; assumindo que estão a trabalhar com 32 bits / píxel(8 bits
                    //para a componente R, 8 para G, 8 para B e 8 para A), a conversão a realizar consistirá
                    //em multiplicar cada componente por 255.0 e convertê-la para um inteiro ou um byte
                    //sem sinal.Por último, deverão colorir o píxel[i][j] com a cor color assim convertida
                    pixel[verticalPos, horizontalPos].Red = (int)255.0 * color.Red;
                    pixel[verticalPos, horizontalPos].Green = (int)255.0 * color.Green;
                    pixel[verticalPos, horizontalPos].Blue = (int)255.0 * color.Blue;
                }
            }
        }

        private Color3 TraceRay(Ray ray, int recursiveLevel)
        {
            return new Color3 { Red = 0.4, Green = 0.5, Blue = 0.6 };
        }
    }
}
