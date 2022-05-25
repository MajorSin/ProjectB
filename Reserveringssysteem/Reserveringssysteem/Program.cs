using System;

namespace Reserveringssysteem
{
    internal class Program
    {
        public static void Prepare()
        {
            MemberController.SetMembers();
            FilmController.SetFilms();
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
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
