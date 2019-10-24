using System;
using System.Collections.Generic;
using Extreme.Mathematics.Curves;

namespace UTN.MatematicaSuperior.Negocio
{
    public static class NewtonGregory
    {
        public static Polynomial Interpolar(bool esProgresivo, List<double> xList, List<double> yList)
        {
            //Nos manejamos con arrays para hacer más eficientes las operaciones.

            //n es la cantidad de puntos, da igual si se obtiene por xList o yList.
            int n = xList.Count;

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
                    y[j, i] = (y[j + 1, i - 1] - y[j, i - 1]) / (x[i + j] - x[j]);

                #region Pequeño analisis

                //j es 0, i es 1
                //y[0, 1] = (y[1, 0] - y[0, 0] / x[1] - x[0]

                //j es 1, i es 1
                //y[1, 1] = (y[2, 0] - y[1, 0] / x[2] - x[1]

                //j es 2, i es 1
                //y[2, 1] = (y[3, 0] - y[2, 0] / x[3] - x[2]

                //j es 0, i es 2
                //y[0, 2] = (y[1, 1] - y[0, 1] / x[2] - x[0]

                //j es 1, i es 2
                //y[1, 2] = (y[2, 1] - y[1, 1] / x[3] - x[1]


                #endregion Pequeño analisis
            }

            //Para progresivo, los coeficientes van a estar en la fila y[0][]
            //Para regresivo, van a estar en la recta y[n-1][0], y[n-2][1], ...

            double[] coeficientes =
                esProgresivo ? ObtenerCoeficientesProgresivo(y, n) : ObtenerCoeficientesRegresivo(y, n);

            //Creo un polinomio con el término independiente.
            Polynomial polinomio = new Polynomial(n);
            polinomio[0] = coeficientes[0];

            //Itero por cada grado empezando del grado 1 (la x).
            for (int i = 1; i < n; i++)
            {
                //Creo un polinomio temporal que va a contener los (x-a) multiplicados.
                //Le asigno el primer (x-a) para evitar recursividad.
                Polynomial polinomioTemp = new Polynomial(new List<double> { esProgresivo ? -x[0] : -x[n-1], 1 });
                var lala = polinomioTemp.ToString();

                //Multiplico los demás (x-a), en caso de haber, al polinomio temporal.
                for (int j = 1; j < i; j++)
                {
                    polinomioTemp *= new Polynomial(new List<double> { esProgresivo ? -x[j] : -x[n-j-1], 1 });
                    var sarasa = polinomioTemp.ToString();
                }

                //Le sumo al polinomio la multiplicación entre el término del árbol y el polinomio obtenido.
                polinomio += new Polynomial(new List<double> { coeficientes[i] }) * polinomioTemp;
                var pepe = polinomio.ToString();
            }


            return polinomio;

            // Dibujo el árbol.
            //for (int i = 0; i < n; i++)
            //{
            //    Console.Write(x[i] + "\t");
            //    for (int j = 0; j < n - i; j++)
            //        Console.Write(y[i, j] + "\t");
            //    Console.WriteLine();
            //}
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
    }
}
