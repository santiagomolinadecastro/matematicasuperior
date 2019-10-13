using System;
using System.Collections.Generic;
using UTN.MatematicaSuperior.Negocio;

namespace UTN.MatematicaSuperior.Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            List<double> xList = new List<double>(), yList = new List<double>();
            
            string resultado = NewtonGregory.Interpolar(xList, yList);

            Console.WriteLine(resultado);
            Console.ReadKey();
        }
    }
}
