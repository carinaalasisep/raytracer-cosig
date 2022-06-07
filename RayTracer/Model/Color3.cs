namespace RayTracer.Model
{
    public class Color3
    {
        public double Red { get; set; } = 0;

        public double Green { get; set; } = 0;

        public double Blue { get; set; } = 0;

        public void CheckRange()
        {
            if (this.Red < 0.0)
            {
                this.Red = 0.0;
            }
            else if (this.Red > 1.0)
            {
                this.Red = 1.0;
            }

            if (this.Green < 0.0)
            {
                this.Green = 0.0;
            }
            else if (this.Green > 1.0)
            {
                this.Green = 1.0;
            }

            if (this.Blue < 0.0)
            {
                this.Blue = 0.0;
            }
            else if (this.Blue > 1.0)
            {
                this.Blue = 1.0;
            }
        }
    }
}
