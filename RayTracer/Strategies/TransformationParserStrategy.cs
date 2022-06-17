namespace RayTracer.Strategies
{
    using System.Globalization;
    using System.Linq;
    using System.Numerics;
    using RayTracer.Model;

    public class TransformationParserStrategy : IParserStrategy
    {
        private const string EntityType = "Transformation";

        public void AddObjectToContext(ObjectContext context, string[] entity)
        {
            var transformation = this.BuildTransformation(entity);

            context.TransformationsScene.Add(transformation);
        }

        public bool CanHandle(string entityName)
        {
            return entityName == EntityType;
        }

        //TODO: fazer refactor disto
        private Transformation BuildTransformation(string[] entity)
        {
            var result = new Transformation();

            if (entity.Length > 2)
            {
                for (int line = 2; line < entity.Length; line++)
                {
                    var elementsArray = entity[line].Split(' ');
                    if (elementsArray.First() == "T")
                    {
                        result.TranslationX = double.Parse(elementsArray[1]);
                        result.TranslationY = double.Parse(elementsArray[2]);
                        result.TranslationZ = double.Parse(elementsArray[3]);
                        result.Translate(new Vector3(float.Parse(elementsArray[1], CultureInfo.InvariantCulture.NumberFormat),
                        (float.Parse(elementsArray[2], CultureInfo.InvariantCulture.NumberFormat)),
                        (float.Parse(elementsArray[3], CultureInfo.InvariantCulture.NumberFormat))));
                    }

                    if (elementsArray.First() == "Rx")
                    {
                        result.RxAngle = double.Parse(elementsArray[1]);
                        result.RotateX(float.Parse(elementsArray[1], CultureInfo.InvariantCulture.NumberFormat));
                    }

                    if (elementsArray.First() == "Ry")
                    {
                        result.RyAngle = double.Parse(elementsArray[1]);
                        result.RotateY(float.Parse(elementsArray[1], CultureInfo.InvariantCulture.NumberFormat));
                    }

                    if (elementsArray.First() == "Rz")
                    {
                        result.RzAngle = double.Parse(elementsArray[1]);
                        result.RotateZ(float.Parse(elementsArray[1], CultureInfo.InvariantCulture.NumberFormat));
                    }

                    if (elementsArray.First() == "S")
                    {
                        result.ScaleX = double.Parse(elementsArray[1]);
                        result.ScaleY = double.Parse(elementsArray[2]);
                        result.ScaleZ = double.Parse(elementsArray[3]);
                        result.Scale(new Vector3(float.Parse(elementsArray[1], CultureInfo.InvariantCulture.NumberFormat),
                        (float.Parse(elementsArray[2], CultureInfo.InvariantCulture.NumberFormat)),
                        (float.Parse(elementsArray[3], CultureInfo.InvariantCulture.NumberFormat))));
                    }
                }
            }

            return result;
        }
    }
}
