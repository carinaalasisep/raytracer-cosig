namespace RayTracer.Strategies
{
    using System.Collections.Generic;
    using RayTracer.Model;
    using RayTracer.Model.Objects;

    public class ObjectContext
    {
        public Image ImageScene { get; set; }
        public Camera CameraScene { get; set; }
        public List<Light> LightsScene { get; set; } = new List<Light>();
        public List<Material> MaterialsScene { get; set; } = new List<Material>();
        public List<Sphere> SpheresScene { get; set; } = new List<Sphere>();
        public List<Box> BoxesScene { get; set; } = new List<Box>();
        public List<Object3D> Objects { get; set; } = new List<Object3D>();
        public List<Transformation> TransformationsScene { get; set; } = new List<Transformation>();
    }
}