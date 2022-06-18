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

        private readonly double kEpsilon = 1e-8;

        public Triangle(int transformation, int material, Vector3 v0, Vector3 v1, Vector3 v2)
        {
            this.TransformationIndex = transformation;
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

        public override bool Intersect(Ray ray, Hit hit, Vector3 origin)
        {
            // Taken from https://stackoverflow.com/questions/54617181/problem-in-rendering-triangle-mesh-with-reflections-in-webgl-and-opengles

            var v0v1 = Vector3.Subtract(this.P1, this.P0);
            var v0v2 = Vector3.Subtract(this.P2, this.P0);

            var vectorP = Vector3.Cross(ray.Direction, v0v2);

            var det = Vector3.Dot(v0v1, vectorP);

            // If the determinant is negative the triangle is backfacing
            // If the determinant is close to 0, the ray misses the triangle
            if (det < this.kEpsilon)
            {
                return false;
            }

            // Ray and Triangle are parallel if det is close to 0
            if (Math.Abs(det) < this.kEpsilon)
            {
                return false;
            }

            var invertedDet = 1 / det;

            var vectorT = Vector3.Subtract(ray.Origin, this.P0);
            var u = Vector3.Dot(vectorT, vectorP) * invertedDet;

            if (u < 0 || u > 1)
            {
                return false;
            }

            var vectorQ = Vector3.Cross(vectorT, v0v1);
            var v = Vector3.Dot(ray.Direction, vectorQ) * invertedDet;

            if (v < 0 || u + v > 1)
            {
                return false;
            }

            var v0v2Q = Vector3.Dot(v0v2, vectorQ);

            // If v0v2Q < 0 the intersect is behind the ray origin
            if (v0v2Q < 0)
            {
                return false;
            }

            var distance = Vector3.Dot(v0v2, vectorQ) * invertedDet;

            var intersectionPoint = Vector3.Add(ray.Origin, ray.Direction * distance);

            this.ObjectCoordToWorldCoord(hit, intersectionPoint, origin);

            if (distance > this.kEpsilon)
            {
                hit.MinDistance = hit.Distance;
                hit.Found = true;
                hit.Material = this.Material;
                Vector3 normal = Vector3.Cross(v0v1, v0v2);

                hit.IntersectionNormal = this.ConvertNormalToWorld(Vector3.Normalize(normal));

                return true;
            }

            return false;
        }
    }
}
