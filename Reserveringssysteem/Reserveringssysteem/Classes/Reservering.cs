using System;

namespace Reserveringssysteem.Classes
{
	public class Reservering
	{
        public string Titel { get; set; }
        public int AantalPersonen { get; set; }
        public string[] Namen { get; set; }
        public string Username { get; set; }
        public int[] Leeftijden { get; set; }
        public string[] Stoelen { get; set; }
        public string UniekeCode { get; set; }
        
        public string RandomCode()
		{
            Random rand = new Random();
            string randomCode = "";
            int lengte = 7;
            for (int i = 0; i < lengte; i++)
            {
                var cijfer = rand.Next(48, 58);
                var randomGroteLetter = rand.Next(65, 91);
                var randomKleineLetter = rand.Next(97, 123);
                var keuze = rand.Next(1, 4);
                if (keuze == 1)
                {
                    randomCode += ((char)cijfer);
                } else if (keuze == 2)
                {
                    randomCode += ((char)randomGroteLetter);
                } else
                {
                    randomCode += ((char)randomKleineLetter);
                }
            }
            return randomCode;
        }
	}
}
