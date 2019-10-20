using System;
using System.Collections.Generic;

namespace UTN.MatematicaSuperior.Negocio
{
    public class Orquestador
    {
        private List<double> _puntosX;
        private List<double> _puntosY;

        public void InicializarPuntosX(string puntosX)
        {
            _puntosX = new List<double>();

            var arrDatos = puntosX.Split(';');

            foreach (var dato in arrDatos)
            {
                var valor = double.Parse(dato);

                _puntosX.Add(valor);
            }
        }

        public void InicializarPuntosY(string puntosY)
        {
            _puntosY = new List<double>();

            var arrDatos = puntosY.Split(';');

            foreach (var dato in arrDatos)
            {
                var valor = double.Parse(dato);

                _puntosY.Add(valor);
            }
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

            } catch (Exception ex)
            {
                return false;
            }
        }

        public string AproximacionPorNewtonGregoryProgresivo()
        {
            throw new NotImplementedException();
        }

        public string AproximacionPorNewtonGregoryRegresivo()
        {
            throw new NotImplementedException();
        }

        public string AproximacionPorLagrange()
        {
            throw new NotImplementedException();
        }
    }
}
