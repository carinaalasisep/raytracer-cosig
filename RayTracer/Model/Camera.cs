namespace RayTracer.Model
{
    public class Camera
    {
        public int Transformation { get; set; }

        public double Distance { get; set; }

        public double VisionField { get; set; }

        public Camera(
            int transformation,
            double distance,
            double visionField)
        {
            this.Transformation = transformation;
            this.Distance = distance;
            this.VisionField = visionField;
        }
    }
}
