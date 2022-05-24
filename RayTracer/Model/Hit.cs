namespace RayTracer.Model
{
    using System.Numerics;

    class Hit
    {
        public bool Found { get; set; }

        public Material Material { get; set; }

        public Vector3 Point { get; set; }

        public Vector3 Normal { get; set; }

        public double Distance { get; set; } //t

        public double MinDistance { get; set; } //tmin
    }
}
