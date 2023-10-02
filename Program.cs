using System;
using System.Collections.Generic;
using System.Linq;

namespace Ahorcado_Juego
{
    internal class Program
    {
        // ----------------- VARIABLES -----------------------------------------------------------

        private static char[,] tablero = new char[3, 3];
        private static int intentosMaximos = 6;
        private static int intentosFallidos = 0;
        private static bool partidaEnJuego = true;
        private static string palabra = "";
        private static string palabraEnBlanco = "";
        private static HashSet<string> palabrasUsadas = new HashSet<string>();
        private static Stack<char> letrasErroneas = new Stack<char>();
        


    // ----------------- FUNCTION MAIN -------------------------------------------------------
    static void Main(string[] args)
        {
            
            palabra = palabraAleatoria();
            palabraEnBlanco = new string('↑', palabra.Length);

            empezarJuego();
            logicaDelJuego();
            mostrarLetrasErroneas();



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
            Console.WriteLine("---------- By: JoseAgraz ---------");
            Console.WriteLine(tableroVisual(tablero));
            Console.WriteLine(palabraEnBlanco);
            Console.WriteLine();
        }

        // ----------------- PALABRA ALEATORIA ------------------------------------------------------
        static string palabraAleatoria()
        {
            string[] palabras = new string[]
            {
                "Manzana", "Banana", "Perro", "Gato", "Casa", "Coche", "Pelota", "Arbol", "Sol", "Luna",
                "Libro", "Computadora", "Café", "Pizza", "Playa", "Rio", "Guitarra", "Pintura", "Telefono", "Amigo",
                "Familia", "Trabajo", "Escuela", "Deporte", "Viaje", "Musica", "Cine", "Ciencia", "Aventura", "Verano",
                "Invierno", "Primavera", "Otoño", "Amor", "Paz", "Felicidad", "Tiempo", "Internet", "Fiesta", "Dinero",
                "Naturaleza", "Arte", "Comida", "Historia", "Religion", "Politica", "Tecnologia", "Animales", "Flor",
                "Camino", "Viento", "Cielo", "Aire", "Almohada", "Cepillo", "Botella", "Silla", "Zapato", "Bolsa",
                "Jardin", "Juego", "Hermano", "Hermana", "Profesor", "Estudiante", "Medicina", "Rey", "Reina",
                "Cuento", "Carta", "Regalo", "Piedra", "Raton", "Papel", "Reloj", "Tijeras", "Lapiz", "Rosa",
                "Tigre", "Leon", "Elefante", "Tortuga", "Pajaro", "Mariposa", "Serpiente", "Rana", "Pez",
                "Escorpion", "Dragon", "Hormiga", "Mosquito", "Estrella", "Planeta", "Galaxia", "Universo",
                "Circulo", "Cuadrado", "Triangulo", "Rectangulo", "Piramide", "Esfera", "Cubo", "Cono",
                "Madera", "Metal", "Plastico", "Vidrio" 
            };

            Random random = new Random();

            string palabraFiltrada; 

            do
            {
                int aleatorio = random.Next(0, palabras.Length);
                palabraFiltrada = palabras[aleatorio].ToUpper();

                palabraFiltrada = palabraFiltrada.Replace("Á", "A");
                palabraFiltrada = palabraFiltrada.Replace("É", "E");
                palabraFiltrada = palabraFiltrada.Replace("Í", "I");
                palabraFiltrada = palabraFiltrada.Replace("Ó", "O");
                palabraFiltrada = palabraFiltrada.Replace("Ú", "U");

            } while (palabrasUsadas.Contains(palabraFiltrada));

            palabrasUsadas.Add(palabraFiltrada); // Agregar la palabra a la lista de palabras usadas

            return palabraFiltrada;

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

            bool letraEncontrada = false;

            for (int i = 0; i < palabra.Length; i++)
            {

                if (palabra[i] == letra)
                {
                    palabraEnBlancoArray[i] = letra;
                    letraEncontrada = true;
                }
            }

            if (!letraEncontrada)
            {
                letrasErroneas.Push(letra);
            }

            
            return new string (palabraEnBlancoArray);
        }

        // --------------- LETRAS ERRONEAS ---------------------------------------------------------

        static void mostrarLetrasErroneas()
        {
            if (letrasErroneas.Count >= 0)
            {
                var letrasErroneasComoString = new List<string>();
                foreach (char letra in letrasErroneas)
                {
                    letrasErroneasComoString.Add(letra.ToString());
                }

                Console.WriteLine("Letras incorrectas: " + string.Join(", ", letrasErroneasComoString));
            }
            else
            {
                Console.WriteLine("");
            }
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
            Console.WriteLine("---------- By: JoseAgraz ---------");
            Console.WriteLine();

            mostrarLetrasErroneas();

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

    }
}