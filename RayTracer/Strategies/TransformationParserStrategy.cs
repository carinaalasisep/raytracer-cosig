namespace RayTracer.Strategies
{
    using System.Linq;
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
                    }

                    if (elementsArray.First() == "Rx")
                    {
                        result.RxAngle = double.Parse(elementsArray[1]);
                    }

                    if (elementsArray.First() == "Ry")
                    {
                        result.RyAngle = double.Parse(elementsArray[1]);
                    }

                    if (elementsArray.First() == "Rz")
                    {
                        result.RzAngle = double.Parse(elementsArray[1]);
                    }

                    if (elementsArray.First() == "S")
                    {
                        result.ScaleX = double.Parse(elementsArray[1]);
                        result.ScaleY = double.Parse(elementsArray[2]);
                        result.ScaleZ = double.Parse(elementsArray[3]);
                    }
                }
            }

            return result;
        }
    }
}
