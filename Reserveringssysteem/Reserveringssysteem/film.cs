using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.IO;

namespace Reserveringssysteem
{
	public class film
	{
		public List<FilmOverzicht> filmsZoeken;
		//LEES JSON
		public void readJson()
		{
			var json = File.ReadAllText("../../../films.json", Encoding.GetEncoding("utf-8"));
			this.filmsZoeken = JsonConvert.DeserializeObject<List<FilmOverzicht>>(json);
		}
		//WAARDEN VAN DE JSON
		public class FilmOverzicht
		{
			public int Id { get; set; }
			public string Titel { get; set; }
			public int Jaar { get; set; }
			public List<string> Taal { get; set; }
			public int? Looptijd { get; set; }
			public List<string> Genre { get; set; }
			public string Directeur { get; set; }
			public string Acteurs { get; set; }
			public string Plot { get; set; }
		}
		//LAAT LIJST ZIEN
		public List<FilmOverzicht> ShowList()
		{
			readJson();
			TitelZoeken();
			GenreZoeken();
			TaalZoeken();
			if (filmsZoeken != null)
			{
				return filmsZoeken;
			} else
			{
				return null;
			}
		}
		//ZOEK OP TITEL
		public void TitelZoeken()
		{
			Console.WriteLine("Wilt op zoek naar een specifieke titel?\n");
			string[] JaNee = { "Ja", "Nee" };
			int KeuzeTitel = KeuzeEen(JaNee) + 1;
			if (KeuzeTitel == 1)
			{
				Console.WriteLine("\n");
				Console.WriteLine("\nWelk titel wil u kiezen?");
				var titelFilter = Console.ReadLine();
				while (string.IsNullOrWhiteSpace(titelFilter))
				{
					Console.WriteLine("Kies uit een titel.");
					titelFilter = Console.ReadLine();
				}
				//FILTER DE JSON
				if (filmsZoeken != null)
				{
					int i = 0;
					while (i < filmsZoeken.Count)
					{
						string title = filmsZoeken[i].Titel;
						string titleLower = title?.ToLower();
						if (titleLower != null)
						{
							if (!titleLower.Contains(titelFilter.ToLower()))
							{
								filmsZoeken.RemoveAt(i);
							} else { i++; }
						}
					}
				}
				Console.Clear();
			}
			Console.Clear();
		}
		//ZOEK OP TAAL
		private void TaalZoeken()
		{
			Console.WriteLine("Wilt u kiezen uit een taal?\n");
			string[] JaNee = { "Ja", "Nee" };
			int KeuzeTaal = KeuzeEen(JaNee) + 1;
			Console.WriteLine("\n");
			if (KeuzeTaal == 1)
			{
				//LIJST EN ARRAY OM TE CHECKEN
				string[] taalKeuzeUit = { "Nederlands", "Engels", "Spaans", "Duits", "Japans", "GEKOZEN" };
				Console.WriteLine($"\n\nU kunt telkens een taal kiezen, de keuze stopt tot u 'GEKOZEN' kiest.\n");
				//FILTER
				List<string> taalGekozen = keuzeFilter(taalKeuzeUit);
				//KIES ALLE FILMS MET DE JUISTE TAAL
				if (taalGekozen?.Count > 0)
				{
					int i = 0;
					while (i < filmsZoeken?.Count)
					{
						var filmsCheckList = filmsZoeken[i].Taal;
						if (filmsCheckList != null)
						{
							//TAAL GEVONDEN
							if (filmsCheckList.Any(x => taalGekozen.Any(y => y == x))) { i++; } else
							//TAAL ZIT ER NIET IN
							{
								filmsZoeken.RemoveAt(i);
							}
						}
					}
				}
			}
			Console.Clear();
		}
		//ZOEK OP GENRE
		private void GenreZoeken()
		{
			Console.WriteLine("Wilt op zoek naar een specifieke genres?\n");
			string[] JaNee = { "Ja", "Nee" };
			int KeuzeGenre = KeuzeEen(JaNee) + 1;
			if (KeuzeGenre == 1)
			{
				//LIJST EN ARRAY OM TE CHECKEN
				string[] genreKeuzeUit = { "actie", "animatie", "avontuur", "documentaire", "drama", "familie", "fantasy", "historisch", "horror", "komedie", "misdaad", "mystery", "oorlog", "romantisch", "sci-fi", "GEKOZEN" };
				//PRINT DE OPTIES
				Console.WriteLine($"\n\n\n\nU kunt telkens een genre kiezen, de keuze stopt tot u 'GEKOZEN' kiest.\n");
				//FILTER
				List<string> genresGekozen = keuzeFilter(genreKeuzeUit);
				Console.WriteLine("");
				//KIES ALLE FILMS VAN DE GENRES
				if (genresGekozen?.Count > 0)
				{
					int i = 0;
					while (i < filmsZoeken?.Count)
					{
						var filmsCheckList = filmsZoeken[i].Genre;
						if (filmsCheckList != null)
						{
							//GENRE GEVONDEN
							if (filmsCheckList.Any(x => genresGekozen.Any(y => y == x))) { i++; } else
							//GENRE ZIT ER NIET IN
							{
								filmsZoeken.RemoveAt(i);
							}
						}
					}
				}
			}
			Console.Clear();
		}
		//KEUZEMENU VOOR 1 KEUZE
		private int KeuzeEen(string[] options)
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
					} else
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
						} else
						{
							currentSelected -= 1;
						}
						break;
					case ConsoleKey.DownArrow:
						if (currentSelected == options.Length - 1)
						{
							break;
						} else
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
		//KEUZEMENU VOOR FILTEREN
		private List<string> keuzeFilter(string[] options)
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
					} else
					{
						Console.ForegroundColor = ConsoleColor.White;
						Console.Write(" ");
					}
					if (selected.Contains(i))
					{
						Console.ForegroundColor = ConsoleColor.DarkCyan;
						Console.WriteLine("  {0}", options[i] + " - TOEGEVOEGD");
					} else if ((options.Length - 1) == i)
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine("  {0}", options[i]);
					} else
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
						} else
						{
							currentSelected -= 1;
						}
						break;
					case ConsoleKey.DownArrow:
						if (currentSelected == options.Length - 1)
						{
							break;
						} else
						{
							currentSelected += 1;
						}
						break;
					case ConsoleKey.Enter:
						if (currentSelected == (options.Length-1))
						{
							return gekozen;
						}
						else {
							if (!gekozen.Contains(options[currentSelected])) 
							{ 
								gekozen.Add(options[currentSelected]);
								selected.Add(currentSelected);
							}
						}
						break;
				}
				// Zorgt ervoor dat de keuzes niet met elkaar gaan overlappen.
				Console.CursorTop = Console.CursorTop - options.Length;
			}
			return gekozen;
		}
	}
}
