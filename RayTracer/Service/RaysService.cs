namespace RayTracer.Service
{
    using System.Drawing;
    using System.Numerics;
    using System.Windows.Forms;
    using RayTracer.Model;
    using RayTracer.Strategies;

    public class RaysService
    {
        public void CalculatePrimaryRays(
            Camera cameraScene,
            RayTracer.Model.Image imageScene,
            double projectionHeight,
            double projectionWidth,
            double pixelDimension,
            PictureBox pictureBox,
            ObjectContext context)
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
                    var px = (float)((verticalPos + 0.5) * pixelDimension - projectionWidth / 2.0); // a origem do sistema de eixos coordenados estálocalizada no centro do plano de projecção, mas o píxel[0][0] está localizado no cantosuperior esquerdo da imagem; daí a subtracção de width / 2.0 à coordenada x do píxel
                    var py = (float)(-(horizontalPos + 0.5) * pixelDimension + projectionHeight / 2.0); // a origem do sistema de eixos coordenados está            localizada no centro do plano de projecção, mas o píxel[0][0] está localizado no cantosuperior esquerdo da imagem; daí a adição de height / 2.0 à coordenada y do píxel
                    var pz = 0.0f; // o plano de projecção é o plano z = 0.0;sabendo que o raio que passa pelo centro do píxel[i][j] (o ponto (P.x, P.y, P.z)) tem            origem na posição da câmara(o ponto(0.0, 0.0, distance)), calculem o vector directionque define a direcção do referido raiodirection = new Vector3(P.x - 0.0, P.y - 0.0, P.z - distance); // ou seja, direction = new
                    var direction = new Vector3
                    {
                        X = px,
                        Y = py,
                        Z = pz - (float)cameraScene.Distance
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
                    var color = this.TraceRay(ray, recursiveLevel, context); // em que ray designa o raio a ser acompanhado e rec um  inteiro que contém o nível máximo de recursividade
                                                                             // limitem as componentes primárias (R, G e B) da cor color. Se alguma delas for < 0.0,            façam - na = 0.0(isto nunca deverá acontecer); se alguma delas for > 1.0, façam - na =             1.0(isto poderá e irá acontecer, pois alguns dos materiais definidos no ficheiro             descritivo da cena 3D reflectem(e / ou refractam) mais luz do que a luz que sobre eles              incide
                    color.CheckRange();

                    // convertam as componentes primárias da cor color num formato compatível com o
                    //do contentor da imagem; assumindo que estão a trabalhar com 32 bits / píxel(8 bits
                    //para a componente R, 8 para G, 8 para B e 8 para A), a conversão a realizar consistirá
                    //em multiplicar cada componente por 255.0 e convertê-la para um inteiro ou um byte
                    //sem sinal.Por último, deverão colorir o píxel[i][j] com a cor color assim convertida
                    pixel[verticalPos, horizontalPos].Red = (int)255.0 * color.Red;
                    pixel[verticalPos, horizontalPos].Green = (int)255.0 * color.Green;
                    pixel[verticalPos, horizontalPos].Blue = (int)255.0 * color.Blue;
                    bitmap.SetPixel(verticalPos, horizontalPos, Color.FromArgb((int)pixel[verticalPos, horizontalPos].Red, (int)pixel[verticalPos, horizontalPos].Green, (int)pixel[verticalPos, horizontalPos].Blue));
                }
            }

            pictureBox.Image = bitmap;
        }

        private Color3 TraceRay(Ray ray, int recursiveLevel, ObjectContext context)
        {
            Hit hit = new Hit();

            foreach (var obj in context.Objects)
            {
                var transformatedRay = new Ray { Origin = ray.Origin, Direction = ray.Direction };
                obj.WorldCoordToObjCoord(transformatedRay, obj.Transformation);
                obj.Intersect(transformatedRay, hit);
            }

            var color = new Color3 { Red = context.ImageScene.Color3.Red, Green = context.ImageScene.Color3.Green, Blue = context.ImageScene.Color3.Blue };

            if (hit.Found)
            {
                foreach (var light in context.LightsScene)
                {
                    var lightColor = light.Color;
                    var materialScene = context.MaterialsScene;
                    var materialHit = materialScene[hit.Material];
                    var materialHitColor = materialScene[hit.Material].Color;

                    color = new Color3
                    {
                        Red = color.Red + (lightColor.Red * materialHitColor.Red) * materialHit.Environment,
                        Green = color.Green + (lightColor.Green * materialHitColor.Green) * materialHit.Environment,
                        Blue = color.Blue + (lightColor.Blue * materialHitColor.Blue) * materialHit.Environment
                    };

                    Vector3 l = Vector3.Subtract(this.applyTransformation(new Vector3(0, 0, 0), light.Transformation), hit.IntersectionPoint);

                    float tLight = l.Length();

                    l = Vector3.Normalize(l);

                    float cosTheta = Vector3.Dot(hit.IntersectionNormal, l);

                    if (cosTheta > Utils.Constants.Epsilon)
                    {

                        var shadowRay = new Ray { Origin = hit.IntersectionPoint + (float)Utils.Constants.Epsilon * hit.IntersectionNormal, Direction = l };
                        var shadowHit = new Hit();
                        shadowHit.MinDistance = tLight;

                        foreach (var object3d in context.Objects)
                        {
                            shadowHit.Found = false;
                            Ray shadowRayTransformed = new Ray { Origin = shadowRay.Origin, Direction = shadowRay.Direction };
                            object3d.WorldCoordToObjCoord(shadowRayTransformed, object3d.Transformation);
                            object3d.Intersect(shadowRayTransformed, shadowHit);

                            if (shadowHit.Found)
                            {
                                break;
                            }
                        }

                        if (!shadowHit.Found)
                        {
                            color = new Color3
                            {
                                Red = color.Red + (lightColor.Red * materialHitColor.Red) * materialHit.Environment * cosTheta,
                                Green = color.Green + (lightColor.Green * materialHitColor.Green) * materialHit.Environment * cosTheta,
                                Blue = color.Blue + (lightColor.Blue * materialHitColor.Blue) * materialHit.Environment * cosTheta
                            };
                        }
                    }
                }
            }
            
            return color;
        }
        Vector3 applyTransformation(Vector3 point, Transformation transformation)
        {

            float[] PointA = { point.X, point.Y, point.Z, 1.0f };
            float[] PointB = Utils.Helper.Multiply(PointA, transformation.Matrix);

            Vector3 newPoint = new Vector3(PointB[0] / PointB[3], PointB[1] / PointB[3], PointB[2] / PointB[3]);

            return newPoint;

        }
    }
}