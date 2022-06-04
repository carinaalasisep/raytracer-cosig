namespace RayTracer.Model
{
    using System;
    using System.Numerics;

    public class Transformation
    {
        public double TranslationX { get; set; }
        public double TranslationY { get; set; }
        public double TranslationZ { get; set; }

        public double RxAngle { get; set; }

        public double RyAngle { get; set; }

        public double RzAngle { get; set; }

        public double ScaleX { get; set; }
        public double ScaleY { get; set; }
        public double ScaleZ { get; set; }

        public Matrix4x4 Matrix { get; set; }

        public void Translate(Vector3 position)
        {
            Matrix4x4 translateMatrix = new Matrix4x4(1.0f, 0.0f, 0.0f, position.X, 0.0f, 1.0f, 0.0f, position.Y,
                0.0f, 0.0f, 1.0f, position.Z, 0.0f, 0.0f, 0.0f, 1.0f);

            this.Matrix = Matrix4x4.Multiply(this.Matrix, translateMatrix);
        }

        public void RotateX(float x)
        {
            double radX = (Math.PI / 180) * x;
            Matrix4x4 rotateXMatrix = new Matrix4x4(1.0f, 0.0f, 0.0f, 0.0f, 0.0f, (float)Math.Cos(radX), (float)-Math.Sin(radX), 0.0f
                , 0.0f, (float)Math.Sin(radX), (float)Math.Cos(radX), 0.0f, 0.0f, 0.0f, 0.0f, 1.0f);

            this.Matrix = Matrix4x4.Multiply(this.Matrix, rotateXMatrix);
        }

        public void RotateY(float y)
        {
            float radY = (float)(Math.PI / 180) * y;
            Matrix4x4 rotateYMatrix = new Matrix4x4((float)Math.Cos(radY), 0.0f, (float)Math.Sin(radY), 0.0f, 0.0f, 1.0f, 0.0f, 0.0f
                , (float)-Math.Sin(radY), 0.0f, (float)Math.Cos(radY), 0.0f, 0.0f, 0.0f, 0.0f, 1.0f);

            this.Matrix = Matrix4x4.Multiply(this.Matrix, rotateYMatrix);
        }

        public void RotateZ(float z)
        {
            float radZ = (float)(Math.PI / 180) * z;
            Matrix4x4 rotateZMatrix = new Matrix4x4((float)Math.Cos(radZ), (float)-Math.Sin(radZ), 0.0f, 0.0f, (float)Math.Sin(radZ), (float)Math.Cos(radZ),
                0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f);

            this.Matrix = Matrix4x4.Multiply(this.Matrix, rotateZMatrix);
        }

        public void Scale(Vector3 scale)
        {
            Matrix4x4 scaleMatrix = new Matrix4x4(scale.X, 0.0f, 0.0f, 0.0f, 0.0f, scale.Y, 0.0f, 0.0f,
                0.0f, 0.0f, scale.Z, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f);

            this.Matrix = Matrix4x4.Multiply(this.Matrix, scaleMatrix);
        }
    }
}
