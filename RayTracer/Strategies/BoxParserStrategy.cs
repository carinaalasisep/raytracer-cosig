namespace RayTracer.Strategies
{
    using RayTracer.Model.Objects;

    public class BoxParserStrategy : IParserStrategy
    {
        private const string EntityType = "Box";

        public void AddObjectToContext(ObjectContext context, string[] entity)
        {
            var box = this.BuildBox(entity);

            context.BoxesScene.Add(box);
        }

        public bool CanHandle(string entityName)
        {
            return entityName == EntityType;
        }

        private Box BuildBox(string[] entity)
        {
            return new Box
            {
                Transformation = int.Parse(entity[2]),
                Material = int.Parse(entity[3])
            };
        }
    }
}
