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
            var opciones = new Dictionary<ConsoleKey, Action>
            {
                {ConsoleKey.A, MenuIngresoDatos},
                {ConsoleKey.B, MostrarPasosCalculo},
                {ConsoleKey.C, EspecializarPolinomio},
                {ConsoleKey.D, AlterarValores}
            };

            var opcionSeleccionada = MenuPrincipal();

            do
            {
                Action ejecutar;

                opciones.TryGetValue(opcionSeleccionada, out ejecutar);

                if(ejecutar != null)
                {
                    ejecutar.Invoke();
                } else
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
            Console.WriteLine("Escriba la opción que desea seleccionar.");
            Console.WriteLine();
            Console.WriteLine("A. Ingreso de datos");
            Console.WriteLine("B. Mostrar pasos de cálculo");
            Console.WriteLine("C. Especializar polinomio en un valor K");
            Console.WriteLine("D. Alterar valores iniciales");
            Console.WriteLine("E. Salir");

            return Console.ReadKey().Key;
        }

        public static ConsoleKey MenuInterpolaciones()
        {
            Console.Clear();
            Console.WriteLine("Escriba la opción que desea seleccionar.");
            Console.WriteLine();
            Console.WriteLine("Interpolar por: ");
            Console.WriteLine();
            Console.WriteLine("A. Lagrange");
            Console.WriteLine("B. Newton Gregory Progresivo");
            Console.WriteLine("C. Newton Gregory Regresivo");
            Console.WriteLine("D. Volver al menú principal");

            return Console.ReadKey().Key;

        }

        public static void MenuIngresoDatos()
        {
            var opciones = new Dictionary<ConsoleKey, Func<string>>
            {
                {ConsoleKey.A, _orquestador.InterpolarLagrange},
                {ConsoleKey.B, _orquestador.InterpolarNGProgresivo},
                {ConsoleKey.C, _orquestador.InterpolarNGRegresivo}
            };

            Console.Clear();
            Console.WriteLine("Ingrese los valores de X separados por punto y coma (;) y al finalizar, presione la tecla enter. Ejemplo: 2.3;2.1;1.0");

            var valoresX = Console.ReadLine();

            if (_orquestador.DatosValidos(valoresX))
            {
                _orquestador.InicializarPuntosX(valoresX);

            } else
            {
                DatosInvalidos();
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
                DatosInvalidos();
                return;
            }

            var opcionSeleccionada = MenuInterpolaciones();

            do
            {
                opciones.TryGetValue(opcionSeleccionada, out Func<string> interpolacion);

                if (interpolacion != null)
                {
                    var resultado = interpolacion.Invoke();
                    Console.Clear();
                    Console.WriteLine("Polinomio interpolante: " + resultado);
                    Console.WriteLine();
                    Console.WriteLine("Presione una tecla para continuar.");
                    Console.ReadKey();
                }
                else
                {
                    MenuOpcionIncorrecta();                    
                }

                opcionSeleccionada = MenuInterpolaciones();
            } while (opcionSeleccionada != ConsoleKey.D);
        }        

        public static void MenuOpcionIncorrecta()
        {
            Console.Clear();
            Console.WriteLine("Opcion incorrecta, presione una tecla para volver al menú anterior.");
            Console.ReadKey();
        }

        public static void MostrarPasosCalculo()
        {
            Console.Clear();
            Console.WriteLine("Presione una tecla para volver al menú principal.");
            Console.ReadKey();
        }

        public static void EspecializarPolinomio()
        {
            Console.Clear();
            Console.WriteLine("Presione una tecla para volver al menú principal.");
            Console.ReadKey();
        }

        public static void AlterarValores()
        {
            Console.Clear();
            Console.WriteLine("Presione una tecla para volver al menú principal.");
            Console.ReadKey();
        }

        public static void DatosInvalidos()
        {
            Console.Clear();
            Console.WriteLine("Datos inválidos, presione una tecla para volver al menu principal.");
            Console.ReadKey();
        }
    }
}
