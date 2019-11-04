using System;
using System.Collections.Generic;
using Extreme.Mathematics.Curves;

namespace UTN.MatematicaSuperior.Negocio
{
    public static class NewtonGregory
    {
        private static string dobleTab = "\t\t";

        public static Polynomial Interpolar(bool esProgresivo, List<double> xList, List<double> yList, out string pasos)
        {
            //Nos manejamos con arrays para hacer más eficientes las operaciones.

            //n es la cantidad de puntos, da igual si se obtiene por xList o yList.
            int n = xList.Count;

            pasos = "1) Arbol\n\nxi" + dobleTab + "yi" + dobleTab;

            for (int i = 1; i < n; i++)
            {
                pasos += "Orden " + i + dobleTab;
            }

            pasos += "\n";

            //Paso la lista de x a un array.
            double[] x = xList.ToArray();

            //Paso la lista de y a un array de nxn que va a contener el árbol.
            double[,] y = new double[n, n];

            for (int i = 0; i < n; i++)
            {
                y[i, 0] = yList[i];
            }

            // Hago las operaciones para generar el árbol.
            for (int i = 1; i < n; i++)
            {
                for (int j = 0; j < n - i; j++)
                {
                    y[j, i] = (y[j + 1, i - 1] - y[j, i - 1]) / (x[i + j] - x[j]);
                }
            }

            for (int i = 0; i < n; i++)
            {
                pasos += x[i] + dobleTab + y[i, 0] + dobleTab;

                for (int j = 1; j < n; j++)
                {
                    if (j <= n - i - 1)
                    {
                        pasos += y[i, j];
                    }
                    else
                    {
                        pasos += string.Empty;
                    }

                    pasos += dobleTab;
                }

                pasos += "\n";
            }
            
            pasos += "\n2) Armado de polinomio\n\n";

            double[] coeficientes =
                esProgresivo ? ObtenerCoeficientesProgresivo(y, n) : ObtenerCoeficientesRegresivo(y, n);

            //Creo un polinomio con el término independiente.
            Polynomial polinomio = new Polynomial(n);
            polinomio[0] = coeficientes[0];
            pasos += coeficientes[0];

            //Itero por cada grado empezando del grado 1 (la x).
            for (int i = 1; i < n; i++)
            {
                pasos += ObtenerExpresion(coeficientes[i]);
                //Creo un polinomio temporal que va a contener los (x-a) multiplicados.
                //Le asigno el primer (x-a) para evitar recursividad.
                Polynomial polinomioTemp = new Polynomial(new List<double> { esProgresivo ? -x[0] : -x[n-1], 1 });
                pasos += "(x" + ObtenerExpresion(polinomioTemp[0]) + ")";

                //Multiplico los demás (x-a), en caso de haber, al polinomio temporal.
                for (int j = 1; j < i; j++)
                {
                    Polynomial polinomioTempSegundo = new Polynomial(new List<double> { esProgresivo ? -x[j] : -x[n - j - 1], 1 });
                    pasos += "(x" + ObtenerExpresion(polinomioTempSegundo[0]) + ")";
                    polinomioTemp *= polinomioTempSegundo;
                }

                //Le sumo al polinomio la multiplicación entre el término del árbol y el polinomio obtenido.
                polinomio += new Polynomial(new List<double> { coeficientes[i] }) * polinomioTemp;
            }

            pasos += "\n\n3) Distributiva y asociativa\n\n" + polinomio.ToString();

            return polinomio;
        }

        private static double[] ObtenerCoeficientesProgresivo(double[,] arbol, int n)
        {
            double[] coeficientes = new double[n];

            for (int i = 0; i < n; i++)
            {
                coeficientes[i] = arbol[0, i];
            }

            return coeficientes;
        }

        private static double[] ObtenerCoeficientesRegresivo(double[,] arbol, int n)
        {
            double[] coeficientes = new double[n];

            for (int i = 0; i < n; i++)
            {
                coeficientes[i] = arbol[n-i-1, i];
            }

            return coeficientes;
        }

        private static string ObtenerExpresion(double numero)
        {
            return (numero > 0 ? "+" : "-") + Math.Abs(numero);
        }
    }
}
