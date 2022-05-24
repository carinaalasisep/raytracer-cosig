namespace RayTracer.Model
{
    public class Light
    {
        public int Transformation;

        public Color3 Color;

        public Light(int transformation, double[] rgb)
        {
            this.Transformation = transformation;

            this.Color = new Color3
            {
                Red = rgb[0],
                Green = rgb[1],
                Blue = rgb[2]
            };
        }
    }
}
