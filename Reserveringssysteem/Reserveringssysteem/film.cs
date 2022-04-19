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
			Console.WriteLine("Wilt op zoek naar een specifieke titel? Typ 1 van de volgende nummer in:\n1. Ja\n2. Nee");
			var keuzeTitel = Console.ReadLine();
			while (keuzeTitel != "1" && keuzeTitel != "2")
			{
				Console.WriteLine("Kies uit 1 of 2.");
				keuzeTitel = Console.ReadLine();
			}
			if (keuzeTitel == "1")
			{
				Console.WriteLine("\n");
				Console.WriteLine("Welk titel wil u kiezen?");
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
			Console.WriteLine("Wilt u kiezen uit een taal?\n1: Ja\n2: Nee");
			var keuzeTaal = Console.ReadLine();
			while (keuzeTaal != "1" && keuzeTaal != "2")
			{
				Console.WriteLine("Kies uit 1 of 2.");
				keuzeTaal = Console.ReadLine();
			}
			Console.WriteLine("\n");
			if (keuzeTaal == "1")
			{
				//LIJST EN ARRAY OM TE CHECKEN
				List<string> taalGekozen = new();
				string[] taalKeuzeUit = { "Nederlands", "Engels", "Spaans", "Duits", "Japans" };
				string taalOpties = "";
				//PRINT DE OPTIES
				for (int i = 0; i < taalKeuzeUit.Length; i++)
				{
					taalOpties += "- " + taalKeuzeUit[i] + "\n";
				}
				string keuzesofKeuzeTaal = taalKeuzeUit.Length == 1 ? "keuze is" : "keuzes zijn";
				Console.WriteLine($"De {keuzesofKeuzeTaal}:\n{taalOpties}U kunt telkens een taal kiezen, de keuze stopt tot u een lege input geeft");
				//FILTER
				string taalKeuze = "nothing";
				while (!string.IsNullOrWhiteSpace(taalKeuze))
				{
					taalKeuze = Console.ReadLine();
					if ((taalKeuze != null) && (!string.IsNullOrWhiteSpace(taalKeuze))) { taalKeuze = char.ToUpper(taalKeuze[0]) + taalKeuze[1..].ToLower(); }
					//STOP BIJ LEGE INPUT OF ALLES IS GEKOZEN
					if (string.IsNullOrWhiteSpace(taalKeuze)) { break; }
					//JUIST GEKOZEN
					else if (taalKeuzeUit.Contains(taalKeuze))
					{
						if (taalGekozen != null)
						{
							//GENRE IS EERDER NIET GEKOZEN: VOEG TOE AAN LIST
							if (!taalGekozen.Contains(taalKeuze))
							{
								taalGekozen?.Add(taalKeuze);
								Console.WriteLine($"{taalKeuze} is toegevoegd!");
							} else { Console.WriteLine($"{taalKeuze} heeft u al eerder gekozen!"); }
						}
					} else
					{
						Console.WriteLine("Keuze niet geldig");
					}
					Console.WriteLine("\n");
					//STOP ALS ALLES IS GEKOZEN
					if (taalKeuzeUit.Length == taalGekozen?.Count) { break; }
				}
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
			Console.WriteLine("Wilt op zoek naar een specifieke genres? Typ 1 van de volgende nummer in:\n1. Ja\n2. Nee");
			var keuzeGenre = Console.ReadLine();
			while (keuzeGenre != "1" && keuzeGenre != "2")
			{
				Console.WriteLine("Kies uit 1 of 2.");
				keuzeGenre = Console.ReadLine();
			}
			Console.WriteLine("\n");
			if (keuzeGenre == "1")
			{
				//LIJST EN ARRAY OM TE CHECKEN
				List<string> genresGekozen = new();
				string[] genreKeuzeUit = { "actie", "animatie", "avontuur", "documentaire", "drama", "familie", "fantasy", "historisch", "horror", "komedie", "misdaad", "mystery", "oorlog", "romantisch", "sci-fi" };
				string GenreOpties = "";
				//PRINT DE OPTIES
				for (int i = 0; i < genreKeuzeUit.Length; i++)
				{
					GenreOpties += "- " + genreKeuzeUit[i] + "\n";
				}
				string keuzesofKeuzeGenre = genreKeuzeUit.Length == 1 ? "keuze is" : "keuzes zijn";
				Console.WriteLine($"De {keuzesofKeuzeGenre}:\n{GenreOpties}U kunt telkens een genre kiezen, de keuze stopt tot u een lege input geeft of alles is gekozen");
				//FILTER
				var genreKeuze = "nothing";
				while (!string.IsNullOrWhiteSpace(genreKeuze))
				{
					genreKeuze = Console.ReadLine();
					//STOP BIJ LEGE INPUT OF ALLES IS GEKOZEN
					if (string.IsNullOrWhiteSpace(genreKeuze)) { break; }
					//JUIST GEKOZEN
					else if (genreKeuzeUit.Contains(genreKeuze.ToLower()))
					{
						if (genresGekozen != null)
						{
							//GENRE IS EERDER NIET GEKOZEN: VOEG TOE AAN LIST
							if (!genresGekozen.Contains(genreKeuze.ToLower()))
							{
								genresGekozen?.Add(genreKeuze.ToLower());
								Console.WriteLine($"{genreKeuze.ToLower()} is toegevoegd!");
							} else { Console.WriteLine($"{genreKeuze.ToLower()} heeft u al eerder gekozen!"); }
						}
					} else
					{
						Console.WriteLine("Keuze niet geldig");
					}
					Console.WriteLine("\n");
					//STOP ALS ALLES IS GEKOZEN
					if (genreKeuzeUit.Length == genresGekozen?.Count) { break; }
				}
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
	}
}
