namespace RayTracer.Model.Objects
{
    using System;
    using System.Numerics;

    public class Box : Object3D
    {
        private float tnear = 0.0f;
        private float tfar = 0.0f;
        private readonly double kEpsilon = 0.001;

        public Box(int transformation, int material)
        {
            this.TransformationIndex = transformation;
            this.Material = material;
        }

        public override bool Intersect(Ray ray, Hit hit, Vector3 origin)
        {
            int axis = -1;
            if (this.IntersectXAxis(ray, ref axis) && this.IntersectYAxis(ray, ref axis) && this.IntersectZAxis(ray, ref axis))
            {
                var resultIntersect = new Vector3(ray.Origin.X + ray.Direction.X * this.tnear,
                    ray.Origin.Y + ray.Direction.Y * this.tnear,
                    ray.Origin.Z + ray.Direction.Z * this.tnear);

                this.ObjectCoordToWorldCoord(hit, resultIntersect, origin);

                if (hit.Distance > this.kEpsilon && hit.Distance < hit.MinDistance)
                {
                    hit.Found = true;

                    hit.MinDistance = hit.Distance;

                    hit.Material = this.Material;

                    switch (axis)
                    {
                        case 0:
                            var normalX = resultIntersect.X > 0 ? 1 : -1;
                            hit.IntersectionNormal = this.ConvertNormalToWorld(new Vector3(Math.Sign(normalX), 0, 0));
                            break;
                        case 1:
                            var normalY = resultIntersect.Y > 0 ? 1 : -1;
                            hit.IntersectionNormal = this.ConvertNormalToWorld(new Vector3(0, Math.Sign(normalY), 0));
                            break;
                        case 2:
                            var normalZ = resultIntersect.Z > 0 ? 1 : -1;
                            hit.IntersectionNormal = this.ConvertNormalToWorld(new Vector3(0, 0, Math.Sign(normalZ)));
                            break;
                    }

                    return true;
                }
            }

            return false;
        }

        private bool IntersectXAxis(Ray ray, ref int axis)
        {
            //If parallel
            if (ray.Direction.X == 0)
            {
                if (ray.Origin.X < -0.5 || ray.Origin.X > 0.5)
                {
                    return false;
                }

                this.tnear = float.MinValue;
                this.tfar = float.MaxValue;
            }
            else
            {
                this.tnear = (float)((-0.5 - ray.Origin.X) / ray.Direction.X);
                this.tfar = (float)((0.5 - ray.Origin.X) / ray.Direction.X);

                if (this.tnear > this.tfar)
                {
                    Utils.Helper.Swap(ref this.tnear, ref this.tfar);
                }

                if (this.tfar < this.kEpsilon)
                {
                    return false;
                }

            }

            axis = 0;

            return true;
        }

        private bool IntersectYAxis(Ray ray, ref int axis)
        {
            //If parallel
            if (ray.Direction.Y == 0)
            {
                if (ray.Origin.Y < -0.5 || ray.Origin.Y > 0.5)
                {
                    return false;
                }
            }
            else
            {
                var t1 = (float)(-0.5 - ray.Origin.Y) / ray.Direction.Y;
                var t2 = (float)(0.5 - ray.Origin.Y) / ray.Direction.Y;

                if (t1 > t2)
                {
                    Utils.Helper.Swap(ref t1, ref t2);
                }

                if (t1 > this.tnear)
                {
                    axis = 1;
                    this.tnear = t1;
                }

                if (t2 < this.tfar)
                {
                    this.tfar = t2;
                }

                if (this.tnear > this.tfar || this.tfar < this.kEpsilon)
                {
                    return false;
                }
            }

            return true;
        }

        private bool IntersectZAxis(Ray ray, ref int axis)
        {
            //If parallel
            if (ray.Direction.Z == 0)
            {
                if (ray.Origin.Z < -0.5 || ray.Origin.Z > 0.5)
                {
                    return false;
                }

            }
            else
            {
                var t1 = (float)(-0.5 - ray.Origin.Z) / ray.Direction.Z;
                var t2 = (float)(0.5 - ray.Origin.Z) / ray.Direction.Z;

                if (t1 > t2)
                {
                    Utils.Helper.Swap(ref t1, ref t2);
                }

                if (t1 > this.tnear)
                {
                    axis = 2;
                    this.tnear = t1;
                }

                if (t2 < this.tfar)
                {
                    this.tfar = t2;
                }

                if (this.tnear > this.tfar || this.tfar < this.kEpsilon)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
