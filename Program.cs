namespace Ahorcado_Juego
{
    internal class Program
    {
        // ----------------- VARIABLES -----------------------------------------------------------

        private static char[,] tablero = new char[3, 3];
        private static int intentosMaximos = 6;
        private static int intentosFallidos = 0;
        private static bool partidaEnJuego = true;
        private static string palabra = palabraAleatoria();
        private static string palabraEnBlanco = new string('↑', palabra.Length);

        // ----------------- FUNCTION MAIN -------------------------------------------------------
        static void Main(string[] args)
        {
            empezarJuego();
            logicaDelJuego();
            Console.ReadLine();
        }

        // ----------------- TABLERO VISUAL ---------------------------------------------------------
        static string tableroVisual(char[,] tablero)
        {
            string horca = "";
            horca += "   ┌────┐" + Environment.NewLine;
            horca += "   │     " + Environment.NewLine;
            horca += "   │     " + Environment.NewLine;
            horca += "   │     " + Environment.NewLine;
            horca += "   │     " + Environment.NewLine;
            horca += "───└──── " + Environment.NewLine;

            return horca;
        }

        // ----------------- EMPEZAR JUEGO ----------------------------------------------------------

        static void empezarJuego()
        {
            Console.WriteLine("¡Bienvenido al juego del ahorcado!");
            Console.WriteLine("----------------------------------");
            Console.WriteLine(tableroVisual(tablero));
            Console.WriteLine(palabraEnBlanco);
            Console.WriteLine();
        }

        // ----------------- PALABRA ALEATORIA ------------------------------------------------------
        static string palabraAleatoria()
        {
            string[] palabras = new string[]
            { "perro", "gato", "coche", "casa"};

            Random random = new Random();

            int aleatorio = random.Next(0, palabras.Length);

            return palabras[aleatorio].ToUpper();
        }

        // ---------------- PONER LETRA -------------------------------------------------------------
        static char ponerLetra(string palabraEnBlanco)
        {
            char letra = ' ';
            bool entradaValida = false;

            do
            {
                Console.WriteLine("Escribe una letra:");
                Console.WriteLine();
                string input = Console.ReadLine().ToUpper();

                if (input.Length == 1 && char.IsLetter(input[0]))
                {
                    letra = input[0];
                    entradaValida = true;


                }
                else
                {
                    Console.WriteLine("Entrada no válida. Por favor ingrese solo una letra.");
                    Console.WriteLine();
                };

            } while (!entradaValida);

            return letra;

        }


        // ----------------- VERIFICAR LETRAS -------------------------------------------------------
        static string verificarLetra(string palabra, char letra, string palabraEnBlanco)
        {
            char[] palabraEnBlancoArray = palabraEnBlanco.ToCharArray();

            for (int i = 0; i < palabra.Length; i++)
            {

                if (palabra[i] == letra)
                {
                    palabraEnBlancoArray[i] = letra;
                }
            }
            return new string (palabraEnBlancoArray);
        }


        // --------------- ETAPAS DEL JUEGO ---------------------------------------------------------
        static string tableroVisual(char[,] tablero, int intentosFallidos)
        {
            string[] etapasHorca = new string[]
            {
                "   ┌────┐" + Environment.NewLine +
                "   │     " + Environment.NewLine +
                "   │     " + Environment.NewLine +
                "   │     " + Environment.NewLine +
                "   │     " + Environment.NewLine +
                "───└──── " + Environment.NewLine,

                "   ┌────┐" + Environment.NewLine +
                "   │    O" + Environment.NewLine +
                "   │     " + Environment.NewLine +
                "   │     " + Environment.NewLine +
                "   │     " + Environment.NewLine +
                "───└──── " + Environment.NewLine,

                "   ┌────┐" + Environment.NewLine +
                "   │    O" + Environment.NewLine +
                "   │    |" + Environment.NewLine +
                "   │     " + Environment.NewLine +
                "   │     " + Environment.NewLine +
                "───└──── " + Environment.NewLine,

                "   ┌────┐" + Environment.NewLine +
                "   │    O" + Environment.NewLine +
                "   │   /|" + Environment.NewLine +
                "   │     " + Environment.NewLine +
                "   │     " + Environment.NewLine +
                "───└──── " + Environment.NewLine,

                "   ┌────┐" + Environment.NewLine +
                "   │    O" + Environment.NewLine +
                "   │   /|\\" + Environment.NewLine +
                "   │     " + Environment.NewLine +
                "   │     " + Environment.NewLine +
                "───└──── " + Environment.NewLine,

                "   ┌────┐" + Environment.NewLine +
                "   │    O" + Environment.NewLine +
                "   │   /|\\" + Environment.NewLine +
                "   │   / " + Environment.NewLine +
                "   │     " + Environment.NewLine +
                "───└──── " + Environment.NewLine,

                "   ┌────┐" + Environment.NewLine +
                "   │    O" + Environment.NewLine +
                "   │   /|\\" + Environment.NewLine +
                "   │   / \\" + Environment.NewLine +
                "   │     " + Environment.NewLine +
                "───└──── " + Environment.NewLine
            };

            return etapasHorca[Math.Min(intentosFallidos, etapasHorca.Length - 1)];
        }

        //------------------ REINICIAR JUEGO-------------------------------------------------------

        static void ReiniciarJuego()
        {
            Console.WriteLine("Presiona 'R' para restablecer o cualquier otra tecla para salir.");
            char respuesta = Console.ReadKey().KeyChar;
            if (respuesta == 'R' || respuesta == 'r')
            {
                // Reinicia las variables y el juego
                Console.Clear();
                intentosFallidos = 0;
                palabra = palabraAleatoria();
                palabraEnBlanco = new string('↑', palabra.Length);

                partidaEnJuego = true;
                empezarJuego();
            }
            else
            {
                partidaEnJuego = false;
            }
        }

        // ----------------- JUEGO EN CURSO ---------------------------------------------------------
        static void juegoEnCurso()
        {
            Console.WriteLine("¡Bienvenido al juego del ahorcado!");
            Console.WriteLine("----------------------------------");
            Console.WriteLine($"Intentos fallidos {intentosFallidos}/{intentosMaximos}");
            Console.WriteLine(tableroVisual(tablero, intentosFallidos));
            Console.WriteLine();
            Console.WriteLine(palabraEnBlanco);
        }

        // ----------------- LOGICA DEL JUEGO ---------------------------------------------------------

        static void logicaDelJuego()
        {
            while (partidaEnJuego)
            {
                string letrasErroneas;

                char letra = ponerLetra(palabraEnBlanco);
                Console.WriteLine();

                if (palabra.Contains(letra.ToString()))
                {
                    palabraEnBlanco = verificarLetra(palabra, letra, palabraEnBlanco);
                    Console.Clear();

                    juegoEnCurso();

                    Console.WriteLine();

                    if (!palabraEnBlanco.Contains('↑'))
                    {
                        Console.WriteLine();
                        Console.WriteLine("¡GANASTE!");
                        Console.WriteLine();
                        partidaEnJuego = false;
                        ReiniciarJuego();

                        
                    }
                }
                else
                {
                    intentosFallidos++;
                    Console.Clear();

                    juegoEnCurso();

                    Console.WriteLine();


                    if (intentosFallidos >= intentosMaximos)
                    {
                        Console.WriteLine("Perdiste!");
                        Console.WriteLine();
                        Console.WriteLine($"La palabra correcta era: {palabra}");
                        partidaEnJuego = false;
                        ReiniciarJuego();

                    }

                }

            }

        }


        
    }
}