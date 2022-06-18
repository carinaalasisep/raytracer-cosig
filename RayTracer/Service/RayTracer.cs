namespace RayTracer.Service
{
    using System;
    using System.Drawing;
    using System.Numerics;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using global::RayTracer.Model;
    using global::RayTracer.Strategies;

    public class RayTracer
    {
        public void CalculatePrimaryRays(
            Camera cameraScene,
            global::RayTracer.Model.Image imageScene,
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

            var pixel = new Color3[imageScene.Vertical, imageScene.Horizontal];

            var bitmap = new Bitmap(imageScene.Vertical, imageScene.Horizontal);

            Parallel.For(0, imageScene.Vertical, new ParallelOptions { MaxDegreeOfParallelism = 10 }, verticalPos =>
            {
                for (int horizontalPos = 0; horizontalPos < imageScene.Horizontal; horizontalPos++)
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

                    // agora que já conhecem quer a origem, quer a direcção do raio, deverão construí-lo
                    var ray = new Ray
                    {
                        Origin = origin,
                        Direction = directionNormal
                    };

                    // uma vez construído o raio, deverão invocar a função traceRay(), a qual irá acompanhar recursivamente o percurso do referido raio; quando regressar, esta
                    //função deverá retornar uma cor color// em que ray designa o raio a ser acompanhado e rec um  inteiro que contém o nível máximo de recursividade
                    // limitem as componentes primárias (R, G e B) da cor color. Se alguma delas for < 0.0,
                    // façam - na = 0.0(isto nunca deverá acontecer); se alguma delas for > 1.0, façam - na =
                    // 1.0(isto poderá e irá acontecer, pois alguns dos materiais definidos no ficheiro
                    // descritivo da cena 3D reflectem(e / ou refractam) mais luz do que a luz que sobre eles incidem
                    var color = this.TraceRay(ray, context, context.RecursiveLevel); 
                    color.CheckRange();

                    // convertam as componentes primárias da cor color num formato compatível com o
                    //do contentor da imagem; assumindo que estão a trabalhar com 32 bits / píxel(8 bits
                    //para a componente R, 8 para G, 8 para B e 8 para A), a conversão a realizar consistirá
                    //em multiplicar cada componente por 255.0 e convertê-la para um inteiro ou um byte
                    //sem sinal.Por último, deverão colorir o píxel[i][j] com a cor color assim convertida
                    pixel[verticalPos, horizontalPos].Red = (int)255.0 * color.Red;
                    pixel[verticalPos, horizontalPos].Green = (int)255.0 * color.Green;
                    pixel[verticalPos, horizontalPos].Blue = (int)255.0 * color.Blue;
                }
            });

            SetBitMapPixelData(imageScene, pixel, bitmap);

            pictureBox.Image = bitmap;
        }

        private static void SetBitMapPixelData(Model.Image imageScene, Color3[,] pixel, Bitmap bitmap)
        {
            for (int verticalPos = 0; verticalPos < imageScene.Vertical; verticalPos++)
            {
                for (int horizontalPos = 0; horizontalPos < imageScene.Horizontal; horizontalPos++)
                {
                    bitmap.SetPixel(verticalPos, horizontalPos, Color.FromArgb((int)pixel[verticalPos, horizontalPos].Red, (int)pixel[verticalPos, horizontalPos].Green, (int)pixel[verticalPos, horizontalPos].Blue));
                }
            }
        }

        private Color3 TraceRay(Ray ray, ObjectContext context, int recursiveLevel)
        {
            Hit hit = new Hit();

            foreach (var obj in context.Objects)
            {
                var origin = ray.Origin;
                var transformatedRay = new Ray { Origin = ray.Origin, Direction = ray.Direction };
                obj.WorldCoordToObjCoord(transformatedRay, obj.Transformation);
                obj.Intersect(transformatedRay, hit, origin);
            }

            var color = new Color3 { Red = 0, Green = 0, Blue = 0 }; // inicialização

            if (hit.Found)
            {
                foreach (var light in context.LightsScene)
                {
                    color = this.GetPixelColor(context, hit, color, light, ray, recursiveLevel);
                }

                return new Color3 { Red = color.Red/context.LightsScene.Count, Green = color.Green/context.LightsScene.Count, Blue = color.Blue/context.LightsScene.Count };
            }
            
            return new Color3 { Red = context.ImageScene.Color3.Red, Green = context.ImageScene.Color3.Green, Blue = context.ImageScene.Color3.Blue };
        }

        private Color3 GetPixelColor(ObjectContext context, Hit hit, Color3 color, Light light, Ray ray, int recursiveLevel)
        {
            var materialScene = context.MaterialsScene;
            var materialHit = materialScene[hit.Material];
            var materialHitColor = materialScene[hit.Material].Color;
            var lightColor = light.Color;
            var environment = context.IsEnvironmentEnabled ? materialHit.Environment : 1;

            // cálculo da componente de reflexão ambiente com origem na fonte de luz light
            color = new Color3
            {
                Red = color.Red + lightColor.Red * materialHitColor.Red * environment,
                Green = color.Green + lightColor.Green * materialHitColor.Green * environment,
                Blue = color.Blue + lightColor.Blue * materialHitColor.Blue * environment
            };

            // cálculo da componente de reflexão difusa com origem na fonte de luz light
            // comecem por construir o vector l que une o ponto de intersecção ao ponto
            // correspondente à posição da fonte de luz light
            var l = Vector3.Subtract(this.ApplyTransformation(new Vector3(0, 0, 0), light.Transformation), hit.IntersectionPoint);

            var tLight = l.Length();

            // normalizem o vector l

            l = Vector3.Normalize(l);

            // calculem o co-seno do ângulo de incidência da luz. Este é igual ao produto
            //escalar do vector normal pelo vector l(assumindo que ambos os vectores são
            //unitários)
            var cosTheta = Vector3.Dot(hit.IntersectionNormal, l);

            // calculem a componente de reflexão difusa e adicionem a cor resultante à cor
            //  color.Tenham, no entanto, em consideração que só interessa calcular esta
            //componente se o ângulo de incidência θ for inferior a 90.0° (por outras palavras,
            //se cosTheta > 0.0).Um ângulo de incidência superior àquele valor significa que o
            //raio luminoso está a incidir no lado de trás da superfície do objecto intersectado

            if (cosTheta > Utils.Constants.Epsilon)
            {
                color = GetShadowsAndDiffuseLighting(context, hit, color, lightColor, materialHit, materialHitColor, l, tLight, cosTheta);
            }

            if (recursiveLevel > 0)
            {
                recursiveLevel--;

                // comecem por calcular o co-seno do ângulo do raio incidente
                var cosThetaV = -Vector3.Dot(ray.Direction, hit.IntersectionNormal);

                if (materialHit.Specular > 0.0 && context.IsReflectionEnabled)
                {
                    color = this.GetReflectionColor(context, hit, color, materialHit, materialHitColor, ray, recursiveLevel, cosThetaV);
                }

                if (materialHit.Refraction > 0.0 && context.IsRefractionEnabled)
                {
                    color = this.GetRefractionLight(context, hit, color, materialHit, materialHitColor, ray, recursiveLevel, cosThetaV);
                }
            }

            return color;
        }

        private Color3 GetRefractionLight(
            ObjectContext context,
            Hit hit,
            Color3 color,
            Material materialHit,
            Color3 materialHitColor,
            Ray ray,
            int recursiveLevel, 
            float cosThetaV)
        {
            // o material constituinte do objecto intersectado refracta a luz
            // para calcular a razão entre os índices de refracção e o co-seno do ângulo do
            //raio refractado, comecem por admitir que o raio luminoso está a transitar do
            //ar para o meio constituinte do objecto intersectado
            var eta = 1.0 / materialHit.RefractionIndex;
            var cosThetaR = Math.Sqrt(1.0 - eta * eta * (1.0 - cosThetaV * cosThetaV));

            // se o raio luminoso estiver a transitar do meio constituinte do objecto
            // intersectado para o ar, invertam a razão entre os índices de refracção e
            //troquem o sinal do co - seno do ângulo do raio refractado
            if (cosThetaV < 0.0)
            {
                eta = materialHit.RefractionIndex;
                cosThetaR = -cosThetaR;
            }

            // calculem a direcção do raio refractado:
            var r = new Vector3(
                (float)(ray.Direction.X + (eta * cosThetaV - cosThetaR) * hit.IntersectionNormal.X),
                (float)(ray.Direction.Y + (eta * cosThetaV - cosThetaR) * hit.IntersectionNormal.Y),
                (float)(ray.Direction.Z + (eta * cosThetaV - cosThetaR) * hit.IntersectionNormal.Z));

            // normalizem o vector
            var rNormal = Vector3.Normalize(r);

            // construam o raio refractado que tem origem no ponto de intersecção e a
            //direcção do vector r
            var refractedRay = new Ray { Origin = hit.IntersectionPoint + (float)Utils.Constants.Epsilon * rNormal, Direction = rNormal };

            // uma vez construído o raio, deverão invocar a função traceRay(), a qual irá
            //acompanhar recursivamente o percurso do referido raio; quando regressar, a
            //cor retornada por esta função deverá ser usada para calcular a componente
            //de refracção, a qual será adicionada à cor color

            var recursiveColor = this.TraceRay(refractedRay, context, recursiveLevel);
            color = new Color3
            {
                Red = color.Red + materialHitColor.Red * materialHit.Refraction * recursiveColor.Red,
                Green = color.Green + materialHitColor.Green * materialHit.Refraction * recursiveColor.Green,
                Blue = color.Blue + materialHitColor.Blue * materialHit.Refraction * recursiveColor.Blue,
            };
            return color;
        }

        private Color3 GetReflectionColor(
            ObjectContext context,
            Hit hit,
            Color3 color,
            Material materialHit,
            Color3 materialHitColor,
            Ray ray,
            int recursiveLevel,
            float cosThetaV)
        {
            // o material constituinte do objecto
            //intersectado reflecte a luz especular
            // calculem a direcção do raio reflectido
            var r = new Vector3(
                (float)(ray.Direction.X + 2.0 * cosThetaV * hit.IntersectionNormal.X),
                (float)(ray.Direction.Y + 2.0 * cosThetaV * hit.IntersectionNormal.Y),
                (float)(ray.Direction.Z + 2.0 * cosThetaV * hit.IntersectionNormal.Z));

            // normalizem o vector
            var rNormal = Vector3.Normalize(r);
            // construam o raio reflectido que tem origem no ponto de intersecção e a
            //direcção do vector r
            var reflectedRay = new Ray
            {
                Origin = hit.IntersectionPoint + (float)Utils.Constants.Epsilon * rNormal,
                Direction = rNormal
            };

            // uma vez construído o raio, deverão invocar a função traceRay(), a qual irá
            //acompanhar recursivamente o percurso do referido raio; quando regressar, a
            //cor retornada por esta função deverá ser usada para calcular a componente
            //de reflexão especular, a qual será adicionada à cor color

            var recursiveColor = this.TraceRay(reflectedRay, context, recursiveLevel);
            color = new Color3
            {
                Red = color.Red + materialHitColor.Red * materialHit.Specular * recursiveColor.Red,
                Green = color.Green + materialHitColor.Green * materialHit.Specular * recursiveColor.Green,
                Blue = color.Blue + materialHitColor.Blue * materialHit.Specular * recursiveColor.Blue,
            };
            return color;
        }

        private static Color3 GetShadowsAndDiffuseLighting(
            ObjectContext context,
            Hit hit,
            Color3 color,
            Color3 lightColor,
            Material materialHit,
            Color3 materialHitColor,
            Vector3 l,
            float tLight,
            float cosTheta)
        {
            var shadowRay = new Ray { Origin = hit.IntersectionPoint + (float)Utils.Constants.Epsilon * hit.IntersectionNormal, Direction = l };
            var shadowHit = new Hit();
            shadowHit.MinDistance = tLight;

            foreach (var object3d in context.Objects)
            {
                shadowHit.Found = false;
                var origin = shadowRay.Origin;
                var shadowRayTransformed = new Ray { Origin = shadowRay.Origin, Direction = shadowRay.Direction };
                object3d.WorldCoordToObjCoord(shadowRayTransformed, object3d.Transformation);
                object3d.Intersect(shadowRayTransformed, shadowHit, origin);

                if (shadowHit.Found)
                {
                    break;
                }
            }

            if (!shadowHit.Found)
            {
                //diffuse light
                var difuseLight = context.IsDiffuseReflectionEnabled ? materialHit.Diffuse : 1;
                color = new Color3
                {
                    Red = color.Red + lightColor.Red * materialHitColor.Red * difuseLight * cosTheta,
                    Green = color.Green + lightColor.Green * materialHitColor.Green * difuseLight * cosTheta,
                    Blue = color.Blue + lightColor.Blue * materialHitColor.Blue * difuseLight * cosTheta
                };
            }

            return color;
        }

        Vector3 ApplyTransformation(Vector3 point, Transformation transformation)
        {
            float[] pointA = { point.X, point.Y, point.Z, 1.0f };
            float[] pointB = Utils.Helper.Multiply1(pointA, transformation.Matrix);

            var newPoint = new Vector3(pointB[0] / pointB[3], pointB[1] / pointB[3], pointB[2] / pointB[3]);

            return newPoint;
        }
    }
}