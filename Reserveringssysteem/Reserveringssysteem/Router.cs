using System;

namespace Reserveringssysteem
{
    public class Router
    {
        public bool IsLoggedIn { get; set; }
        public string CurrentScreen { get; set; }

        // Toont het scherm waar je nu op bent.
        public void displayScreen()
        {
            switch (CurrentScreen)
            {
                case "Authorizatie":
                    displayAuthorization();
                    break;
                case "Inloggen":
                    displayInloggen();
                    break;
                case "Registreren":
                    displayRegistreren();
                    break;
                case "Home":
                    displayHome();
                    break;
                case "Reserveren":
                    displayReserveren();
                    break;
                case "Films":
                    displayFilms();
                    break;
                case "Eten & Drinken":
                    displayEtenDrinken();
                    break;
                case "Informatie":
                    displayInformatie();
                    break;
                default:
                    CurrentScreen = "Authorizatie";
                    displayAuthorization();
                    break;
            }
        }

        // Keuze van de gebruiker vaststellen.
        private int awaitResponse(string[] options)
        {
            int currentSelected = 0;
            bool selectionMade = false;

            // Loopt door de opties en houdt bij welke keuze je maakt met pijltjestoetsen.
            while (!selectionMade)
            {
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == currentSelected)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        Console.Write(" ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write(" ");
                    }
                    Console.WriteLine("  {0}", options[i]);
                    Console.ResetColor();
                }
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        if (currentSelected == 0)
                        {
                            break;
                        }
                        else
                        {
                            currentSelected -= 1;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (currentSelected == options.Length - 1)
                        {
                            break;
                        }
                        else
                        {
                            currentSelected += 1;
                        }
                        break;
                    case ConsoleKey.Enter:
                        selectionMade = true;
                        break;
                }
                // Zorgt ervoor dat de keuzes niet met elkaar gaan overlappen.
                Console.CursorTop = Console.CursorTop - options.Length;
            }
            return currentSelected;
        }

        // Authorisatie scherm.
        private void displayAuthorization()
        {
            ConsoleColor color = ConsoleColor.White;
            string title = @"
              __________
           ///^^^^{}^^^^\\\
         //..@----------@..\\
        ///&%&%&%&/\&%&%&%&\\\
        ||&%&%&_.'  '._&%&%&||
        ||&%'''        '''%&||
        ||&%&  Bioscoop  &%&||
        ||&%&             %&||
        ||&%&            &%&||
  ______||&%&&==========&&%&||______
                ";
            string underline = "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n";
            showHeader(color, title, underline);

            Console.WriteLine(" Gebruik pijltjestoetsen ↑ en ↓ om te navigeren\n en druk enter om een optie te kiezen.\n");

            string[] options = new string[]
            {
                    IsLoggedIn ? "Uitloggen" : "Inloggen",
                    "Registreren",
                    "Doorgaan als gast",
                    "Beeïndigen",
            };

            int choice = awaitResponse(options);

            switch (choice)
            {
                case 0:
                    if (IsLoggedIn)
                    {
                        IsLoggedIn = false;
                        CurrentScreen = "Authorizatie";
                    } 
                    else { 
                        CurrentScreen = "Inloggen";
                    }
                    break;
                case 1:
                    CurrentScreen = "Registreren";
                    break;
                case 2:
                    CurrentScreen = "Home";
                    IsLoggedIn = false;
                    break;
                default:
                    Console.Clear();
                    Environment.Exit(0);
                    break;
            }
            Console.Clear();
            displayScreen();
        }

        // Lay-out van de header teruggeven.
        private void showHeader(ConsoleColor color, string title, string underline)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(@"{0}", title);
            Console.BackgroundColor = color;
            Console.WriteLine(underline);
            Console.ResetColor();
        }

        // Inlog scherm.
        private void displayInloggen()
        {
            ConsoleColor color = ConsoleColor.Cyan;
            string title = @"
   _____           _                                        
  |_   _|         | |                                       
    | |    _ __   | |   ___     __ _    __ _    ___   _ __  
    | |   | '_ \  | |  / _ \   / _` |  / _` |  / _ \ | '_ \ 
   _| |_  | | | | | | | (_) | | (_| | | (_| | |  __/ | | | |
  |_____| |_| |_| |_|  \___/   \__, |  \__, |  \___| |_| |_|
                               __/ |   __/ |               
                              |___/   |___/
                ";
            string underline = "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n";
            showHeader(color, title, underline);

            Console.WriteLine("[inloggen inhoud]\n");

            string[] options = new string[]
            {
                    "Terug",
                    "Verder",
            };

            int choice = awaitResponse(options);

            switch (choice)
            {
                case 0:
                    CurrentScreen = "Authorizatie";
                    break;
                case 1:
                    CurrentScreen = "Home";
                    IsLoggedIn = true;
                    break;
            }

            Console.Clear();
            displayScreen();
        }

        // Registratie scherm.
        private void displayRegistreren()
        {
            ConsoleColor color = ConsoleColor.Cyan;
            string title = @"
   _____                   _         _                                       
  |  __ \                 (_)       | |                                      
  | |__) |   ___    __ _   _   ___  | |_   _ __    ___   _ __    ___   _ __  
  |  _  /   / _ \  / _` | | | / __| | __| | '__|  / _ \ | '__|  / _ \ | '_ \ 
  | | \ \  |  __/ | (_| | | | \__ \ | |_  | |    |  __/ | |    |  __/ | | | |
  |_|  \_\  \___|  \__, | |_| |___/  \__| |_|     \___| |_|     \___| |_| |_|
                   __/ |                                                    
                  |___/    

                ";

            string underline = "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n";
            showHeader(color, title, underline);

            Console.WriteLine("[registreren inhoud]\n");

            string[] options = new string[]
            {
                    "Terug",
            };

            int choice = awaitResponse(options);

            switch (choice)
            {
                case 0:
                    CurrentScreen = "Authorizatie";
                    break;
            }

            Console.Clear();
            displayScreen();
        }
        // Hoofdscherm.
        private void displayHome()
        {
            int currentHour = DateTime.Now.Hour;
            Func<int, string> greeting = hour =>
            {
                switch (hour)
                {
                    case < 12:
                        return "Goedemorgen,";
                    case >= 12 and < 18:
                        return "Goedemiddag,";
                    default:
                        return "Goedenavond,";
                }
            };

            ConsoleColor color = ConsoleColor.White;
            string result = greeting(currentHour);
            string name = "";

            if (IsLoggedIn)
            {
                name = "gebruiker";
            } else
            {
                name = "gast";
            }

            string title = @$"   
             
   {result} {name}!    
                            ___________I____________
                           ( _____________________ ()
                         _.-'|                    ||
                     _.-'   ||                    || 
    ______       _.-'       ||                    ||
   |      |_ _.-'           ||      Welkom op     ||
   |      |_|_              ||         het        ||
   |______|   `-._          ||     hoofdscherm.   ||
      /\          `-._      ||                    ||
     /  \             `-._  ||                    ||
    /    \                `-||____________________||
   /      \                 ------------------------
  /________\___________________/________________\______
                ";

            string underline = "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n";
            showHeader(color, title, underline);

            Console.WriteLine(" Gebruik pijltjestoetsen ↑ en ↓ om te navigeren\n en druk enter om een optie te kiezen.\n");

            string[] options = new string[] {
                "Reserveren",
                "Films",
                "Eten & Drinken",
                "Informatie",
                "Terug"
            };

            int choice = awaitResponse(options);

            switch (choice)
            {
                case 0:
                    if (IsLoggedIn)
                    {
                        CurrentScreen = options[choice];
                    } 
                    else
                    {
                        CurrentScreen = "Inloggen";
                    }
                    break;
                case > 0 and < 4:
                    CurrentScreen = options[choice];
                    break;
                default:
                    CurrentScreen = "Authorizatie";
                    break;
            }

            Console.Clear();
            displayScreen();
        }

        // Reserveer scherm.
        private void displayReserveren()
        {
            ConsoleColor color = ConsoleColor.Magenta;
            string title = @"
   _____                                                                  
  |  __ \                                                                 
  | |__) |   ___   ___    ___   _ __  __   __   ___   _ __    ___   _ __  
  |  _  /   / _ \ / __|  / _ \ | '__| \ \ / /  / _ \ | '__|  / _ \ | '_ \ 
  | | \ \  |  __/ \__ \ |  __/ | |     \ V /  |  __/ | |    |  __/ | | | |
  |_|  \_\  \___| |___/  \___| |_|      \_/    \___| |_|     \___| |_| |_|
                                                                                       
                ";
            string underline = "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n";
            showHeader(color, title, underline);

            Console.WriteLine("[reserveren inhoud]\n");

            string[] options = new string[]
            {
                    "Terug",
            };

            int choice = awaitResponse(options);

            switch (choice)
            {
                case 0:
                    CurrentScreen = "Home";
                    break;
            }

            Console.Clear();
            displayScreen();
        }

        // Films scherm.
        private void displayFilms()
        {
            ConsoleColor color = ConsoleColor.Blue;
            string title = @"
   ______   _   _                   
  |  ____| (_) | |                  
  | |__     _  | |  _ __ ___    ___ 
  |  __|   | | | | | '_ ` _ \  / __|
  | |      | | | | | | | | | | \__ \
  |_|      |_| |_| |_| |_| |_| |___/
                                 
                ";
            string underline = "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n";
            showHeader(color, title, underline);
            //FILMS INHOUD
            filmOverzicht zoeken = new filmOverzicht();
            var films = zoeken.ShowList();
            if (films != null)
            {
                if (films.Count > 0)
                {
                    Console.WriteLine("Uw zoekopdracht heeft de volgende resultat(en) opgeleverd:\n");
                    foreach (var film in films)
                    {
                        //KRIJG DE TALEN
                        int taalLength = film?.Taal?.Count ?? 0;
                        string talen = "";
                        for (int i = 0; i < taalLength; i++)
                        {
                            talen += film?.Taal?[i];
                            //VOEG DE JUISTE TEKEN TOE
                            if (taalLength > 1)
                            {
                                if ((taalLength - 2) == i)
                                {
                                    talen += " & ";
                                } else if ((taalLength - 1) != i)
                                {
                                    talen += ", ";
                                }
                            }
                        }
                        string taalString = taalLength == 1 ? "Taal" : "Talen";
                        //KRIJG DE GENRE(S)
                        int genreLength = film?.Genre?.Count ?? 0;
                        string genres = "";
                        for (int i = 0; i < genreLength; i++)
                        {
                            genres += film?.Genre?[i];
                            //VOEG DE JUISTE TEKEN TOE
                            if (genreLength > 1)
                            {
                                if ((genreLength - 2) == i)
                                {
                                    genres += " & ";
                                } else if ((genreLength - 1) != i)
                                {
                                    genres += ", ";
                                }
                            }
                        }
                        string genreString = genreLength == 1 ? "Genre" : "Genres";
                        //LAAT ALLES BOVEN DE 1 UUR ZIEN
                        if (film?.Looptijd > 60)
                        {
                            int? uur = film.Looptijd / 60;
                            int? minuten = film.Looptijd - (uur * 60);
                            string uurString = uur == 1 ? "uur" : "uren";
                            string minuutString = minuten == 1 ? "minuut" : "minuten";
                            //PRINT DE GEGEVENS
                            Console.WriteLine($"- {film.Titel} ({film.Jaar}) \n" +
                                $"  - {taalString} : {talen} \n" +
                                $"  - Looptijd: {uur} {uurString} en {minuten} {minuutString}.\n" +
                                $"  - {genreString} : {genres}\n" +
                                $"  - Directeur(s) : {film.Directeur}\n" +
                                $"  - Acteurs : {film.Acteurs}\n" +
                                $"  - Plot: {film.Plot}\n");
                        }
                        //LAAT ALLES ONDER DE 1 UUR ZIEN
                        else
                        {
                            //PRINT DE GEGEVENS
                            Console.WriteLine($"- {film?.Titel} ({film?.Jaar}), \n" +
                                $"  - {taalString} : {talen} \n" +
                                $"  - Looptijd: {film?.Looptijd}.\n" +
                                $"  - {genreString} : {genres}\n" +
                                $"  - Directeur(s) : {film?.Directeur}\n" +
                                $"  - Acteurs : {film?.Acteurs}\n" +
                                $"  - Plot: {film?.Plot}\n");
                        }
                    }
                } else { Console.WriteLine("Er zijn geen films gevonden"); }
            } else { Console.WriteLine("Iets ging fout"); }
            //GA TERUG NAAR MENU
            string[] options = new string[]
            {
                    "Terug",
            };
            int choice = awaitResponse(options);
            switch (choice)
            {
                case 0:
                    CurrentScreen = "Home";
                    break;
            }

            Console.Clear();
            displayScreen();
        }

        // Eten & Drinken scherm.
        private void displayEtenDrinken()
        {
            ConsoleColor color = ConsoleColor.Red;
            string title = @"
   ______   _                                 _____           _           _                   
  |  ____| | |                      ___      |  __ \         (_)         | |                  
  | |__    | |_    ___   _ __      ( _ )     | |  | |  _ __   _   _ __   | | __   ___   _ __  
  |  __|   | __|  / _ \ | '_ \     / _ \/\   | |  | | | '__| | | | '_ \  | |/ /  / _ \ | '_ \ 
  | |____  | |_  |  __/ | | | |   | (_>  <   | |__| | | |    | | | | | | |   <  |  __/ | | | |
  |______|  \__|  \___| |_| |_|    \___/\/   |_____/  |_|    |_| |_| |_| |_|\_\  \___| |_| |_|
                                                                                                      
                ";
            string underline = "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n";
            showHeader(color, title, underline);
            //OVERZICHT ETEN EN DRINKEN
            Console.WriteLine("Dit is een overzicht van het menu. U kunt op een item klikken om de voedingswaarde te zien.\n");
            string[] options = new string[] { "Zout popcorn", "Zoet popcorn", "Nachos", "Paprika chips", "Naturel chips", "Cola", "Sprite", "Spa blauw", "Spa rood", "GA TERUG" };
            int choice = awaitResponse(options);
            if (choice ==  options.Length -1){
                    CurrentScreen = "Home";
            } else
			{
                voeding product = new voeding(options[choice]);
                Console.Clear();
                showHeader(color, title, underline);
                Console.WriteLine(options[choice].ToUpper() + "\n");
                Console.WriteLine(product.returnVoedingswaarde());
                Console.WriteLine("\n");
                string[] terug = { "GA TERUG" };
				awaitResponse( terug );
            }
            Console.Clear();
            displayScreen();
        }

        // Informatie scherm.
        private void displayInformatie()
        {
            ConsoleColor color = ConsoleColor.Green;
            string title = @"
   _____            __                                      _     _        
  |_   _|          / _|                                    | |   (_)       
    | |    _ __   | |_    ___    _ __   _ __ ___     __ _  | |_   _    ___ 
    | |   | '_ \  |  _|  / _ \  | '__| | '_ ` _ \   / _` | | __| | |  / _ \
   _| |_  | | | | | |   | (_) | | |    | | | | | | | (_| | | |_  | | |  __/
  |_____| |_| |_| |_|    \___/  |_|    |_| |_| |_|  \__,_|  \__| |_|  \___|
                                                                                 
                ";
            string underline = "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━\n";
            showHeader(color, title, underline);

            Console.WriteLine("[informatie inhoud]\n");

            string[] options = new string[]
            {
                    "Terug",
            };

            int choice = awaitResponse(options);

            switch (choice)
            {
                case 0:
                    CurrentScreen = "Home";
                    break;
            }

            Console.Clear();
            displayScreen();
        }
    }
}
