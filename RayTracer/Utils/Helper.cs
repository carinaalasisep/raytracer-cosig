namespace RayTracer.Utils
{
    using System.Numerics;

    public static class Helper
    {
        public static void Swap<T>(ref T obj1, ref T obj2)
        {
            T temp;
            temp = obj1;
            obj1 = obj2;
            obj2 = temp;
        }

        public static float[] Multiply(float[] pointA, Matrix4x4 transformMatrix) // multiplica uma matriz 4 x 4 por uma matriz-coluna representativa de um ponto ou vector expresso em coordenadas homogéneas
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