using Extreme.Mathematics.Curves;

namespace UTN.MatematicaSuperior.Negocio
{
    public static class Evaluacion
    {
        public static double EvaluarPolinomio(Polynomial polinomio, double k)
        {
            return polinomio.ValueAt(k);
        }
    }
}
