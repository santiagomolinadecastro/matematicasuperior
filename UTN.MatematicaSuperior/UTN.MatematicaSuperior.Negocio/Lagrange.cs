using System;
using System.Collections.Generic;
using Extreme.Mathematics.Curves;

namespace UTN.MatematicaSuperior.Negocio
{
    public static class Lagrange
    {
        // Cada uno de los polinomios base, necesito tenerlos. Pasar a atributo de clase
        public static Polynomial[] Lj;
        
        public static Polynomial Interpolar(List<double> xList, List<double> yList)
        {
            //Nos manejamos con arrays para hacer más eficientes las operaciones.

            //n es el grado máximo del polinomio
            int n = xList.Count - 1;

            //Paso la lista de x a un array.
            double[] x = xList.ToArray();

            //Paso la lista de y a un array
            double[] y = yList.ToArray();

            Lj = new Polynomial[n + 1];
            
            // de cada Lj, denominador del productorio
            double[] denominadores = new double[n + 1];
            double comun_denominador = 1;

            // Por cada término calculo las bases polinómicas
            for (int j = 0; j <= n; j++)
            {
                double[] raices = new double[n];
                int aux = 0;
                double denominador = 1;

                for (int i = 0; i <= n; i++)
                {
                    if (i == j) continue; // Filtro

                    raices[aux] = x[i];
                    aux++;

                    denominador = denominador * (x[j] - x[i]);
                }

                denominadores[j] = denominador;
                comun_denominador = comun_denominador * denominador;

                Lj[j] = Polynomial.FromRoots(raices);

                Polynomial factor = new Polynomial(0);
                factor[0] = y[j];

                Lj[j] = Lj[j] * factor;
            }

            // instancio el polinomio de grado n que será el resultado
            Polynomial polinomio = new Polynomial(n);
            polinomio[0] = 0;

            Polynomial divisor = new Polynomial(0);
            divisor[0] = comun_denominador;
            
            // multiplico cada Li con el factor del común divisor
            for (int j = 0; j <= n; j++)
            {
                Polynomial factor = new Polynomial(0);
                factor[0] = comun_denominador / denominadores[j];

                polinomio += ( Lj[j] * factor);
                
                // al Lj lo divido para ya tenerlo listo
                Lj[j] = Lj[j] / divisor;
            }

            polinomio = polinomio / divisor;

            return polinomio;
        }
    }
}
