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
                        result.Translate(new Vector3(float.Parse(elementsArray[1], CultureInfo.InvariantCulture.NumberFormat),
                        (float.Parse(elementsArray[2], CultureInfo.InvariantCulture.NumberFormat)),
                        (float.Parse(elementsArray[3], CultureInfo.InvariantCulture.NumberFormat))));
                    }

                    if (elementsArray.First() == "Rx")
                    {
                        result.RotateX(float.Parse(elementsArray[1], CultureInfo.InvariantCulture.NumberFormat));
                    }

                    if (elementsArray.First() == "Ry")
                    {
                        result.RotateY(float.Parse(elementsArray[1], CultureInfo.InvariantCulture.NumberFormat));
                    }

                    if (elementsArray.First() == "Rz")
                    {
                        result.RotateZ(float.Parse(elementsArray[1], CultureInfo.InvariantCulture.NumberFormat));
                    }

                    if (elementsArray.First() == "S")
                    {
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
