namespace RayTracer.Model
{
    public class Light
    {
        public int Transformation { get; set; }

        public Color3 Color { get; set; }

        public Light(int transformation, double red, double green, double blue)
        {
            this.Transformation = transformation;

            this.Color = new Color3
            {
                Red = red,
                Green = green,
                Blue = blue
            };
        }
    }
}
