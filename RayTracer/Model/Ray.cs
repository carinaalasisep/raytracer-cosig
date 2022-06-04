namespace RayTracer.Model
{
    using System.Numerics;

    public class Ray
    {
        public Vector3 Origin { get; set; }

        public Vector3 Direction { get; set; } //should be normalized

        // callback do button start vamos ter um duplo ciclo for que percorre todos os pixeis de todas as linhas da nossa imagem
        // FUNÇAO Recursiva que vai dar a cor   -> traceRay(ray,rec) -> em que rec é o numero selecionado na GUI

    }
}
