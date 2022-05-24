namespace RayTracer.Model
{
    public class Camera
    {
        public int Transformation;

        public double Distance;

        public double VisionField;

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
