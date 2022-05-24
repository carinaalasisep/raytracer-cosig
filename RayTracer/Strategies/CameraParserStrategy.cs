namespace RayTracer.Strategies
{
    using RayTracer.Model;

    public class CameraParserStrategy : IParserStrategy
    {
        private const string EntityType = "Camera";

        public void AddObjectToContext(ObjectContext context, string[] entity)
        {
            var camera = this.BuildCamera(entity);

            context.CameraScene = camera;
        }

        public bool CanHandle(string entityName)
        {
            return entityName == EntityType;
        }

        private Camera BuildCamera(string[] entity)
        {
            return new Camera(
                transformation: int.Parse(entity[2]),
                distance: double.Parse(entity[3]),
                visionField: double.Parse(entity[4]));
        }
    }
}
