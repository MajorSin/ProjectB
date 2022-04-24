using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.IO;

namespace Reserveringssysteem
{
    public class FilmController
    {
        public static List<Film> Films { get; set; }

		public List<Film> FilteredFilms { get; set; }

		public void ShowFilms(List<Film> films)
        {
			Console.ForegroundColor = ConsoleColor.White;
			string fields = String.Format("   {0,-15} {1,-30} {2,20}", "Id", "Titel", "Jaar");
			Console.WriteLine(fields);
			Console.WriteLine("   ----------------------------------------------------------------------");
			Console.ResetColor();

			for (int i = 0; i < films.Count; i++)
			{
				string filmTitle = films[i].Titel;

				if (filmTitle.Length > 25)
				{
					filmTitle = filmTitle.Substring(0, 25) + "...";
				}
				Console.WriteLine(String.Format("   {0,-15} {1,-30} {2,20}",
					films[i].Id, filmTitle, films[i].Jaar)
				);
				Console.WriteLine("   ----------------------------------------------------------------------");
			}

			Console.WriteLine();
		}

		public void ShowFilm(List<Film> films, Action showHeader)
        {
			int id = 0;
			string error = "";
			bool chosen = false;

			while (!chosen)
			{
				Console.Clear();
				showHeader();
				ShowFilms(films);
				Console.WriteLine("   Van welke film wilt u de details bekijken? (Typ het nummer in van de film)");

				if (error != "")
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(error);
					Console.ResetColor();
					error = "";
				}

				Console.CursorLeft = 3;
				string givenInput = Console.ReadLine();

				if (String.IsNullOrEmpty(givenInput))
				{
					error = "   Het veld kan niet leeg zijn.";
				}
				else if (!int.TryParse(givenInput, out id))
				{
					error = "   Dit is geen nummer.";
				}
				else
				{
					bool exists = false;
					for (int i = 0; i < films.Count; i++)
					{
						if (films[i].Id == id)
						{
							exists = true;
							chosen = true;
							break;
						}
					}
					if (!exists)
					{
						error = "   Film niet gevonden.";
					}
				}
			}

			Console.WriteLine("   Hieronder staan de details van de film:");

			for (int i = 0; i < films.Count; i++)
			{
				if (films[i].Id == id)
				{
					Console.Clear();
					showHeader();
					Console.WriteLine(films[i]);
					break;
				}
			}
		}

		//LAAT LIJST ZIEN
		public string ShowList(Action showHeader, Router router)
		{
			FilteredFilms = new List<Film>();

			for (int i = 0; i < Films.Count; i++)
            {
				FilteredFilms.Add(Films[i]);
            }

			Func<string>[] arr = new Func<string>[] {
				() => TitelZoeken(showHeader, router),
				() => GenreZoeken(showHeader, router),
				() => TaalZoeken(showHeader, router)
			};

			string result = "";
			for (int i = 0; i < arr.Length; i++)
			{
				Console.Clear();
				showHeader();
				result = arr[i]();

				if (result == "Terug")
				{
					break;
				}
			}

			if (result == "Terug")
			{
				return "Back";
			}
			return "";
		}

		//ZOEK OP TITEL
		public string TitelZoeken(Action showHeader, Router router)
		{
			Console.WriteLine("   Wilt u zoeken naar een specifieke titel?\n");
			string[] jaNee = { "Ja", "Nee", "Terug" };
			string keuzeTitel = router.AwaitResponse(jaNee);

			if (keuzeTitel == jaNee[0])
			{
				Console.Clear();
				showHeader();
				Console.WriteLine("   Welk titel wil u kiezen?");
				Console.CursorLeft = 3;
				var titelFilter = Console.ReadLine();
				while (string.IsNullOrWhiteSpace(titelFilter))
				{
					Console.WriteLine("   Kies uit een titel.");
					titelFilter = Console.ReadLine();
				}
				//FILTER DE JSON
				if (FilteredFilms != null)
				{
					int i = 0;
					while (i < FilteredFilms.Count)
					{
						string title = FilteredFilms[i].Titel;
						string titleLower = title?.ToLower();
						if (titleLower != null)
						{
							if (!titleLower.Contains(titelFilter.ToLower()))
							{
								FilteredFilms.RemoveAt(i);
							}
							else { i++; }
						}
					}
				}
			}
			return keuzeTitel;
		}

