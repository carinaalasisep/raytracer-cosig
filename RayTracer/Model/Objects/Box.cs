using System.Numerics;

namespace RayTracer.Model.Objects
{
    public class Box : Object3D
    {
        private float tnear = 0.0f;
        private float tfar = 0.0f;
        private float t1 = 0.0f;
        private float t2 = 0.0f;
        private Vector3 resultIntersect;

        public override bool Intersect(Ray ray, Hit hit)
        {
            var axis = 0;
            if(this.IntersectXAxis(ray, ref axis))
            {
                return true;
            }
            throw new System.NotImplementedException();
        }

        private bool IntersectXAxis(Ray ray, ref int axis)
        {
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
                    this.Swap(ref this.tnear, ref this.tfar);
                }

                if (this.tfar < Constants.Constants.Epsilon) //Se aparecer acne trocar 0 para kEpsilon
                {
                    return false;
                }

            }

            axis = 0;

            return true;
        }

        private void Swap(ref float tnear, ref float tfar)
        {
            throw new System.NotImplementedException();
        }
    }
}
