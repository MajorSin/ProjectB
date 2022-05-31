using System;
using System.Collections.Generic;
using System.Linq;
using Reserveringssysteem.Classes;

namespace Reserveringssysteem
{
	public class AdminController : UserController
	{
		public static Admin makeAdmin()
		{
			Admin admin = new Admin(1, "admin", "123", "Admin", "Admin", "unknown", "11-1-2001", "admin@hotmail.com");
			return admin;
		}

        // Kopie maken van een Film object.
        public Film MakeCopy(Film film)
        {
            Film copy = new Film(film.Id, film.Titel, film.Jaar, film.Taal, (int)film.Looptijd, film.Genre, film.Directeur, film.Acteurs, film.Plot);
            return copy;
        }

        // Check of het gegeven tijd geldig is.
        public bool IsTime(string txtTime)
        {
            TimeSpan time;
            return TimeSpan.TryParse(txtTime, out time);
        }

        // Laat een film draaien.
        public string DraaiFilm(Router router, draaiendeFilms draaienClass, Action showHeader, FilmController filmController, List<Film> films)
        {
            Film film = filmController.ShowFilm(films, showHeader);

            if (film != null)
            {
                List<draaiendeFilms.draaienFilms> gedraaideFilms = draaienClass.GetDraaienFilms();

                string error = "";

                string date = "";
                DateTime dateTime = DateTime.Now;
                bool choseDate = false;

                string time = "";
                bool choseTime = false;

                string strAuditorium = "";
                int auditorium = 0;
                bool choseAuditorium = false;

                while (!choseDate)
                {
                    Console.Clear();
                    showHeader();

                    Console.WriteLine($"   Gekozen film: {film.Titel}\n");

                    if (error != "")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(error);
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine($"   Kies een datum voor de film: (Voorbeeld: {dateTime.ToString("dd-MM-yyyy")})");
                    Console.CursorLeft = 3;

                    date = Console.ReadLine();

                    if (String.IsNullOrEmpty(date))
                    {
                        error = "   Veld kan niet leeg zijn.";
                    }
                    else if (!IsDateTime(date))
                    {
                        error = "   Datum klopt niet.";
                    }
                    else
                    {
                        error = "";
                        DateTime tempDate = DateTime.Parse(date);

                        if (tempDate < dateTime.AddDays(-1))
                        {
                            error = "   Datum kan niet al geweest zijn.";
                        }
                        else
                        {
                            choseDate = true;
                            dateTime = DateTime.Parse(date);
                        }
                    }
                }
                while (!choseAuditorium)
                {
                    Console.Clear();
                    showHeader();

                    Console.WriteLine($"   Gekozen film: {film.Titel}\n" +
                        $"   Gekozen datum: {dateTime.ToString("dd-MM-yyyy")}\n");

                    if (error != "")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(error);
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine($"   Kies een zaal voor de film: 1) Zaal 1 | 2) Zaal 2 | 3) Zaal 3");
                    Console.CursorLeft = 3;

                    strAuditorium = Console.ReadLine();

                    if (String.IsNullOrEmpty(strAuditorium))
                    {
                        error = "   Veld kan niet leeg zijn.";
                    }
                    else if (strAuditorium != "1" && strAuditorium != "2" && strAuditorium != "3")
                    {
                        error = "   Kies uit een van de 3 zalen.";
                    }
                    else
                    {
                        error = "";
                        auditorium = Convert.ToInt32(strAuditorium);
                        choseAuditorium = true;
                    }
                }
                while (!choseTime)
                {
                    Console.Clear();
                    showHeader();

                    Console.WriteLine($"   Gekozen film: {film.Titel}\n" +
                        $"   Gekozen datum: {dateTime.ToString("dd-MM-yyyy")}\n" +
                        $"   Gekozen zaal: {auditorium}\n");

                    if (error != "")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(error);
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.WriteLine($"   Kies een tijd voor de film: (Voorbeeld: 18:30)");
                    Console.CursorLeft = 3;

                    time = Console.ReadLine();

                    if (String.IsNullOrEmpty(time))
                    {
                        error = "   Veld kan niet leeg zijn.";
                    }
                    else if (time.Length != 5)
                    {
                        error = "   Tijd klopt niet.";
                    }
                    else if (!(time[2] == ':') || ((!char.IsDigit(time[0])) || !char.IsDigit(time[1]) || !char.IsDigit(time[3]) || !char.IsDigit(time[4])))
                    {
                        error = "   Tijd klopt niet.";
                    }
                    else if (!IsTime(time))
                    {
                        error = "   Tijd klopt niet.";
                    }
                    else
                    {
                        error = "";
                        for (int i = 0; i < gedraaideFilms.Count; i++)
                        {
                            for (int j = 0; j < gedraaideFilms[i].Datum.Count; j++)
                            {
                                if ((DateTime.Parse(gedraaideFilms[i].Datum[j]).Date == dateTime.Date) && (gedraaideFilms[i].Zaal[j] == auditorium))
                                {
                                    Film gedraaideFilm = null;
                                    DateTime gedraaideFilmDatum = DateTime.Parse(gedraaideFilms[i].Datum[j]);
                                    string strGedraaideFilmTijd = gedraaideFilmDatum.ToString("HH:mm");
                                    TimeSpan gedraaideFilmTijd = TimeSpan.Parse(strGedraaideFilmTijd);
                                    TimeSpan gekozenTijd = TimeSpan.Parse(time);

                                    for (int t = 0; t < films.Count; t++)
                                    {
                                        if (films[t].Id == gedraaideFilms[i].FilmID)
                                        {
                                            gedraaideFilm = films[t];
                                            break;
                                        }
                                    }

                                    if (gekozenTijd == gedraaideFilmTijd)
                                    {
                                        error = "   Tijd is niet beschikbaar.";
                                        break;
                                    }
                                    else if (gekozenTijd < gedraaideFilmTijd)
                                    {
                                        gekozenTijd += TimeSpan.FromMinutes((double)film.Looptijd);
                                        Console.WriteLine(gekozenTijd.ToString(@"hh\:mm"));
                                        Console.WriteLine(gedraaideFilmTijd);

                                        if (!(gekozenTijd < gedraaideFilmTijd))
                                        {
                                            error = "   Looptijd loopt af op andere film.";
                                        }
                                    }
                                    else if (gekozenTijd > gedraaideFilmTijd)
                                    {
                                        gedraaideFilmTijd += TimeSpan.FromMinutes((double)gedraaideFilm.Looptijd);
                                        if (!(gekozenTijd > gedraaideFilmTijd))
                                        {
                                            error = "   Andere film draait nog tijdens deze tijd.";
                                        }
                                    }
                                }
                            }
                            if (error != "")
                            {
                                break;
                            }
                        }
                        if (error == "")
                        {
                            TimeSpan tempTime = TimeSpan.Parse(time);
                            dateTime = dateTime.Date + tempTime;
                            choseTime = true;
                        }
                    }
                }

                Console.Clear();
                showHeader();

                Console.WriteLine($"   Gekozen film: {film.Titel}\n" +
                $"   Gekozen datum: {dateTime.ToString("dd-MM-yyyy")}\n" +
                $"   Gekozen tijd: {dateTime.ToString("HH:mm")}\n" +
                $"   Gekozen zaal: {auditorium}\n");

                string[] options = new string[] {
                    "Bevestigen",
                };

                router.AwaitResponse(options);

                bool alreadyExists = false;
                for (int i = 0; i < gedraaideFilms.Count; i++)
                {
                    if (gedraaideFilms[i].FilmID == film.Id)
                    {
                        alreadyExists = true;
                        gedraaideFilms[i].Datum.Add(dateTime.ToString("yyyy-MM-ddTHH:mm:ss"));
                        gedraaideFilms[i].Zaal.Add(auditorium);
                        break;
                    }
                }

                if (!alreadyExists)
                {
                    List<string> datums = new List<string>();
                    datums.Add(dateTime.ToString("yyyy-MM-ddTHH:mm:ss"));

                    List<int> zalen = new List<int>();
                    zalen.Add(auditorium);

                    gedraaideFilms.Add(new draaiendeFilms.draaienFilms(film.Id, film.Titel, datums, zalen));
                }

                draaienClass.UpdateDraaienFilms();

                return "";
            }
            else
            {
                return "Back";
            }
        }

