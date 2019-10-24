using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTN.MatematicaSuperior.Negocio;

namespace UTN.MatematicaSuperior.Pruebas
{
    [TestClass]
    public class EvaluarPolinomio_Tests
    {
        [TestMethod]
        public void T01_EvaluarEnTres()
        {
            List<double> coeficientes = new List<double>{ 2.3, 7.1, 1.1 };

            var polinomio = new Evaluacion(coeficientes);

            var resultado = polinomio.EvaluarPolinomio(3);

            Assert.IsTrue(resultado == 33.5);
        }

        [TestMethod]
        public void T01_EvaluarEnCeroConTerminoIndependiente()
        {
            List<double> coeficientes = new List<double> { 2.3, 7.1, 1.1 };

            var polinomio = new Evaluacion(coeficientes);

            var resultado = polinomio.EvaluarPolinomio(0);

            Assert.IsTrue(resultado == 2.3);
        }

        [TestMethod]
        public void T01_EvaluarEnCeroSinTerminoIndependiente()
        {
            List<double> coeficientes = new List<double> { 0, 7.1, 1.1 };

            var polinomio = new Evaluacion(coeficientes);

            var resultado = polinomio.EvaluarPolinomio(0);

            Assert.IsTrue(resultado == 0);
        }
    }
}
