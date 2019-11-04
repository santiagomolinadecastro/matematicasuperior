using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UTN.MatematicaSuperior.Negocio;

namespace UTN.MatematicaSuperior.Pruebas
{
    [TestClass]
    public class Lagrange_Tests
    {
        private List<double> xList, yList;

        [TestMethod]
        public void Lagrange_1()
        {
            xList = new List<double> { -5, -1, 3, 5 };
            yList = new List<double> { 333, -3, -83, -387 };

            string interpolacion = Lagrange.Interpolar(xList, yList, out string pasos).ToString();

            Assert.AreEqual("-3x^3-x^2+3x-2", interpolacion);
        }

        [TestMethod]
        public void Lagrange_2()
        {
            xList = new List<double> { 1, 2, 4 };
            yList = new List<double> { 1, 4, 16 };

            string interpolacion = Lagrange.Interpolar(xList, yList, out string pasos).ToString();

            Assert.AreEqual("x^2", interpolacion);
        }
    }
}
