using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using UTN.MatematicaSuperior.Negocio;
using Extreme.Mathematics.Curves;

namespace UTN.MatematicaSuperior.Pruebas
{
    [TestClass]
    public class EvaluarPolinomio_Tests
    {
        [TestMethod]
        public void T01_EvaluarEnTres()
        {
            Polynomial polinomio = new Polynomial(new double[] { 2.3, 7.1, 1.1 });

            var resultado = Evaluacion.EvaluarPolinomio(polinomio,3);

            Assert.IsTrue(resultado == 33.5);
        }

        [TestMethod]
        public void T01_EvaluarEnCeroConTerminoIndependiente()
        {
            Polynomial polinomio = new Polynomial(new double[] { 2.3, 7.1, 1.1 });

            var resultado = Evaluacion.EvaluarPolinomio(polinomio, 0);

            Assert.IsTrue(resultado == 2.3);
        }

        [TestMethod]
        public void T01_EvaluarEnCeroSinTerminoIndependiente()
        {
            Polynomial polinomio = new Polynomial(new double[] { 0, 7.1, 1.1 });

            var resultado = Evaluacion.EvaluarPolinomio(polinomio, 0);

            Assert.IsTrue(resultado == 0);
        }
    }
}
