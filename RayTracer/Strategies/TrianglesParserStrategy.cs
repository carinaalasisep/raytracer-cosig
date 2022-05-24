namespace RayTracer.Strategies
{
    using System.Collections.Generic;
    using System.Numerics;
    using RayTracer.Model.Objects;

    public class TrianglesParserStrategy : IParserStrategy
    {
        private const string EntityType = "Triangles";

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
            var result = new List<Triangle>();
            var transformation = entity[2];

            for (int line = 3; line < entity.Length; line+=4)
            {
                var material = entity[line];
                var v0Values = entity[line + 1].Split(' ');
                var v1Values = entity[line + 2].Split(' ');
                var v2Values = entity[line + 3].Split(' ');

                var v0 = new Vector3
                {
                    X = float.Parse(v0Values[0]),
                    Y = float.Parse(v0Values[1]),
                    Z = float.Parse(v0Values[2])
                };

                var v1 = new Vector3
                {
                    X = float.Parse(v1Values[0]),
                    Y = float.Parse(v1Values[1]),
                    Z = float.Parse(v1Values[2])
                };

                var v2 = new Vector3
                {
                    X = float.Parse(v2Values[0]),
                    Y = float.Parse(v2Values[1]),
                    Z = float.Parse(v2Values[2])
                };

                var triangle = new Triangle(
                transformation: int.Parse(transformation),
                material: int.Parse(material),
                v0: v0,
                v1: v1,
                v2: v2);

                result.Add(triangle);
            }

            return new Triangles
            {
                TriangleList = result
            };
        }
    }
}
