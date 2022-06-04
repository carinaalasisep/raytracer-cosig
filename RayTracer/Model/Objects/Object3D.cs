namespace RayTracer.Model
{
    using System.Collections.Generic;
    using System.Numerics;

    public abstract class Object3D
    {
        public int TransformationIndex { get; set; }

        public Transformation Transformation { get; set; }

        public int Material { get; set; }

        public abstract bool Intersect(Ray ray, Hit hit);

        public Transformation transform(int cameraTransform, int objTransform, List<Transformation> transformations)
        {
            return new Transformation(Matrix4x4.Multiply(transformations.ToArray()[cameraTransform].Matrix, transformations.ToArray()[objTransform].Matrix));
        }

        public Vector3 ConvertNormalToWorld(Vector3 normal)
        {
            Matrix4x4 invertMatrix;
            Matrix4x4.Invert(this.Transformation.Matrix, out invertMatrix);

            Matrix4x4 transposeMatrix;
            transposeMatrix = Matrix4x4.Transpose(invertMatrix);

            float[] otherNormal = { normal.X, normal.Y, normal.Z, 0.0f };

            float[] aux = this.Multiply(otherNormal, transposeMatrix);

            return Vector3.Normalize(new Vector3(aux[0], aux[1], aux[2]));
        }

        public void ObjCoordToWorldCoord(Ray ray, Hit hit, Vector3 intersectionPoint)
        {
            float[] hitPoint = { hit.IntersectionPoint.X, hit.IntersectionPoint.Y, hit.IntersectionPoint.Z, 1.0f };

            float[] aux = this.Multiply(hitPoint, this.Transformation.Matrix);

            hit.IntersectionPoint = new Vector3(aux[0] / aux[3], aux[1] / aux[3], aux[2] / aux[3]);

            hit.Distance = Vector3.Subtract(this.ObjectCoordToWorldCoordVector(intersectionPoint), this.ObjectCoordToWorldCoordVector(ray.Origin)).Length();
        }

        private Vector3 ObjectCoordToWorldCoordVector(Vector3 vector)
        {
            float[] vec = { vector.X, vector.Y, vector.Z, 1.0f };

            float[] aux = this.Multiply(vec, this.Transformation.Matrix);

            return new Vector3(aux[0], aux[1], aux[2]);
        }

        public float[] Multiply(float[] pointA, Matrix4x4 transformMatrix) // multiplica uma matriz 4 x 4 por uma matriz-coluna representativa de um ponto ou vector expresso em coordenadas homogéneas
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
