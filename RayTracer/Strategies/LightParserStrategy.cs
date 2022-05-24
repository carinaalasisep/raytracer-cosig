namespace RayTracer.Strategies
{
    using System.Collections.Generic;
    using RayTracer.Model;

    public class LightParserStrategy : IParserStrategy
    {
        private const string EntityType = "Light";

        public void AddObjectToContext(ObjectContext context, string[] entity)
        {
            var light = this.BuildLight(entity);

            context.LightsScene.Add(light);
        }

        public bool CanHandle(string entityName)
        {
            return entityName == EntityType;
        }

        private Light BuildLight(string[] entity)
        {
            var LightData = new List<string>();

            for (int line = 2; line < entity.Length; line++)
            {
                LightData.AddRange(entity[line].Split(' '));
            }

            return new Light(
                transformation: int.Parse(LightData[0]),
                red: double.Parse(LightData[1]),
                green: double.Parse(LightData[2]), 
                blue: double.Parse(LightData[3]));
        }
    }
}
