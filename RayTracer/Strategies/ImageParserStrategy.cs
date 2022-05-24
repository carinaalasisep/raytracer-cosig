namespace RayTracer.Strategies
{
    using System.Collections.Generic;
    using RayTracer.Model;

    public class ImageParserStrategy : IParserStrategy
    {
        private const string EntityType = "Image";

        public void AddObjectToContext(ObjectContext context, string[] entity)
        {
            var image = this.BuildImage(entity);

            context.ImageScene = image;
        }

        public bool CanHandle(string entityName)
        {
            return entityName == EntityType;
        }

        private Image BuildImage(string[] entity)
        {
            var imageData = new List<string>();

            for (int line = 2; line < entity.Length; line++)
            {
                imageData.AddRange(entity[line].Split(' '));
            }

            return new Image(
                horizontal: int.Parse(imageData[0]),
                vertical: int.Parse(imageData[1]),
                red: double.Parse(imageData[2]),
                green: double.Parse(imageData[3]), 
                blue: double.Parse(imageData[4]));
        }
    }
}