		//ZOEK OP TAAL
		public string TaalZoeken(Action showHeader, Router router)
		{
			Console.WriteLine("   Wilt u kiezen uit een taal?\n");
			string[] jaNee = { "Ja", "Nee", "Terug" };
			string keuzeTaal = router.AwaitResponse(jaNee);

			if (keuzeTaal == jaNee[0])
			{
				Console.Clear();
				showHeader();
				//LIJST EN ARRAY OM TE CHECKEN
				string[] taalKeuzeUit = { "Nederlands", "Engels", "Spaans", "Duits", "Japans", "GEKOZEN" };
				Console.WriteLine($"   U kunt telkens een taal kiezen, de keuze stopt tot u 'GEKOZEN' kiest.\n");
				//FILTER
				List<string> taalGekozen = KeuzeFilter(taalKeuzeUit);
				//KIES ALLE FILMS MET DE JUISTE TAAL
				if (taalGekozen?.Count > 0)
				{
					int i = 0;
					while (i < FilteredFilms?.Count)
					{
						var filmsCheckList = FilteredFilms[i].Taal;
						if (filmsCheckList != null)
						{
							//TAAL GEVONDEN
							if (filmsCheckList.Any(x => taalGekozen.Any(y => y == x))) { i++; }
							else
							//TAAL ZIT ER NIET IN
							{
								FilteredFilms.RemoveAt(i);
							}
						}
					}
				}
			}
			return keuzeTaal;
		}

		//ZOEK OP GENRE
		public string GenreZoeken(Action showHeader, Router router)
		{
			Console.WriteLine("   Wilt u zoeken naar specifieke genres?\n");
			string[] jaNee = { "Ja", "Nee", "Terug" };
			string keuzeGenre = router.AwaitResponse(jaNee);

			if (keuzeGenre == jaNee[0])
			{
				Console.Clear();
				showHeader();
				//LIJST EN ARRAY OM TE CHECKEN
				string[] genreKeuzeUit = { "actie", "animatie", "avontuur", "documentaire", "drama", "familie", "fantasy",
				"historisch", "horror", "komedie", "misdaad", "mystery", "oorlog", "romantisch", "sci-fi", "GEKOZEN" };
				//PRINT DE OPTIES
				Console.WriteLine($"   U kunt telkens een genre kiezen, de keuze stopt tot u 'GEKOZEN' kiest.\n");
				//FILTER
				List<string> genresGekozen = KeuzeFilter(genreKeuzeUit);
				Console.WriteLine("");
				//KIES ALLE FILMS VAN DE GENRES
				if (genresGekozen?.Count > 0)
				{
					int i = 0;
					while (i < FilteredFilms?.Count)
					{
						var filmsCheckList = FilteredFilms[i].Genre;
						if (filmsCheckList != null)
						{
							//GENRE GEVONDEN
							if (filmsCheckList.Any(x => genresGekozen.Any(y => y == x))) { i++; }
							else
							//GENRE ZIT ER NIET IN
							{
								FilteredFilms.RemoveAt(i);
							}
						}
					}
				}
			}
			return keuzeGenre;
		}

		//KEUZEMENU VOOR FILTEREN
		public List<string> KeuzeFilter(string[] options)
		{
			int currentSelected = 0;
			List<int> selected = new();
			bool selectionDone = false;
			List<string> gekozen = new();

			// Loopt door de opties en houdt bij welke keuze je maakt met pijltjestoetsen.
			while (!selectionDone)
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
					if (selected.Contains(i))
					{
						Console.ForegroundColor = ConsoleColor.DarkCyan;
						Console.WriteLine("  {0}", options[i] + " - TOEGEVOEGD");
					}
					else if ((options.Length - 1) == i)
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("  {0}", options[i]);
					}
					else
					{
						Console.WriteLine("  {0}", options[i]);
					}
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
						if (currentSelected == (options.Length - 1))
						{
							return gekozen;
						}
						else
						{
							if (!gekozen.Contains(options[currentSelected]))
							{
								gekozen.Add(options[currentSelected]);
								selected.Add(currentSelected);
							}
						}
						break;
				}
				// Zorgt ervoor dat de keuzes niet met elkaar gaan overlappen.
				Console.CursorTop -= options.Length;
			}
			return gekozen;
		}

		public List<Film> GetFilms()
		{
			return Films;
		}

		public static void SetFilms()
		{
			var json = File.ReadAllText("../../../DataFiles/films.json", Encoding.GetEncoding("utf-8"));
			Films = JsonConvert.DeserializeObject<List<Film>>(json);
		}
	}
}