        // Valideren of alle waardes kloppen.
        public void EditField(Film film, string field, Action showHeader, FilmController filmController)
        {
            bool errorsGone = false;
            string error = "";

            while (!errorsGone)
            {
                Console.Clear();
                showHeader();

                if (error != "")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(error);
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    error = "";
                }

                switch (field)
                {
                    case "Titel":
                        Console.WriteLine($"   Huidige titel: {film.Titel}\n");
                        Console.WriteLine($"   Wat wilt u de titel waarde naar veranderen? (Laat leeg om huidige titel waarde te houden)");
                        Console.CursorLeft = 3;
                        string title = Console.ReadLine();

                        if (title != "")
                        {
                            if (string.IsNullOrWhiteSpace(title))
                            {
                                error = "   Het kan niet alleen spaties bevatten.";
                            } else
                            {
                                film.Titel = title;
                            }
                        }
                        break;
                    case "Jaar":
                        Console.WriteLine($"   Huidige jaar: {film.Jaar}\n");
                        Console.WriteLine($"   Wat wilt u de jaar waarde naar veranderen? (Laat leeg om huidige jaar waarde te houden)");
                        Console.CursorLeft = 3;
                        string year = Console.ReadLine();

                        if (year != "")
                        {
                            int number;
                            bool converted = int.TryParse(year, out number);
                            DateTime currentDate = DateTime.Now;

                            if (!converted)
                            {
                                error = "   Dit is geen jaar.";
                            }
                            else if (year.Length != 4)
                            {
                                error = "   Aantal cijfers klopt niet.";
                            }
                            else if (number > currentDate.Year)
                            {
                                error = "   Dit jaar moet nog komen.";
                            }
                            else if (number < 1930)
                            {
                                error = "   Jaar is te ver achteruit.";
                            }

                            if (error == "")
                            {
                                film.Jaar = Int32.Parse(year);
                            }
                        }
                        break;
                    case "Taal":
                        string[] taalKeuzeUit = { "Engels", "Nederlands", "Duits", "Italiaans", "Perzisch", "Pools", "Grieks", "Frans", "Russisch", "Portugees",
                        "Vietnamees", "Arabisch", "Turks", "Spaans", "Koreaans", "Japans", "Zweeds", "Mandarijn", "GEKOZEN" };
                        Console.WriteLine($"   U kunt telkens een taal kiezen, de keuze stopt tot u 'GEKOZEN' kiest.\n");
                        List<string> talen = filmController.KeuzeFilter(taalKeuzeUit, film.Taal);
                        film.Taal = talen;
                        break;
                    case "Genre":
                        string[] genreKeuzeUit = { "actie", "animatie", "avontuur", "documentaire", "drama", "familie", "fantasy",
                        "historisch", "horror", "komedie", "misdaad", "mystery", "oorlog", "thriller", "romantisch", "sci-fi", "GEKOZEN" };
                        Console.WriteLine($"   U kunt telkens een genre kiezen, de keuze stopt tot u 'GEKOZEN' kiest.\n");
                        List<string> genres = filmController.KeuzeFilter(genreKeuzeUit, film.Genre);
                        film.Genre = genres;
                        break;
                    case "Directeur":
                        Console.WriteLine($"   Huidige directeur: {film.Directeur}\n");
                        Console.WriteLine($"   Wat wilt u de directeur waarde naar veranderen? (Laat leeg om huidige directeur waarde te houden)");
                        Console.CursorLeft = 3;
                        string directeur = Console.ReadLine();

                        if (directeur != "")
                        {
                            if (string.IsNullOrWhiteSpace(directeur))
                            {
                                error = "   Het kan niet alleen spaties bevatten.";
                            }
                            else if (directeur.Any(c => !char.IsLetter(c) && c != ' '))
                            {
                                error = "   Het kan niet alleen cijfers of symbolen bevatten.";
                            }
                            else { 
                                film.Directeur = directeur;
                            }
                        }
                        break;
                    case "Acteurs":
                        Console.WriteLine($"   Huidige acteurs: {film.Acteurs}\n");
                        Console.WriteLine($"   Wat wilt u de acteurs waarde naar veranderen? (Laat leeg om huidige acteurs waarde te houden)");
                        Console.CursorLeft = 3;
                        string actors = Console.ReadLine();
                        
                        if (actors != "")
                        {
                            film.Acteurs = actors;
                        }
                        break;
                    case "Plot":
                        Console.WriteLine($"   Huidige plot:\n");

                        string filmPlot = film.Plot;
                        string[] words = filmPlot.Split(' ');
                        words[0] = "   " + words[0];
                        int currentLen = 10;

                        for (int j = 0; j < words.Length; j++)
                        {
                            if (j >= currentLen)
                            {
                                words[j] = words[j] + "\n" + "  ";
                                currentLen += 10;
                            }
                        }

                        Console.WriteLine(String.Join(" ", words) + "\n");

                        Console.WriteLine($"   Wat wilt u de plot waarde naar veranderen? (Laat leeg om huidige plot waarde te houden)");
                        Console.CursorLeft = 3;
                        string plot = Console.ReadLine();

                        if (plot != "")
                        {
                            if (string.IsNullOrWhiteSpace(plot))
                            {
                                error = "   Het kan niet alleen spaties bevatten.";
                            }
                            else if (plot.Any(c => char.IsLetter(c)))
                            {
                                film.Plot = plot;
                            }
                            else
                            {
                                error = "   Plot bevat geen letters.";
                            }
                        }
                        break;
                }
                if (error == "")
                {
                    errorsGone = true;
                }
            }
        }

