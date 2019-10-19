using Extreme.Mathematics.Curves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTN.MatematicaSuperior.Negocio
{
    public class Polinomio
    {
        Polynomial _polinomio;

        public Polinomio(List<double> coeficientes)
        {
            _polinomio = new Polynomial(coeficientes.Count);

            int index = 0;

            foreach (var coeficiente in coeficientes)
            {
                _polinomio[index] = coeficiente;

                index++;
            }
        }

        public double EvaluarPolinomio(double k)
        {
            return _polinomio.ValueAt(k);
        }
    }
}
