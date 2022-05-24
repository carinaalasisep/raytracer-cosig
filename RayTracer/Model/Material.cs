namespace RayTracer.Model
{
    public class Material
    {
        public Color3 Color;

        public double Environment; // 0.0 <= __ <= 1.0

        public double Difuse;  // 0.0 <= __ <= 1.0

        public double Specular;  // 0.0 <= __ <= 1.0

        public double Refraction;  // 0.0 <= __ <= 1.0

        public double RefractionIdex; // >1.0

        public Material(double[] rgb, double[] coefficients)
        {
            this.Color = new Color3
            {
                Red = rgb[0],
                Green = rgb[1],
                Blue = rgb[2]
            };

            this.Environment = coefficients[0];
            this.Difuse = coefficients[1];
            this.Specular = coefficients[2];
            this.Refraction = coefficients[3];
            this.RefractionIdex = coefficients[4];
        }

    }
}