        // Film aanpassen uit de huidige films.
        public string EditFilm(Router router, Action showHeader, FilmController filmController, List<Film> films)
        {
            Film film = filmController.ShowFilm(films, showHeader);
            draaiendeFilms draaienClass = new draaiendeFilms();
            string[] options = new string[0];
            string choice = "";

            if (film == null)
            {
                return "Back";
            }
            else
            {
                Console.Clear();
                showHeader();

                Film copyOfFilm = MakeCopy(film);
                bool finishedEditing = false;

                while (!finishedEditing)
                {
                    Console.Clear();
                    showHeader();

                    Console.WriteLine(copyOfFilm);
                    Console.WriteLine("   Welke waarde wilt u aanpassen? (Kies 'Klaar' als u klaar bent met aanpassen)\n");

                    options = new string[]
                    {
                        "Titel",
                        "Jaar",
                        "Taal",
                        "Genre",
                        "Directeur",
                        "Acteurs",
                        "Plot",
                        "Klaar"
                    };

                    choice = router.AwaitResponse(options);

                    if (choice == "Klaar")
                    {
                        finishedEditing = true;
                    }
                    else
                    {
                        EditField(copyOfFilm, choice, showHeader, filmController);
                    }
                }

                Console.Clear();
                showHeader();

                Console.WriteLine(copyOfFilm);

                options = new string[]
                {
                    "Bevestigen",
                    "Terug"
                };

                choice = router.AwaitResponse(options);

                if (choice == "Terug")
                {
                    return "Back";
                }
                else
                {
                    string oldTitel = film.Titel;
                    film = MakeCopy(copyOfFilm);
                    films = filmController.GetFilms();

                    for (int i = 0; i < films.Count; i++)
                    {
                        if (films[i].Id == film.Id)
                        {
                            films[i] = film;
                            break;
                        }
                    }
                    filmController.UpdateFilms();

                    List<draaiendeFilms.draaienFilms> gedraaideFilms = draaienClass.GetDraaienFilms();
                    for (int i = 0; i < gedraaideFilms.Count; i++)
                    {
                        if (gedraaideFilms[i].FilmID == film.Id)
                        {
                            gedraaideFilms[i].Name = film.Titel;
                            break;
                        }
                    }
                    draaienClass.UpdateDraaienFilms();

                    Zaal newZaal = new Zaal(1);
                    newZaal.leesJsonReserveringen();
                    List<Zaal.reserveringenJson> reserveringen = newZaal.reserveringen;
                    for (int i = 0; i < reserveringen.Count; i++)
                    {
                        if (reserveringen[i].Titel == oldTitel)
                        {
                            reserveringen[i].Titel = film.Titel;
                            break;
                        }
                    }
                    newZaal.UpdateReservations();
                }
            }
            return "";
        }

