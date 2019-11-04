﻿using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UTN.MatematicaSuperior.Negocio;

namespace UTN.MatematicaSuperior.Pruebas
{
    [TestClass]
    public class NewtonGregory_Progresivo_Tests
    {
        private List<double> xList, yList;

        [TestMethod]
        public void NewtonGregoryProgresivo1()
        {
            xList = new List<double> {1, 2, 4};
            yList = new List<double>{1, 8, 64};

            string interpolacion = NewtonGregory.Interpolar(true, xList, yList, out string pasos).ToString();

            Assert.AreEqual("7x^2-14x+8", interpolacion);
        }

        [TestMethod]
        public void NewtonGregoryProgresivo2()
        {
            xList = new List<double>{-3, -1, 1, 3, 5, 7, 9};
            yList = new List<double>{39, 19, -21, -57, -65, -21, 99};

            string interpolacion = NewtonGregory.Interpolar(true, xList, yList, out string pasos).ToString();

            Assert.AreEqual("0,5x^3-x^2-20,5x", interpolacion);
        }

        [TestMethod]
        public void NewtonGregoryProgresivo3()
        {
            xList = new List<double> {1, 3, 4, 5, 7};
            yList = new List<double> {1, 3, 13, 37, 151};

            string interpolacion = NewtonGregory.Interpolar(true, xList, yList, out string pasos).ToString();

            Assert.AreEqual("x^3-5x^2+8x-3", interpolacion);
        }
    }
}
