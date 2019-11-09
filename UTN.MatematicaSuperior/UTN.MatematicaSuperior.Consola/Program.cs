using System;
using System.Collections.Generic;
using UTN.MatematicaSuperior.Negocio;

namespace UTN.MatematicaSuperior.Consola
{
    class Program
    {
        private static Orquestador _orquestador;
        private static readonly string escribaOpcion = "Escriba la opción que desea seleccionar.";
        private static readonly string menuAnterior = "Presione una tecla para volver al menú anterior.";

        static void Main(string[] args)
        {
            _orquestador = new Orquestador();
            var opciones = new Dictionary<ConsoleKey, Action>
            {
                {ConsoleKey.A, MenuIngresoDatos}
            };

            var opcionSeleccionada = MenuPrincipal();

            while (opcionSeleccionada != ConsoleKey.B)
            {
                opciones.TryGetValue(opcionSeleccionada, out Action ejecutar);

                if (ejecutar != null)
                {
                    ejecutar.Invoke();
                }
                else
                {
                    MenuOpcionIncorrecta();
                }

                opcionSeleccionada = MenuPrincipal();
            }
        }

        public static ConsoleKey MenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("======");
            Console.WriteLine("FINTER");
            Console.WriteLine("======");
            Console.WriteLine();
            Console.WriteLine(escribaOpcion);
            Console.WriteLine();
            Console.WriteLine("A. Ingreso de datos");
            Console.WriteLine("B. Salir");

            return Console.ReadKey().Key;
        }

        public static ConsoleKey IngresarMenuInterpolaciones()
        {
            Console.Clear();
            Console.WriteLine(escribaOpcion);
            Console.WriteLine();
            Console.WriteLine("Interpolar por: ");
            Console.WriteLine();
            Console.WriteLine("A. Lagrange");
            Console.WriteLine("B. Newton Gregory Progresivo");
            Console.WriteLine("C. Newton Gregory Regresivo");
            Console.WriteLine("D. Volver al menú principal");

            return Console.ReadKey().Key;
        }


        public static ConsoleKey IngresarMenuPosteriorInterpolaciones()
        {
            Console.WriteLine(escribaOpcion);
            Console.WriteLine();
            Console.WriteLine("A. Mostrar pasos de cálculo");
            Console.WriteLine("B. Especializar polinomio en un valor K");
            Console.WriteLine("C. Alterar valores iniciales");
            Console.WriteLine("D. Volver al menú anterior");

            return Console.ReadKey().Key;
        }

        public static ConsoleKey MenuAlterarValores()
        {
            Console.WriteLine();
            Console.WriteLine(escribaOpcion);
            Console.WriteLine();
            Console.WriteLine("A. Agregar puntos.");
            Console.WriteLine("B. Eliminar puntos.");
            Console.WriteLine("C. Volver al menú principal.");

            return Console.ReadKey().Key;
        }

        public static void MenuIngresoDatos()
        {
            Console.Clear();
            IngresoDatos(OpcionPuntosEnum.Inicializar);
        }

        public static void MenuInterpolaciones()
        {
            var opcionesInterpolacion = new Dictionary<ConsoleKey, Func<string>>
            {
                {ConsoleKey.A, _orquestador.InterpolarLagrange},
                {ConsoleKey.B, _orquestador.InterpolarNGProgresivo},
                {ConsoleKey.C, _orquestador.InterpolarNGRegresivo}
            };
            var opcionSeleccionada = IngresarMenuInterpolaciones();


            while (opcionSeleccionada != ConsoleKey.D)
            {
                opcionesInterpolacion.TryGetValue(opcionSeleccionada, out Func<string> interpolacion);

                if (interpolacion != null)
                {
                    var resultado = interpolacion.Invoke();
                    Console.Clear();
                    Console.WriteLine("Polinomio interpolante: " + resultado);
                    Console.WriteLine();
                    Console.WriteLine();

                    MenuPosteriorInterpolaciones(resultado);
                }
                else
                {
                    MenuOpcionIncorrecta();
                }

                opcionSeleccionada = IngresarMenuInterpolaciones();
            }
        }

        public static void MenuPosteriorInterpolaciones(string resultado)
        {
            var opcionesPosteriorInterpolacion = new Dictionary<ConsoleKey, Action>
            {
                {ConsoleKey.A, MostrarPasosCalculo},
                {ConsoleKey.B, EspecializarPolinomio},
                {ConsoleKey.C, AlterarValores}
            };
            var opcionSeleccionada = IngresarMenuPosteriorInterpolaciones();

            while (opcionSeleccionada != ConsoleKey.D)
            {
                opcionesPosteriorInterpolacion.TryGetValue(opcionSeleccionada, out Action posteriorInterpolacion);

                if (posteriorInterpolacion != null)
                {
                    posteriorInterpolacion.Invoke();
                }
                else
                {
                    MenuOpcionIncorrecta();
                }

                Console.Clear();
                Console.WriteLine("Polinomio interpolante: " + resultado);
                Console.WriteLine();
                Console.WriteLine();

                opcionSeleccionada = IngresarMenuPosteriorInterpolaciones();
            }
        }

