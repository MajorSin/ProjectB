using System;
using System.Collections.Generic;

namespace Reserveringssysteem
{
	public class Film
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

		public Film(int id, string title, int jaar, List<string> taal, int looptijd, List<string> genre, string directeur, string acteurs, string plot)
		{
			this.Id = id;
			this.Titel = title;
			this.Jaar = jaar;
			this.Taal = taal;
			this.Looptijd = looptijd;
			this.Genre = genre;
			this.Directeur = directeur;
			this.Acteurs = acteurs;
			this.Plot = plot;
		}

		public override string ToString()
		{
			//KRIJG DE TALEN
			int taalLength = this?.Taal?.Count ?? 0;
			string talen = "";
			for (int i = 0; i < taalLength; i++)
			{
				talen += this?.Taal?[i];
				//VOEG DE JUISTE TEKEN TOE
				if (taalLength > 1)
				{
					if ((taalLength - 2) == i)
					{
						talen += " & ";
					}
					else if ((taalLength - 1) != i)
					{
						talen += ", ";
					}
				}
			}

			string taalString = taalLength == 1 ? "Taal" : "Talen";
			//KRIJG DE GENRE(S)
			int genreLength = this?.Genre?.Count ?? 0;
			string genres = "";
			for (int i = 0; i < genreLength; i++)
			{
				genres += this?.Genre?[i];
				//VOEG DE JUISTE TEKEN TOE
				if (genreLength > 1)
				{
					if ((genreLength - 2) == i)
					{
						genres += " & ";
					}
					else if ((genreLength - 1) != i)
					{
						genres += ", ";
					}
				}
			}

			string genreString = genreLength == 1 ? "Genre" : "Genres";
			string looptijdString = this.Looptijd.ToString();

			// CONTROLEERT DE LOOPTIJD EN HET AANTAL UREN
			if (this?.Looptijd > 60)
			{
				int? uur = this.Looptijd / 60;
				int? minuten = this.Looptijd - (uur * 60);
				string uurString = uur == 1 ? "uur" : "uren";
				string minuutString = minuten == 1 ? "minuut" : "minuten";
				looptijdString = $"{uur} {uurString} en {minuten} {minuutString}";
			}

			string[] acteurs = this.Acteurs.Split(',');
			if (acteurs.Length > 4)
            {
				string[] acteursReduced = new string[4];
				for (int i = 0; i < acteursReduced.Length; i++)
                {
					acteursReduced[i] = acteurs[i];
                }
				acteurs = acteursReduced;
            }

			string[] plot = this.Plot.Split(" ");
			string[] plotReduced = new string[10];
			for (int i = 0; i < plotReduced.Length; i++)
            {
				plotReduced[i] = plot[i];
            }
			plot = plotReduced;

			string title = this.Titel;

			if (title.Length > 25)
			{
				title = title.Substring(0, 25) + "...";
			}

            return String.Format(
                "   Titel: {0} ({1})\n" +
                "   {2}: {3}\n" +
                "   Looptijd: {4}\n" +
                "   {5}: {6}\n" +
                "   Acteurs: {7}\n" +
                "   Plot: {8}",
                this.Titel, this.Jaar, taalString, talen, looptijdString, genreString,
                genres, String.Join(",", acteurs) + "...", String.Join(" ", plot) + "...\n"
            );
        }
	}
}
