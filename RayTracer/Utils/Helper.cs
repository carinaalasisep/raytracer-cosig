namespace RayTracer.Utils
{
    public class Helper
    {
        public static void Swap<T>(ref T obj1, ref T obj2)
        {
            T temp;
            temp = obj1;
            obj1 = obj2;
            obj2 = temp;
        }
    }
}
