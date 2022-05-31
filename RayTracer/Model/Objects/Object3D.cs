namespace RayTracer.Model
{
    public abstract class Object3D
    {
        public int Transformation { get; set; }

        public int Material { get; set; }

        public abstract bool Intersect(Ray ray, Hit hit);
    }
}