        // Film verwijderen uit de huidige films.
        public string RemoveFilm(Router router, Action showHeader, FilmController filmController, List<Film> films)
        {
            Film film = filmController.ShowFilm(films, showHeader);
            draaiendeFilms draaienClass = new draaiendeFilms();
            string[] options = new string[0];
            string choice = "";

            if (film == null)
            {
                return "Back";
            }
            else
            {
                Console.Clear();
                showHeader();

                Console.WriteLine(film);
                Console.WriteLine("   Wilt u deze film verwijderen?\n");

                options = new string[]
                {
                    "Bevestigen",
                    "Terug"
                };

                choice = router.AwaitResponse(options);

                if (choice == "Terug")
                {
                    return "Back";
                }
                else
                {
                    List<draaiendeFilms.draaienFilms> gedraaideFilms = draaienClass.GetDraaienFilms();
                    for (int i = 0; i < gedraaideFilms.Count; i++)
                    {
                        if (gedraaideFilms[i].FilmID == film.Id)
                        {
                            gedraaideFilms.Remove(gedraaideFilms[i]);
                            break;
                        }
                    }
                    draaienClass.UpdateDraaienFilms();

                    Zaal newZaal = new Zaal(1);
                    newZaal.leesJsonReserveringen();
                    List<Zaal.reserveringenJson> reserveringen = newZaal.reserveringen;
                    for (int i = 0; i < reserveringen.Count; i++)
                    {
                        if (reserveringen[i].Titel == film.Titel)
                        {
                            reserveringen.Remove(reserveringen[i]);
                            break;
                        }
                    }
                    newZaal.UpdateReservations();

                    films = filmController.GetFilms();
                    for (int i = 0; i < films.Count; i++)
                    {
                        if (films[i].Id == film.Id)
                        {
                            films.Remove(films[i]);
                            break;
                        }
                    }
                    filmController.UpdateFilms();
                }
            }
            return "";
        }
    }
}
