using System;
using System.Collections.Generic;
using UTN.MatematicaSuperior.Negocio;

namespace UTN.MatematicaSuperior.Consola
{
    class Program
    {
        private static Orquestador _orquestador;

        static void Main(string[] args)
        {
            _orquestador = new Orquestador();

            var opcionSeleccionada = MenuPrincipal();

            do
            {
                // No me gustan los IF acá, si hay tiempo habría que cambiarlo.
                if (opcionSeleccionada == ConsoleKey.A)
                {
                    MenuIngresoDatos();

                } else if (opcionSeleccionada == ConsoleKey.B)
                {
                    // Mostrar pasos de cálculo.
                    Console.Clear();
                    Console.WriteLine("Presione una tecla para volver al menú principal.");
                    Console.ReadKey();
                }
                else if (opcionSeleccionada == ConsoleKey.C)
                {
                    // Especializar polinomio en un valor K.
                    Console.Clear();
                    Console.WriteLine("Presione una tecla para volver al menú principal.");
                    Console.ReadKey();
                }
                else if (opcionSeleccionada == ConsoleKey.D)
                {
                    // Alterar valores iniciales.
                    Console.Clear();
                    Console.WriteLine("Presione una tecla para volver al menú principal.");
                    Console.ReadKey();
                }
                else if (opcionSeleccionada != ConsoleKey.E)
                {
                    MenuOpcionIncorrecta();
                }

                opcionSeleccionada = MenuPrincipal();

            } while (opcionSeleccionada != ConsoleKey.E);
        }

        public static ConsoleKey MenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("======");
            Console.WriteLine("FINTER");
            Console.WriteLine("======");
            Console.WriteLine();
            Console.WriteLine("Escriba el número de la opcion que desea seleccionar.");
            Console.WriteLine();
            Console.WriteLine("A. Ingreso de datos");
            Console.WriteLine("B. Mostrar pasos de cálculo");
            Console.WriteLine("C. Especializar polinomio en un valor K");
            Console.WriteLine("D. Alterar valores iniciales");
            Console.WriteLine("E. Salir");

            return Console.ReadKey().Key;
        }

        public static void MenuIngresoDatos()
        {
            // Ingreso de datos.
            Console.Clear();
            Console.WriteLine("Ingrese los valores de X separados por punto y coma (;) y al finalizar, presione la tecla enter. Ejemplo: 2.3;2.1;1.0");

            var valoresX = Console.ReadLine();

            if (_orquestador.DatosValidos(valoresX))
            {

                _orquestador.InicializarPuntosX(valoresX);

            } else
            {
                Console.Clear();
                Console.WriteLine("Datos inválidos, presione una tecla para volver al menu principal.");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("Ingrese los valores de Y separados por punto y coma (;) y al finalizar, presione la tecla enter. Ejemplo: 2.3;2.1;1.0");

            var valoresY = Console.ReadLine();

            if (_orquestador.DatosValidos(valoresY))
            {
                _orquestador.InicializarPuntosY(valoresY);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Datos inválidos, presione una tecla para volver al menu principal.");
                Console.ReadKey();
                return;
            }
        }

        public static void MenuOpcionIncorrecta()
        {
            Console.Clear();
            Console.WriteLine("Opcion incorrecta, presione una tecla para volver al menú principal.");
            Console.ReadKey();
        }
    }
}
