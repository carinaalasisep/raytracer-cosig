namespace RayTracer.Model
{
    using System.Collections.Generic;
    using System.Numerics;

    public class Light
    {
        public int TransformationIndex { get; set; }

        public Transformation Transformation { get; set; }

        public Color3 Color { get; set; }

        public Light(int transformation, double red, double green, double blue)
        {
            this.TransformationIndex = transformation;

            this.Color = new Color3
            {
                Red = red,
                Green = green,
                Blue = blue
            };
        }

        public Transformation Transform(int cameraTransform, List<Transformation> transformations)
        {
            return new Transformation(Matrix4x4.Multiply(transformations.ToArray()[cameraTransform].Matrix, transformations.ToArray()[this.TransformationIndex].Matrix));
        }
    }
}
