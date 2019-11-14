using System;
using System.Collections.Generic;
using Extreme.Mathematics.Curves;
using Fractions;

namespace UTN.MatematicaSuperior.Negocio
{
    public static class Lagrange
    {
        // Cada uno de los polinomios base, necesito tenerlos. Pasar a atributo de clase
        public static Polynomial[] Lj;

        public static Polynomial Interpolar(List<double> xList, List<double> yList, out string pasos)
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
                    if (i != j) // Filtro
                    {
                        raices[aux] = x[i];
                        aux++;
                        denominador *= (x[j] - x[i]);
                    }
                }

                denominadores[j] = denominador;
                comun_denominador *= denominador;

                if (n != 0)
                    Lj[j] = Polynomial.FromRoots(raices);
                else
                {
                    Lj[j] = new Polynomial(0);
                    Lj[j][0] = 1;
                }

                Polynomial factor = new Polynomial(0);
                factor[0] = y[j];

                Lj[j] *= factor;
            }

            // instancio el polinomio de grado n que será el resultado
            Polynomial polinomio = new Polynomial(n);


                // multiplico cada Li con el factor del común divisor
            int grado = n;

            while (grado >= 0)
            {
                Fraction sumatoria = Fraction.FromDecimal(0);

                for (int j = 0; j <= n; j++)
                {
                    Fraction fraccion = Fraction.FromDecimal((decimal)Lj[j][grado]);
                    Fraction fraccionb = new Fraction((decimal)denominadores[j]);
                    fraccion = fraccion / fraccionb;
                    fraccion.Reduce();

                    sumatoria += fraccion;
                }

                polinomio[grado] = sumatoria.ToDouble();

                grado--;
            }

            // Mostrar paso
            pasos = "Estos son los pasos de Lagrange.\n\n";

            // Imprimo cada polinomio
            pasos += "1) Polinomios Li base:\n\n";

            // conversión a fracciones
            for (int j = 0; j <= n; j++)
            {
                grado = Lj[j].Degree;

                pasos += "L" + j + ": ";

                while (grado >= 0)
                {
                    Fraction fraccion = Fraction.FromDecimal((decimal)Lj[j][grado]);
                    Fraction fraccionb = new Fraction((decimal)denominadores[j]);
                    fraccion = fraccion / fraccionb;
                    fraccion.Reduce();

                    // Signo para separar término (menos el primero), si es neg. ya lo imprime
                    if (grado < Lj[j].Degree && (Lj[j][grado] * denominadores[j]) > 0)
                        pasos += "+";

                    // imprimo el coeficiente sólo si es dif a cero o es polinomio grado cero
                    if (Lj[j][grado] != 0 || (Lj[j].Degree == 0 && grado == 0))
                        if (grado > 1)
                            pasos += fraccion.ToString() + "x^" + grado;
                        else if (grado == 1)
                            pasos += fraccion.ToString() + "x";
                        else
                            pasos += fraccion.ToString();

                    grado--;
                }

                pasos += "\n\n";
            }

            // Imprimo cada polinomio
            pasos += "2) Armado del polinomio:\n\n";

            // recorro del grado mayor al menor
            for (grado = n; grado >= 0; grado--)
            {
                if (grado < n)
                    pasos += "+";

                pasos += "(";

                for (int j = 0; j <= n; j++)
                {
                    if (j > 0 && (Lj[j][grado] * denominadores[j]) >= 0)
                        pasos += "+";

                    Fraction fraccion = Fraction.FromDecimal((decimal)Lj[j][grado]);
                    Fraction fraccionb = new Fraction((decimal)denominadores[j]);
                    fraccion = fraccion / fraccionb;
                    fraccion.Reduce();

                    pasos += fraccion.ToString();
                }

                pasos += ")";

                if(grado > 1)
                    pasos += "x^" + grado;
                else if(grado == 1)
                    pasos += "x";

            }

            pasos += "\n\n";

            // Resultado
            pasos += "3) Resultado:\n\n";
            // Imprimo cada polinomio
            pasos += polinomio.ToString() + "\n\n";


            return polinomio;
        }
    }
}
