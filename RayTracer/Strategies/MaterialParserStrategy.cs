namespace RayTracer.Strategies
{
    using System.Collections.Generic;
    using RayTracer.Model;

    public class MaterialParserStrategy : IParserStrategy
    {
        private const string EntityType = "Material";

        public void AddObjectToContext(ObjectContext context, string[] entity)
        {
            var material = this.BuildMaterial(entity);

            context.MaterialsScene.Add(material);
        }

        public bool CanHandle(string entityName)
        {
            return entityName == EntityType;
        }

        private Material BuildMaterial(string[] entity)
        {
            var materialData = new List<string>();

            for (int line = 2; line < entity.Length; line++)
            {
                materialData.AddRange(entity[line].Split(' '));
            }

            return new Material(red: double.Parse(materialData[2]),
                green: double.Parse(materialData[3]),
                blue: double.Parse(materialData[4]),
                coefficients: new double[] { double.Parse(materialData[5]), double.Parse(materialData[6]), double.Parse(materialData[7]) });
        }
    }
}
