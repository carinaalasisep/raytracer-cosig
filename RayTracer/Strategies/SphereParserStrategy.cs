namespace RayTracer.Strategies
{
    using RayTracer.Model.Objects;

    public class SphereParserStrategy : IParserStrategy
    {
        private const string EntityType = "Box";

        public void AddObjectToContext(ObjectContext context, string[] entity)
        {
            var sphere = this.BuildSphere(entity);

            context.SpheresScene.Add(sphere);
        }

        public bool CanHandle(string entityName)
        {
            return entityName == EntityType;
        }

        private Sphere BuildSphere(string[] entity)
        {
            return new Sphere
            {
                Transformation = int.Parse(entity[3]),
                Material = int.Parse(entity[4])
            };
        }
    }
}
