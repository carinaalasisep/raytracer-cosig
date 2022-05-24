namespace RayTracer.Model.Objects
{
    using System.Numerics;

    public class Triangles : Object3D
    {
        public Vector3 V0 { get; set; }

        public Vector3 V1 { get; set; }

        public Vector3 V2 { get; set; }

        public Vector3 Normal { get; set; }

        public Triangles(int transformation, int material, Vector3 v0, Vector3 v1, Vector3 v2)
        {
            this.Transformation = transformation;
            this.Material = material;
            this.V0 = v0;
            this.V1 = v1;
            this.V2= v2;
        }
    }
}
