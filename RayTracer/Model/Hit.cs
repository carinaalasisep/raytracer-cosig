namespace RayTracer.Model
{
    using System.Numerics;

    class Hit
    {
        public bool Found;

        public Material Material;

        public Vector3 Point;

        public Vector3 Normal;

        public double Distance; //t

        public double MinDistance; //tmin
    }
}
