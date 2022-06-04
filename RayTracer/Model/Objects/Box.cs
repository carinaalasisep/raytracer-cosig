namespace RayTracer.Model.Objects
{
    using System;
    using System.Numerics;

    public class Box : Object3D
    {
        private Vector3 minVector;
        private Vector3 maxVector;

        public Box(int transformation, int material)
        {
            this.Transformation = transformation;
            this.Material = material;
            this.minVector = new Vector3(-0.5f, -0.5f, -0.5f);
            this.maxVector = new Vector3(0.5f, 0.5f, 0.5f);
        }

        public override bool Intersect(Ray ray, Hit hit)
        {
            var rayOrigin = ray.Origin;
            var rayDirection = ray.Direction;

            double[] origin = { rayOrigin.X, rayOrigin.Y, rayOrigin.Z };
            double[] min = { this.minVector.X, this.minVector.Y, this.minVector.Z };
            double[] max = { this.maxVector.X, this.maxVector.Y, this.maxVector.Z };
            double[] direction = { 1 / rayDirection.X, 1 / rayDirection.Y, 1 / rayDirection.Z };

            var t1 = (min[0] - origin[0]) * direction[0];
            var t2 = (max[0] - origin[0]) * direction[0];

            var tnear = Math.Min(t1, t2);
            var tfar = Math.Max(t1, t2);

            for (int i = 1; i < 3; ++i)
            {
                t1 = (min[i] - origin[i]) * direction[i];
                t2 = (max[i] - origin[i]) * direction[i];

                tnear = Math.Max(tnear, Math.Min(Math.Min(t1, t2), tfar));
                tfar = Math.Min(tfar, Math.Max(Math.Max(t1, t2), tnear));
            }

            //TODO rever isto tudo e ver se funciona...

            if (tfar > Math.Max(tnear, 0.0))
            {
                if (tnear > 0)
                {
                    hit.IntersectionPoint = new Vector3(rayOrigin.X + rayDirection.X * (float)tnear,
                            rayOrigin.Y + rayDirection.Y * (float)tnear,
                            rayOrigin.Z + rayDirection.Z * (float)tnear);
                    //TODO fazer a normal 
                    hit.IntersectionNormal = Vector3.Normalize(this.Normal(hit.IntersectionNormal));
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            if (Vector3.Dot(hit.IntersectionNormal, rayDirection) > 0)
            {
                hit.IntersectionNormal = Vector3.Negate(hit.IntersectionNormal);
                hit.Found = true;
            }

            return true;
        }

        private Vector3 Normal(Vector3 point)
        {
            Vector3 normal = new Vector3(0, 0, 0);
            double min = double.PositiveInfinity;
            double distance;

            distance = Math.Abs(this.maxVector.X - Math.Abs(point.X));

            if (distance < min)
            {
                min = distance;
                normal = new Vector3(1, 0, 0);    // Cardinal axis for X
            }
            distance = Math.Abs(this.maxVector.Y - Math.Abs(point.Y));
            if (distance < min)
            {
                min = distance;
                normal = new Vector3(0, 1, 0);     // Cardinal axis for Y
            }
            distance = Math.Abs(this.maxVector.X - Math.Abs(point.Z));

            if (distance < min)
            {
                normal = new Vector3(0, 0, 1);    // Cardinal axis for Z
            }

            return normal;
        }
    }
}
