namespace RayTracer.Strategies
{
    using System.Collections.Generic;
    using System.Numerics;
    using RayTracer.Model.Objects;

    public class TrianglesParserStrategy : IParserStrategy
    {
        private const string EntityType = "Image";

        public void AddObjectToContext(ObjectContext context, string[] entity)
        {
            var triangles = this.BuildTriangles(entity);

            context.TrianglesScene.Add(triangles);
        }

        public bool CanHandle(string entityName)
        {
            return entityName == EntityType;
        }

        private Triangles BuildTriangles(string[] entity)
        {
            var triangleData = new List<string>();
            var transformation = entity[2];

            for (int line = 3; line < entity.Length; line++)
            { 
                triangleData.AddRange(entity[line].Split(' '));
            }

            var v0 = new Vector3
            {
                X = float.Parse(triangleData[1]),
                Y = float.Parse(triangleData[2]),
                Z = float.Parse(triangleData[3])
            };

            var v1 = new Vector3
            {
                X = float.Parse(triangleData[4]),
                Y = float.Parse(triangleData[5]),
                Z = float.Parse(triangleData[6])
            };

            var v2 = new Vector3
            {
                X = float.Parse(triangleData[7]),
                Y = float.Parse(triangleData[8]),
                Z = float.Parse(triangleData[9])
            };

            return new Triangles(
                transformation: int.Parse(transformation),
                material: int.Parse(triangleData[0]),
                v0: v0,
                v1: v1,
                v2: v2);
        }
    }
}
