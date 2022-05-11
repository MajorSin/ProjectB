using Reserveringssysteem.Classes;
using System;
using System.Collections.Generic;
using static Reserveringssysteem.UserController;

namespace Reserveringssysteem
{
    public class Router
    {
        public string CurrentScreen { get; set; }

        // Toont het scherm waar je nu op bent.
        public void DisplayScreen()
        {
            switch (CurrentScreen)
            {
                case "Authorizatie":
                    DisplayAuthorization();
                    break;
                case "Inloggen":
                    DisplayInloggen();
                    break;
                case "Registreren":
                    DisplayRegistreren();
                    break;
                case "Home":
                    DisplayHome();
                    break;
                case "Reserveren":
                    DisplayReserveren();
                    break;
                case "Films":
                    DisplayFilms();
                    break;
                case "Eten & Drinken":
                    DisplayEtenDrinken();
                    break;
                case "Informatie":
                    DisplayInformatie();
                    break;
                default:
                    SetCurrentScreen("Authorizatie");
                    DisplayAuthorization();
                    break;
            }
        }

        // Lay-out van de header teruggeven.
        private void ShowHeader(ConsoleColor color, string title)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(@"{0}", title);
            Console.BackgroundColor = color;
            Console.ResetColor();
        }

        // Keuze van de gebruiker vaststellen.
        public string AwaitResponse(string[] options)
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
            return options[currentSelected];
        }

        // Authorisatie scherm.
        private void DisplayAuthorization()
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
            ShowHeader(color, title);

            Console.WriteLine("   Gebruik pijltjestoetsen ↑ en ↓ om te navigeren\n   en druk enter om een optie te kiezen.\n");

            string[] options;

            if (IsLoggedIn)
            {
                options = new string[]
                {
                    "Uitloggen",
                    "Doorgaan naar home",
                    "Beeïndigen",
                };
            }
            else
            {
                options = new string[]
                {
                    "Inloggen",
                    "Registreren",
                    "Doorgaan als gast",
                    "Beeïndigen",
                };
            }

            string choice = AwaitResponse(options);

            switch (choice)
            {
                case "Inloggen":
                    SetCurrentScreen("Inloggen");
                    break;
                case "Uitloggen":
                    if (IsLoggedIn)
                    {
                        LogOut();
                        SetCurrentScreen("Authorizatie");
                    }
                    break;
                case "Registreren":
                    SetCurrentScreen("Registreren");
                    break;
                case "Doorgaan naar home":
                    SetCurrentScreen("Home");
                    break;
                case "Doorgaan als gast":
                    SetCurrentScreen("Home");
                    break;
                case "Beeïndigen":
                    Environment.Exit(0);
                    break;
                default:
                    SetCurrentScreen("Authorizatie");
                    break;
            }
        }

        // Inlog scherm.
        private void DisplayInloggen()
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
            ShowHeader(color, title);

            Action showHeader = () => ShowHeader(color, title);
            UserController controller = new();
            controller.Login(showHeader, this);
        }

        // Registratie scherm.
        private void DisplayRegistreren()
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
            ShowHeader(color, title);

            Action showHeader = () => ShowHeader(color, title);
            MemberController controller = new();
            controller.Register(showHeader, this);
        }

        // Hoofdscherm.
        private void DisplayHome()
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
                name = CurrentUser.GetFirstName();
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
            ShowHeader(color, title);

            Console.WriteLine("   Gebruik pijltjestoetsen ↑ en ↓ om te navigeren\n   en druk enter om een optie te kiezen.\n");

            string[] options = new string[] {
                "Reserveren",
                "Films",
                "Eten & Drinken",
                "Informatie",
                "Terug"
            };

            string choice = AwaitResponse(options);

            switch (choice)
            {
                case "Reserveren":
                    if (IsLoggedIn)
                    {
                        SetCurrentScreen(choice);
                    } 
                    else
                    {
                        SetCurrentScreen("Inloggen");
                    }
                    break;
                case "Terug":
                    SetCurrentScreen("Authorizatie");
                    break;
                default:
                    SetCurrentScreen(choice);
                    break;
            }
        }

        // Reserveer scherm.
        private void DisplayReserveren()
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
            ShowHeader(color, title);
            string[] options = new string[]
            {
                    "Terug",
            };

            //LAAT ALLE FILMS ZIEN DIE BINNEKORT DRAAIEN
            draaiendeFilms draaienClass = new draaiendeFilms();
            var filmsDieBinnekortDraaien = draaienClass.laatDraaiendeFilmsZien();
            if(filmsDieBinnekortDraaien.Count <= 0) { Console.WriteLine("Er draaien binnenkort geen films"); }
            else { 
                for (int i = 0; i < filmsDieBinnekortDraaien.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + filmsDieBinnekortDraaien[i].Name);
                }
            }

            string choice = AwaitResponse(options);
            switch (choice)
            {
                case "Terug":
                    SetCurrentScreen("Home");
                    break;
            }
        }

        // Films scherm.
        private void DisplayFilms()
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
            ShowHeader(color, title);

            Action showHeader = () => ShowHeader(color, title);
            FilmController controller = new();
            List<Film> films = controller.GetFilms();

            controller.ShowFilms(films);

            string[] options = new string[]
            {
                "Bekijk film details",
                "Filter films",
                "Terug"
            };

            string choice = AwaitResponse(options);

            if (choice == "Bekijk film details")
            {
                controller.ShowFilm(films, showHeader);

                options = new string[]
                {
                    "Terug",
                };

                choice = AwaitResponse(options);
            } else if (choice == "Filter films")
            {
                string result = controller.ShowList(showHeader, this);
                films = controller.FilteredFilms;

                Console.Clear();
                ShowHeader(color, title);

                if (result != "Back")
                {
                    if (films.Count > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("   Uw zoekopdracht heeft de volgende resultaat(en) opgeleverd:\n");
                        Console.ResetColor();
                        controller.ShowFilms(films);

                        options = new string[]
                        {
                            "Bekijk film details",
                            "Terug",
                        };

                        choice = AwaitResponse(options);

                        if (choice == "Bekijk film details")
                        {
                            controller.ShowFilm(films, showHeader);
                            options = new string[]
                            {
                                "Terug",
                            };
                            choice = AwaitResponse(options);
                        }
                    } else
                    {
                        Console.WriteLine("   Er zijn geen films gevonden\n");
                        options = new string[]
                        {
                            "Terug",
                        };
                        choice = AwaitResponse(options);
                    }
                }
            } else
            {
                SetCurrentScreen("Home");
            }
        }

        // Eten & Drinken scherm.
        private void DisplayEtenDrinken()
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
            ShowHeader(color, title);

            //OVERZICHT ETEN EN DRINKEN
            Console.WriteLine("   Dit is een overzicht van het menu. U kunt op een item klikken om de voedingswaarde te zien.\n");
            string[] options = new string[] { "Zout popcorn", "Zoet popcorn", "Nachos", "Paprika chips", "Naturel chips", "Cola", "Sprite", "Spa blauw", "Spa rood", "Terug" };
            string choice = AwaitResponse(options);

            if (choice == "Terug")
            {
                SetCurrentScreen("Home");
            }
            else
            {
                Voeding product = new(choice);
                Console.Clear();
                ShowHeader(color, title);

                Console.WriteLine($"   {choice.ToUpper()}\n");
                Console.WriteLine(product.returnVoedingswaarde());
                Console.WriteLine("\n");

                options = new string[]
                {
                    "Terug",
                };

                AwaitResponse(options);
            }

            Console.Clear();
            DisplayScreen();
        }

        // Informatie scherm.
        private void DisplayInformatie()
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
            ShowHeader(color, title);

            Console.WriteLine(@"   Hier vindt u de algemene informatie over de bioscoop.

   Adres:    Wijnhaven 107, Rotterdam
   Postcode: 3011 WN
    
   Openingstijden:
        Maandag:      09:00 - 22:00
        Dinsdag:      09:00 - 22:00
        Woensdag:     09:00 - 22:00
        Donderdag:    09:00 - 22:00
        Vrijdag:      09:00 - 00:00
        Zaterdag:     09:00 - 00:00
        Zondag:       09:00 - 22:00
            ");

            string[] options = new string[]
            {
                    "Terug",
            };

            string choice = AwaitResponse(options);

            SetCurrentScreen("Home");
        }

        public void SetCurrentScreen(string screen)
        {
            CurrentScreen = screen;
        }
    }
}