        public static void IngresoDatos(OpcionPuntosEnum opcion)
        {
            var opcionesX = new Dictionary<OpcionPuntosEnum, Action<string>>
            {
                {OpcionPuntosEnum.Inicializar, _orquestador.InicializarPuntosX},
                {OpcionPuntosEnum.Agregar, _orquestador.AgregarPuntosX},
                {OpcionPuntosEnum.Eliminar, _orquestador.EliminarPuntos},
            };

            var opcionesY = new Dictionary<OpcionPuntosEnum, Action<string>>
            {
                {OpcionPuntosEnum.Inicializar, _orquestador.InicializarPuntosY},
                {OpcionPuntosEnum.Agregar, _orquestador.AgregarPuntosY}
            };

            Action<string> ejecutar;

            Console.WriteLine("Ingrese los valores de X separados por punto y coma (;) y al finalizar, presione la tecla enter. Ejemplo: 2.3;2.1;1.0");

            var valoresX = Console.ReadLine();

            if (!_orquestador.DatosValidos(valoresX))
            {
                DatosInvalidos();
                return;
            }

            if (!_orquestador.DatosOrdenados(valoresX))
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("Los valores de las x deben estar ordenados. " + menuAnterior);
                Console.WriteLine();
                Console.ReadKey();
                return;
            }

            opcionesX.TryGetValue(opcion, out ejecutar);

            try
            {
                ejecutar.Invoke(valoresX);
            }
            catch (ValorRepetidoException)
            {
                Console.WriteLine();
                Console.WriteLine("Los valores de x no pueden estar repetidos. " + menuAnterior);
                Console.WriteLine();
                Console.ReadKey();
                return;
            }

            if (opcion != OpcionPuntosEnum.Eliminar)
            {
                Console.Clear();
                Console.WriteLine("Ingrese los valores de Y separados por punto y coma (;) y al finalizar, presione la tecla enter. Ejemplo: 2.3;2.1;1.0");

                var valoresY = Console.ReadLine();

                if (_orquestador.DatosValidos(valoresY))
                {
                    opcionesY.TryGetValue(opcion, out ejecutar);

                    ejecutar.Invoke(valoresY);
                }
                else
                {
                    DatosInvalidos();
                    return;
                }
            }

            MenuInterpolaciones();
        }

        public static void MostrarPasosCalculo()
        {
            Console.Clear();
            Console.WriteLine(_orquestador.Pasos);
            Console.WriteLine("\n------------------------------");
            Console.WriteLine(_orquestador.DatosExtra);
            Console.WriteLine();
            Console.WriteLine(menuAnterior);
            Console.ReadKey();
        }

        public static void EspecializarPolinomio()
        {
            Console.Clear();

            if (_orquestador.PuntosIngresados() && _orquestador.PolinomioInterpolado())
            {
                Console.WriteLine("Ingrese el valor de k a especializar: ");

                var k = Console.ReadLine();

                if (_orquestador.DatosValidos(k))
                {
                    _orquestador.InicializarK(k);

                }

                var resultado = _orquestador.EvaluarEnK();
                Console.WriteLine("El valor del polinomio especializado es: " + resultado);
            }
            else
            {
                Console.WriteLine("Por favor, ingresar los puntos para interpolar el polinomio.");
            }

            Console.WriteLine();
            Console.WriteLine("Presione una tecla para continuar.");
            Console.ReadKey();
        }

        public static void AlterarValores()
        {
            var opcionesInterpolacion = new Dictionary<ConsoleKey, Action>
            {
                {ConsoleKey.A, AgregarPuntos},
                {ConsoleKey.B, EliminarPuntos}
            };
            
            Console.Clear();
            Console.WriteLine("Valores ingresados previamente:");
            Console.Write("X: ");

            foreach (var puntoX in _orquestador.PuntosX)
            {
                Console.Write(puntoX + ";");
            }

            Console.WriteLine("");
            Console.Write("Y: ");

            foreach (var puntoY in _orquestador.PuntosY)
            {
                Console.Write(puntoY + ";");
            }

            var opcionSeleccionada = MenuAlterarValores();

            while (opcionSeleccionada != ConsoleKey.C)
            {
                opcionesInterpolacion.TryGetValue(opcionSeleccionada, out Action accion);

                if (accion != null)
                {
                    accion.Invoke();
                }

                Console.Clear();
                opcionSeleccionada = MenuAlterarValores();
            } 
        }

        public static void AgregarPuntos()
        {
            Console.Clear();
            IngresoDatos(OpcionPuntosEnum.Agregar);
        }

        public static void EliminarPuntos()
        {
            Console.Clear();
            IngresoDatos(OpcionPuntosEnum.Eliminar);
        }

        public static void DatosInvalidos()
        {
            Console.Clear();
            Console.WriteLine("Datos inválidos, presione una tecla para volver al menu principal.");
            Console.ReadKey();
        }

        public static void MenuOpcionIncorrecta()
        {
            Console.Clear();
            Console.WriteLine("Opcion incorrecta, presione una tecla para volver al menú anterior.");
            Console.ReadKey();
        }
    }
}
