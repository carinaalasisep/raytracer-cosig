namespace RayTracer.Model
{
    using System.Numerics;

    public class Hit
    {
        public bool Found { get; set; }

        public Material Material { get; set; }

        public Vector3 IntersectionPoint { get; set; }

        public Vector3 IntersectionNormal { get; set; }

        public double Distance { get; set; } //t

        public double MinDistance { get; set; } //tmin
    }
}
