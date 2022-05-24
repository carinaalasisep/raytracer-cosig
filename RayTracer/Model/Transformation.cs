namespace RayTracer.Model
{
    using System.Numerics;

    public class Transformation
    {
        public double TranslationX { get; set; }
        public double TranslationY { get; set; }
        public double TranslationZ { get; set; }

        public double RxAngle { get; set; }

        public double RyAngle { get; set; }

        public double RzAngle { get; set; }

        public double ScaleX { get; set; }
        public double ScaleY { get; set; }
        public double ScaleZ { get; set; }

        public Matrix4x4 Matrix { get; set; }
    }
}
