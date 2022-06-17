namespace RayTracer.Model
{
    using System.Collections.Generic;
    using System.Numerics;

    public abstract class Object3D
    {
        public int TransformationIndex { get; set; }

        public Transformation Transformation { get; set; }

        public int Material { get; set; }

        public abstract bool Intersect(Ray ray, Hit hit, Vector3 origin);

        public Transformation Transform(int cameraTransform, int objTransform, List<Transformation> transformations)
        {
            return new Transformation(Matrix4x4.Multiply(transformations.ToArray()[cameraTransform].Matrix, transformations.ToArray()[objTransform].Matrix));
        }

        public Vector3 ConvertNormalToWorld(Vector3 normal)
        {
            Matrix4x4 invertMatrix;
            Matrix4x4.Invert(this.Transformation.Matrix, out invertMatrix);

            Matrix4x4 transposeMatrix;
            transposeMatrix = Matrix4x4.Transpose(invertMatrix);

            float[] normalObject = { normal.X, normal.Y, normal.Z, 0.0f };

            float[] normalWorld = Utils.Helper.Multiply1(normalObject, transposeMatrix);

            return Vector3.Normalize(new Vector3(normalWorld[0], normalWorld[1], normalWorld[2]));
        }

        public void ObjectCoordToWorldCoord(Ray ray, Hit hit, Vector3 intersectionPoint, Vector3 origin)
        {
            hit.IntersectionPoint = this.ObjectCoordToWorldCoordVector(intersectionPoint);

            hit.Distance = Vector3.Subtract(hit.IntersectionPoint, origin).Length();
        }

        private Vector3 ObjectCoordToWorldCoordVector(Vector3 vectorObject)
        {
            float[] vec = { vectorObject.X, vectorObject.Y, vectorObject.Z, 1.0f };

            float[] aux = Utils.Helper.Multiply1(vec, this.Transformation.Matrix);

            return new Vector3(aux[0], aux[1], aux[2]);
        }

        public void WorldCoordToObjCoord(Ray ray, Transformation transformation)
        {
            float[] dir = { ray.Direction.X, ray.Direction.Y, ray.Direction.Z, 0.0f };
            float[] orig = { ray.Origin.X, ray.Origin.Y, ray.Origin.Z, 1.0f };
            Matrix4x4 invertMatrix;
            Matrix4x4.Invert(transformation.Matrix, out invertMatrix);

            float[] directionMatrix = Utils.Helper.Multiply1(dir, invertMatrix);
            float[] originMatrix = Utils.Helper.Multiply1(orig, invertMatrix);

            ray.Direction = Vector3.Normalize(new Vector3(directionMatrix[0], directionMatrix[1], directionMatrix[2]));
            ray.Origin = new Vector3(originMatrix[0], originMatrix[1], originMatrix[2]);
        }
    }
}
