namespace RayTracer.Model.Objects
{
    using System;
    using System.Numerics;

    public class Triangle : Object3D
    {
        public Vector3 P0 { get; set; }

        public Vector3 P1 { get; set; }

        public Vector3 P2 { get; set; }

        public Vector3 Normal { get; set; }

        public Triangle(int transformation, int material, Vector3 v0, Vector3 v1, Vector3 v2)
        {
            this.Transformation = transformation;
            this.Material = material;
            this.P0 = v0;
            this.P1 = v1;
            this.P2 = v2;
            this.Normal = this.Normalize(v0,v1,v2);
        }

        private Vector3 Normalize(Vector3 v0, Vector3 v1, Vector3 v2)
        {
            var newV1 = Vector3.Subtract(v1, v0);
            var newV2 = Vector3.Subtract(v2, v0);

            var newX = ((newV1.Y * newV2.Z) - (newV1.Z * newV2.Y));
            var newY = ((newV1.Z * newV2.X) - (newV1.X * newV2.Z));
            var newZ = ((newV1.X * newV2.Y) - (newV1.Y * newV2.X));
            var size = Math.Sqrt(Math.Pow(newX, 2) + Math.Pow(newY, 2) + Math.Pow(newZ, 2));

            return new Vector3
            {
                X = (float)(newX / size),
                Y = (float)(newY / size),
                Z = (float)(newZ / size)
            };
        }

        public override bool Intersect(Ray ray, Hit hit)
        {
            throw new NotImplementedException();
        }
    }
}
