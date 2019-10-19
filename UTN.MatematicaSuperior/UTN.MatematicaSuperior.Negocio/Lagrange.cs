using System;
using System.Collections.Generic;
using Extreme.Mathematics.Curves;

namespace UTN.MatematicaSuperior.Negocio
{
    public static class Lagrange
    {
        public static string Interpolar(List<double> xList, List<double> yList)
        {
            //Nos manejamos con arrays para hacer más eficientes las operaciones.

            //n es el grado máximo del polinomio
            int n = xList.Count - 1;

            //Paso la lista de x a un array.
            double[] x = xList.ToArray();

            //Paso la lista de y a un array
            double[] y = yList.ToArray();

            // instancio el polinomio degrado n
            Polynomial polinomio = new Polynomial(n);
            polinomio[0] = 0;

            Polynomial[] Lj = new Polynomial[n + 1]; // Cada uno de los polinomios base
            Polynomial li;

            // Por cada término calculo las bases polinómicas
            for (int j = 0; j <= n; j++)
            {
                // Base
                Lj[j] = new Polynomial(n);
                Lj[j][0] = y[j];

                for (int i = 0; i <= n; i++)
                {
                    if (i == j) continue; // Filtro

                    // Voy multiplicando polinomios
                    li = new Polynomial(1);
                    li[0] = (-x[i]) / (x[j] - x[i]);
                    li[1] = 1 / (x[j] - x[i]);

                    Lj[j] = Lj[j] * li;
                }

                polinomio += Lj[j];
            }

            return polinomio.ToString();
        }
    }
}
