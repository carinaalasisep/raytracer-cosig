namespace RayTracer.Model.Objects
{
    using System;
    using System.Numerics;

    public class Sphere : Object3D
    {
        private const float Radius = 1;

        public override bool Intersect(Ray ray, Hit hit)
        {
            //Taken from
            //https://www.scratchapixel.com/lessons/3d-basic-rendering/minimal-ray-tracer-rendering-simple-shapes/ray-sphere-intersection

            // geometric solution
            var distanceFromRayToOrigin = Vector3.Zero - ray.Origin; 
            var tca = Vector3.Dot(distanceFromRayToOrigin, ray.Direction); 

            // if (tca < 0) return false;

            var d2 = Vector3.Dot(distanceFromRayToOrigin, distanceFromRayToOrigin) - tca * tca;

            if (d2 > Radius) 
            {
                return false; 
            } 
            var thc = (float)Math.Sqrt(Radius - d2);

            //solutions for t if the ray intersects 
            var t0 = tca - thc; 
            var t1 = tca + thc;

            if (t0 > t1) 
            { 
                Utils.Helper.Swap(ref t0, ref t1);
            }

            if (t0 < Utils.Constants.Epsilon)
            {
                t0 = t1;  //if t0 is negative, let's use t1 instead 

                if (t0 < Utils.Constants.Epsilon) 
                { 
                    return false; //both t0 and t1 are negative 
                }
            }

            var t = t0;

            var intersectionPoint = new Vector3(
                ray.Origin.X + ray.Direction.X * t,
                ray.Origin.Y + ray.Direction.Y * t,
                ray.Origin.Z + ray.Direction.Z * t);

            hit.Point = intersectionPoint;

            return true;
        }
    }
}
