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

        public override bool Intersect(Ray ray, Hit hit)
        {
            // Taken from
            // https://www.scratchapixel.com/lessons/3d-basic-rendering/ray-tracing-rendering-a-triangle/barycentric-coordinates

            // compute plane's normal
            var p0p1Distance = this.P1 - this.P0;
            var p0p2Distance = this.P2 - this.P0;

            // no need to normalize
            var crossedVector = Vector3.Cross(p0p1Distance, p0p2Distance);  //N 
            crossedVector = Vector3.Normalize(crossedVector);
            var area2 = crossedVector.Length(); // TODO: Check if area2 variable is necessary

            // Step 1: finding the intersection point
            // check if ray and plane are parallel ?
            var dotRayDirection = Vector3.Dot(crossedVector, ray.Direction);

            if (Math.Abs(dotRayDirection) < Utils.Constants.Epsilon) //almost 0 
            {
                return false; //they are parallel so they don't intersect ! 
            }  
                
            // compute d parameter using equation 2
            var d = -Vector3.Dot(crossedVector, this.P0);

            // compute t (equation 3)
            var TDistance = -(Vector3.Dot(crossedVector, ray.Origin) + d) / dotRayDirection;

            // check if the triangle is in behind the ray
            if (TDistance < 0)
            {
                return false;  //the triangle is behind 
            }

            // compute the intersection point using equation 1
            var intersectionPoint = new Vector3(ray.Origin.X + ray.Direction.X * TDistance,
                ray.Origin.Y + ray.Direction.Y * TDistance,
                ray.Origin.Z + ray.Direction.Z * TDistance);

            // Step 2: inside-outside test

            // edge 0
            var edge0 = this.P1 - this.P0;
            var vp0 = intersectionPoint - this.P0;

            var perpendicularVector = Vector3.Cross(edge0, vp0); //vector perpendicular to triangle's plane 

            if (Vector3.Dot(crossedVector, perpendicularVector) < Utils.Constants.Epsilon)
            {
                return false; //The intersection point is on the right side 
            }

            // edge 1
            var edge1 = this.P2 - this.P1;
            var vp1 = intersectionPoint - this.P1;
            perpendicularVector = Vector3.Cross(edge1, vp1);
            var u = Vector3.Dot(crossedVector, perpendicularVector);
            if (u < Utils.Constants.Epsilon)
            {
                return false;  //The intersection point is on the right side 
            }  

            // edge 2
            var edge2 = this.P0 - this.P2;
            var vp2 = intersectionPoint - this.P2;
            perpendicularVector = Vector3.Cross(edge2, vp2);
            var v = perpendicularVector.Length() / area2;
            if (Vector3.Dot(crossedVector, perpendicularVector) < Utils.Constants.Epsilon)
            {
                return false; //The intersection point is on the right side
            }

            if (Vector3.Dot(ray.Direction, hit.IntersectionNormal) < -Utils.Constants.Epsilon)
            {
                return false;
            }

            hit.IntersectionPoint = intersectionPoint;

            this.ObjCoordToWorldCoord(ray, hit, intersectionPoint);

            if (hit.Distance > Utils.Constants.Epsilon && hit.Distance < hit.MinDistance)
            {
                hit.MinDistance = hit.Distance;
                hit.Found = true;
                hit.Material = this.Material;

                hit.IntersectionNormal = this.ConvertNormalToWorld(crossedVector);
            }

            return true;  //this ray hits the triangle 
        }
    }
}
