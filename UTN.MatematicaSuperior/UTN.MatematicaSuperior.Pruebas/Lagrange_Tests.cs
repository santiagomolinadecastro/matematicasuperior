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
        public void Lagrange_()
        {
            xList = new List<double> { -5, -1, 3, 5 };
            yList = new List<double> { 333, -3, -83, -387 };

            string interpolacion = Lagrange.Interpolar(xList, yList);

            Assert.AreEqual("-3x^3-x^2+3x-2", interpolacion);
        }
    }
}
