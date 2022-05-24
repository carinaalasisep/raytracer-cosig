namespace RayTracer.Model
{
    public class Image
    {
        public Color3 Color3;

        public int Horizontal;

        public int Vertical;

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
