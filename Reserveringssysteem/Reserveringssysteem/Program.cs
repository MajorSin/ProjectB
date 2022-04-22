using System;

namespace Reserveringssysteem
{
    internal class Program
    {
        public static void Prepare()
        {
            FilmController.SetFilms();
        }

        static void Main(string[] args)
        {
            Prepare();
            Router router = new();
            while (true)
            {
                router.DisplayScreen();
                Console.Clear();
            }
        }
    }
}
