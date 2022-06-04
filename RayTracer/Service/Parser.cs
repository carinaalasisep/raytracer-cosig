namespace RayTracer.Service
{
    using System;
    using System.Linq;
    using RayTracer.Strategies;

    public class Parser
    {
        private readonly IParserStrategy[] strategies = new IParserStrategy[]
        {
            new ImageParserStrategy(),
            new TransformationParserStrategy(),
            new MaterialParserStrategy(),
            new CameraParserStrategy(),
            new LightParserStrategy(),
            new TrianglesParserStrategy(),
            new BoxParserStrategy(),
            new SphereParserStrategy()
        };

        public void Parse(string fileContent, ObjectContext context) {
            var fileWithoutTabs = fileContent.Replace("\t", string.Empty);
            var entitySeparation = fileWithoutTabs.Split('}', StringSplitOptions.RemoveEmptyEntries);

            foreach (var item in entitySeparation)
            {
                var entity = item.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

                if(entity.FirstOrDefault() != null)
                {
                    var objectType = entity.First();
                    var selectedStrategy = this.strategies.First(s => s.CanHandle(objectType));
                    selectedStrategy.AddObjectToContext(context, entity);
                }
            }

            foreach (var light in context.LightsScene)
            {
                light.Transformation = light.Transform(context.CameraScene.Transformation, context.TransformationsScene);
            }

            foreach (var sphere in context.SpheresScene)
            {
                sphere.Transformation = sphere.transform(context.CameraScene.Transformation, sphere.TransformationIndex, context.TransformationsScene);
            }

            foreach (var box in context.BoxesScene)
            {
                box.Transformation = box.transform(context.CameraScene.Transformation, box.TransformationIndex, context.TransformationsScene);
            }

            foreach (var triangleMesh in context.TrianglesScene)
            {
                // TODO: preciso de normalizar outra vez?...
                foreach (var triangle in triangleMesh.TriangleList)
                {
                    triangle.Transformation = triangle.transform(context.CameraScene.Transformation, triangleMesh.TriangleList.First().TransformationIndex, context.TransformationsScene);
                }
            }
        }
    }
}
