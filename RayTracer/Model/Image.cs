namespace RayTracer.Model
{
    public class Image
    {
        public Color3 Color3 { get; set; }

        public int Horizontal { get; set; }

        public int Vertical { get; set; }

        public Image(
            int horizontal,
            int vertical,
            double red,
            double green,
            double blue)
        {
            this.Horizontal = horizontal;
            this.Vertical = vertical;
            this.Color3 = new Color3
            {
                Red = red,
                Green = green,
                Blue = blue
            };
        }
    }
}
