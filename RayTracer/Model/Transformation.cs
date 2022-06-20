namespace RayTracer.Model
{
    using System;
    using System.Numerics;

    public class Transformation
    {
        public Matrix4x4 Matrix { get; set; }

        public Transformation()
        {
            this.Matrix = Matrix4x4.Identity;
        }

        public Transformation(Matrix4x4 matrix)
        {
            this.Matrix = matrix;
        }

        public void Translate(Vector3 position)
        {
            var translateMatrix = new Matrix4x4(1.0f, 0.0f, 0.0f, position.X, 0.0f, 1.0f, 0.0f, position.Y,
                0.0f, 0.0f, 1.0f, position.Z, 0.0f, 0.0f, 0.0f, 1.0f);

            this.Matrix = Matrix4x4.Multiply(this.Matrix, translateMatrix);
        }

        public void RotateX(float x)
        {
            var radX = (Math.PI / 180) * x;
            var rotateXMatrix = new Matrix4x4(1.0f, 0.0f, 0.0f, 0.0f, 0.0f, (float)Math.Cos(radX), (float)-Math.Sin(radX), 0.0f
                , 0.0f, (float)Math.Sin(radX), (float)Math.Cos(radX), 0.0f, 0.0f, 0.0f, 0.0f, 1.0f);

            this.Matrix = Matrix4x4.Multiply(this.Matrix, rotateXMatrix);
        }

        public void RotateY(float y)
        {
            var radY = (float)(Math.PI / 180) * y;
            var rotateYMatrix = new Matrix4x4((float)Math.Cos(radY), 0.0f, (float)Math.Sin(radY), 0.0f, 0.0f, 1.0f, 0.0f, 0.0f
                , (float)-Math.Sin(radY), 0.0f, (float)Math.Cos(radY), 0.0f, 0.0f, 0.0f, 0.0f, 1.0f);

            this.Matrix = Matrix4x4.Multiply(this.Matrix, rotateYMatrix);
        }

        public void RotateZ(float z)
        {
            var radZ = (float)(Math.PI / 180) * z;
            var rotateZMatrix = new Matrix4x4((float)Math.Cos(radZ), (float)-Math.Sin(radZ), 0.0f, 0.0f, (float)Math.Sin(radZ), (float)Math.Cos(radZ),
                0.0f, 0.0f, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f);

            this.Matrix = Matrix4x4.Multiply(this.Matrix, rotateZMatrix);
        }

        public void Scale(Vector3 scale)
        {
            var scaleMatrix = new Matrix4x4(scale.X, 0.0f, 0.0f, 0.0f, 0.0f, scale.Y, 0.0f, 0.0f,
                0.0f, 0.0f, scale.Z, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f);

            this.Matrix = Matrix4x4.Multiply(this.Matrix, scaleMatrix);
        }
    }
}
