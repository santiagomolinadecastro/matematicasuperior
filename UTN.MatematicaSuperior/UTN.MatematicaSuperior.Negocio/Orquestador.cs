using System;
using System.Collections.Generic;

namespace UTN.MatematicaSuperior.Negocio
{
    public class Orquestador
    {
        private double _k;

        public List<double> PuntosX { get; set; } 
        public List<double> PuntosY { get; set; }


        // Ahora los puntos X e Y son propiedades, no atributos. Si hay tiempo, evaluar si conviene poner esto en el setter de la priopiedad.
        public void InicializarPuntosX(string puntosX)
        {
            PuntosX = new List<double>();

            var arrDatos = puntosX.Split(';');

            foreach (var dato in arrDatos)
            {
                var valor = double.Parse(dato);

                PuntosX.Add(valor);
            }
        }

        // Ahora los puntos X e Y son propiedades, no atributos. Si hay tiempo, evaluar si conviene poner esto en el setter de la priopiedad.
        public void InicializarPuntosY(string puntosY)
        {
            PuntosY = new List<double>();

            var arrDatos = puntosY.Split(';');

            foreach (var dato in arrDatos)
            {
                var valor = double.Parse(dato);

                PuntosY.Add(valor);
            }
        }

        public void InicializarK(string k)
        {
            _k = double.Parse(k);
        }

        public bool DatosValidos(string datos)
        {
            try
            {
                var arrDatos = datos.Split(';');

                foreach (var dato in arrDatos)
                {
                    double.Parse(dato);
                }

                return true;

            } catch (Exception)
            {
                return false;
            }
        }

        public bool DatosValidosX(string datos)
        {
            // Valido que los puntos X esten ordenados.
            var arrDatos = datos.Split(';');

            return SortTools.IsSorted(arrDatos);
        }

        public bool PuntosIngresados()
        {
            return PuntosX != null && PuntosY != null;
        }

        public string InterpolarNGProgresivo()
        {
            return NewtonGregory.Interpolar(true, PuntosX, PuntosY).ToString();
        }

        public string InterpolarNGRegresivo()
        {
            return NewtonGregory.Interpolar(false, PuntosX, PuntosY).ToString();
        }

        public string InterpolarLagrange()
        {
            return Lagrange.Interpolar(PuntosX, PuntosY).ToString();
        }

        public string EvaluarEnK()
        {
            var polinomio = NewtonGregory.Interpolar(true, PuntosX, PuntosY);

            return Evaluacion.EvaluarPolinomio(polinomio, _k).ToString();
        }

        
    }
}
