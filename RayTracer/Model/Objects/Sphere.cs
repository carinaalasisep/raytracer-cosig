namespace RayTracer.Model.Objects
{
    using System;
    using System.Numerics;

    public class Sphere : Object3D
    {
        private const float PrimitiveRadius = 1;

        public override bool Intersect(Ray ray, Hit hit)
        {
            //Taken from
            //https://www.scratchapixel.com/lessons/3d-basic-rendering/minimal-ray-tracer-rendering-simple-shapes/ray-sphere-intersection

            // geometric solution
            var distanceFromRayToOrigin = Vector3.Zero - ray.Origin; 
            var tca = Vector3.Dot(distanceFromRayToOrigin, ray.Direction); 

            var d2 = Vector3.Dot(distanceFromRayToOrigin, distanceFromRayToOrigin) - tca * tca;

            if (d2 > PrimitiveRadius) 
            {
                return false; 
            } 
            var thc = (float)Math.Sqrt(PrimitiveRadius - d2);

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
                        
            this.ObjCoordToWorldCoord(ray, hit, intersectionPoint);

            var normalIntersectionPoint = Vector3.Normalize(intersectionPoint);

            if (hit.Distance > Utils.Constants.Epsilon && hit.Distance < hit.MinDistance)
            {
                hit.IntersectionPoint = intersectionPoint;
                hit.MinDistance = hit.Distance;
                hit.Found = true;
                hit.Material = this.Material;
                hit.IntersectionNormal = this.ConvertNormalToWorld(normalIntersectionPoint);
            }

            return true;
        }

        private Vector3 ConvertNormalToWorld(Vector3 normal)
        {
            Matrix4x4 invertMatrix;
            Matrix4x4.Invert(this.TransformationNew.Matrix, out invertMatrix);

            Matrix4x4 transposeMatrix;
            transposeMatrix = Matrix4x4.Transpose(invertMatrix);

            float[] otherNormal = { normal.X, normal.Y, normal.Z, 0.0f };

            float[] aux = this.Multiply(otherNormal, transposeMatrix);

            return Vector3.Normalize(new Vector3(aux[0], aux[1], aux[2]));
        }

        private void ObjCoordToWorldCoord(Ray ray, Hit hit, Vector3 intersectionPoint)
        {
            float[] hitPoint = { hit.IntersectionPoint.X, hit.IntersectionPoint.Y, hit.IntersectionPoint.Z, 1.0f };

            float[] aux = this.Multiply(hitPoint, this.TransformationNew.Matrix);

            hit.IntersectionPoint = new Vector3(aux[0] / aux[3], aux[1] / aux[3], aux[2] / aux[3]);

            hit.Distance = Vector3.Subtract(this.ObjectCoordToWorldCoordVector(intersectionPoint), this.ObjectCoordToWorldCoordVector(ray.Origin)).Length();
        }

        private Vector3 ObjectCoordToWorldCoordVector(Vector3 vector)
        {
            float[] vec = { vector.X, vector.Y, vector.Z, 1.0f };

            float[] aux = this.Multiply(vec, this.TransformationNew.Matrix);

            return new Vector3(aux[0], aux[1], aux[2]);
        }

        private float[] Multiply(float[] pointA, Matrix4x4 transformMatrix) // multiplica uma matriz 4 x 4 por uma matriz-coluna representativa de um ponto ou vector expresso em coordenadas homogéneas
        {
            float[] pointB = new float[4];

            pointB[0] = transformMatrix.M11 * pointA[0] + transformMatrix.M12 * pointA[1]
                + transformMatrix.M13 * pointA[2] + transformMatrix.M14 * pointA[3];
            pointB[1] = transformMatrix.M21 * pointA[0] + transformMatrix.M22 * pointA[1]
                + transformMatrix.M23 * pointA[2] + transformMatrix.M24 * pointA[3];
            pointB[2] = transformMatrix.M31 * pointA[0] + transformMatrix.M32 * pointA[1]
                + transformMatrix.M33 * pointA[2] + transformMatrix.M34 * pointA[3];
            pointB[3] = transformMatrix.M41 * pointA[0] + transformMatrix.M42 * pointA[1]
                + transformMatrix.M43 * pointA[2] + transformMatrix.M44 * pointA[3];

            return pointB;
        }
    }
}
