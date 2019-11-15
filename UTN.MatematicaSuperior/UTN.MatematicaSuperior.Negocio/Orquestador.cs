using System;
using System.Collections.Generic;
using Extreme.Mathematics.Curves;

namespace UTN.MatematicaSuperior.Negocio
{
    public class Orquestador
    {
        private double _k;
        private Polynomial polinomio;
        public List<double> PuntosX { get; set; } 
        public List<double> PuntosY { get; set; }
        public string Pasos { get; set; }

        public string DatosExtra { get; set; }

        // Ahora los puntos X e Y son propiedades, no atributos. Si hay tiempo, evaluar si conviene poner esto en el setter de la priopiedad.
        public void InicializarPuntosX(string puntos)
        {
            PuntosX = new List<double>();
            AgregarPuntos(puntos, true);
        }

        // Ahora los puntos X e Y son propiedades, no atributos. Si hay tiempo, evaluar si conviene poner esto en el setter de la priopiedad.
        public void InicializarPuntosY(string puntos)
        {
            PuntosY = new List<double>();
            AgregarPuntos(puntos, false);
        }

        //public void EditarPunto(string puntoAEditar, string puntoX, string puntoY)
        //{
        //    var i = int.Parse(puntoAEditar);
        //    var x = double.Parse(puntoX);
        //    var y = double.Parse(puntoY);

        //    PuntosX[i] = x;
        //    PuntosY[i] = y;
        //}

        public void AgregarPuntosX(string puntos)
        {
            AgregarPuntos(puntos, true);
        }

        public void AgregarPuntosY(string puntos)
        {
            AgregarPuntos(puntos, false);
        }

        private void AgregarPuntos(string puntos, bool sonX)
        {
            var arrDatos = puntos.Split(';');

            foreach (var dato in arrDatos)
            {
                var valor = double.Parse(dato);

                if (sonX)
                {
                    if (PuntosX.Contains(valor))
                    {
                        throw new ValorRepetidoException();
                    }

                    PuntosX.Add(valor);
                }
                else
                {
                    PuntosY.Add(valor);
                }
            }
        }

        public void EliminarPuntos(string puntosX)
        {
            var arrDatos = puntosX.Split(';');

            foreach (var dato in arrDatos)
            {
                var valor = double.Parse(dato);

                if (PuntosX.Contains(valor))
                {
                    var posicion = PuntosX.IndexOf(valor);
                    PuntosX.RemoveAt(posicion);
                    PuntosY.RemoveAt(posicion);
                }
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

        public bool DatosOrdenados(string datos)
        {
            // Valido que los puntos X esten ordenados.
            var arrDatos = datos.Split(';');

            double[] arrDouble =new double[arrDatos.Length];

            int i = 0;

            foreach (var dato in arrDatos)
            {
                var valor = double.Parse(dato);
                arrDouble[i] = valor;

                i++;
            }

            return SortTools.IsSorted(arrDouble);
        }

        public bool PuntosIngresados()
        {
            return PuntosX != null && PuntosY != null;
        }

        public bool PolinomioInterpolado()
        {
            // Dice si ya se interpoló, útil si hay q especializar en k
            return polinomio != null;
        }

        public string InterpolarNGProgresivo()
        {
            polinomio = NewtonGregory.Interpolar(true, PuntosX, PuntosY, out string pasos);
            Pasos = pasos;
            ActualizarDatosExtra(polinomio);

            return polinomio.ToString();
        }

        public string InterpolarNGRegresivo()
        {
            polinomio = NewtonGregory.Interpolar(false, PuntosX, PuntosY, out string pasos);
            Pasos = pasos;
            ActualizarDatosExtra(polinomio);

            return polinomio.ToString();
        }

        public string InterpolarLagrange()
        {
            polinomio = Lagrange.Interpolar(PuntosX, PuntosY, out string pasos);
            Pasos = pasos;
            ActualizarDatosExtra(polinomio);

            return polinomio.ToString();
        }

        public string EvaluarEnK()
        {
            return Evaluacion.EvaluarPolinomio(polinomio, _k).ToString();
        }

        private void ActualizarDatosExtra(Polynomial polinomio)
        {
            DatosExtra = "\n\nGrado del polinomio: " + polinomio.Degree + "\n\n";

            if (SortTools.IsEquidistant(PuntosX.ToArray()))
                DatosExtra += "Los puntos son equidistantes";
            else
                DatosExtra += "Los puntos no son equidistantes";
        }

        
    }
}
