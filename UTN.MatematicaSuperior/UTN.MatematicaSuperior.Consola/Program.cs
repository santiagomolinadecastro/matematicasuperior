using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTN.MatematicaSuperior.Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            NewtonGregory();
        }

        #region - Private Methods -

        static void NewtonGregory()
        {
            int n = 5;
            double[] x = { 45, 50, 55, 60, 70 };

            double[,] y = new double[n, n];
            y[0, 0] = 0.7071;
            y[1, 0] = 0.7660;
            y[2, 0] = 0.8192;
            y[3, 0] = 0.8660;
            y[4, 0] = 0.9660;

            // Hago las operaciones.
            for (int i = 1; i < n; i++)
            {
                for (int j = 0; j < n - i; j++)
                    y[j, i] = y[j + 1, i - 1] - y[j, i - 1];
            }

            // Dibujo el árbol.
            for (int i = 0; i < n; i++)
            {
                Console.Write(x[i] + "\t");
                for (int j = 0; j < n - i; j++)
                    Console.Write(y[i, j] + "\t");
                Console.WriteLine();
            }

            // Evalúo el polinomio en 52.
            double valor = 52;



            // initializing u and sum 
            // Evalúa el punto.
            double sum = y[0, 0];
            double u = (valor - x[0]) / (x[1] - x[0]);
            for (int i = 1; i < n; i++)
            {
                sum = sum + (u_cal(u, i) * y[0, i]) /
                                        fact(i);
            }

            Console.WriteLine("\n Value at " + valor + " is " + Math.Round(sum, 6));
            Console.ReadKey();
        }

        // calculating u mentioned in the formula 
        static double u_cal(double u, int n)
        {
            double aux = u;
            for (int i = 1; i < n; i++)
                aux = aux * (u - i);
            return aux;
        }

        // calculating factorial of given number n 
        static int fact(int n)
        {
            int f = 1;
            for (int i = 2; i <= n; i++)
                f *= i;
            return f;
        }

        #endregion - Private Methods -
    }
}
