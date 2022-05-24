namespace RayTracer.Model
{
    public class Material
    {
        public Color3 Color { get; set; }

        public double Environment { get; set; } // 0.0 <= __ <= 1.0

        public double Difuse { get; set; }  // 0.0 <= __ <= 1.0

        public double Specular { get; set; }  // 0.0 <= __ <= 1.0

        public double Refraction { get; set; }  // 0.0 <= __ <= 1.0

        public double RefractionIdex { get; set; } // >1.0

        public Material(double red, double green, double blue, double[] coefficients)
        {
            this.Color = new Color3
            {
                Red = red,
                Green = green,
                Blue = blue
            };

            this.Environment = coefficients[0];
            this.Difuse = coefficients[1];
            this.Specular = coefficients[2];
            this.Refraction = coefficients[3];
            this.RefractionIdex = coefficients[4];
        }

    }
}