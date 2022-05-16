using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Globalization;

namespace Reserveringssysteem
{
	public class draaiendeFilms
	{
		public List<draaienFilms> draaienFilmsList;
		public List<DateTime> datumsDraaien = new List<DateTime>();
		public DateTime[] datumsDraaienArray;
		public string[] datumsDraaienString;

		public draaiendeFilms()
		{
			var jsonFilmsDraaien = File.ReadAllText("../../../DataFiles/gedraaideFilms.json", Encoding.GetEncoding("utf-8"));
			this.draaienFilmsList = JsonConvert.DeserializeObject<List<draaienFilms>>(jsonFilmsDraaien);
		}

		public class draaienFilms
		{
			public int FilmID { get; set; }
			public string Name { get; set; }
			public List<string> Datum { get; set; }
			public List<int> Zaal { get; set; }

			public draaienFilms(int filmId, string name, List<string> datum, List<int> zaal)
            {
				this.FilmID = filmId;
				this.Name = name;
				this.Datum = datum;
				this.Zaal = zaal;
            }
		}

		//LAAT ALLE FILMS ZIEN DIE BINNEKORT DRAAIEN
		public List<draaienFilms> laatDraaiendeFilmsZien()
		{
			//VERWIJDER ALLE DATUMS DIE AL ZIJN GEWEEST
			for(int i = 0; i < draaienFilmsList.Count; i++)
			{
				int DatumremoveAt = 0;
				int runFor = draaienFilmsList[i].Datum.Count;
				for (int j = 0; j < runFor; j++)
				{
					DateTime datumdraaien = DateTime.Parse(draaienFilmsList[i].Datum[DatumremoveAt]);
					if (datumdraaien < DateTime.Now.AddDays(-1))
					{
						draaienFilmsList[i].Datum.RemoveAt(DatumremoveAt);
						draaienFilmsList[i].Zaal.RemoveAt(DatumremoveAt);
					} else
					{
						DatumremoveAt++;
					}
				}
			}
			//ALS ER GEEN DATUMS MEER ZIJN, VERWIJDER VAN HELE LIST
			int ListremoveAt = 0;
			int LengteDraaienFilmList = draaienFilmsList.Count;
			for (int i = 0; i < LengteDraaienFilmList; i++)
			{
				if (draaienFilmsList[ListremoveAt].Datum.Count <= 0)
				{
					draaienFilmsList.RemoveAt(ListremoveAt);
					ListremoveAt--;
				}
				ListremoveAt++;
			}

			UpdateDraaienFilms();

			return draaienFilmsList;
		}

		//LAAT FILMS DETAILS ZIEN ZOALS DATUMS EN ZAAL
		public string filmDatumDetails(string titel)
		{
			string returnTekst = $"   {titel}\n\n";
			//List<DateTime> datumWanneerFilmDraait;
			//LAAD JSON DATA IN OM DETAILS TE LATEN ZIEN
			var jsonFilms = File.ReadAllText("../../../DataFiles/films.json", Encoding.GetEncoding("utf-8"));
			var filmsList = JsonConvert.DeserializeObject<List<Film>>(jsonFilms);

			for(int i = 0; i < filmsList.Count; i++)
			{
				if (filmsList[i].Titel == titel)
				{
					string plot = filmsList[i].Plot;
					string[] words = plot.Split(' ');
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

					returnTekst += String.Join(" ", words);
				}
			}
			returnTekst += "\n\n   Beschikbare Datums:\n";
			//LAAD DE DATUM IN
			for(int i = 0; i < draaienFilmsList.Count; i++)
			{
				if (draaienFilmsList[i].Name == titel)
				{
					for(int j = 0; j < draaienFilmsList[i].Datum.Count; j++)
					{
						DateTime datumDraaien = DateTime.Parse(draaienFilmsList[i].Datum[j]);
						if (datumDraaien > DateTime.Now.AddDays(-1))
						{
							this.datumsDraaien.Add(datumDraaien);
						}
					}
				}
			}
			this.datumsDraaienArray = datumsDraaien.ToArray();
			//CONVERTEER NAAR STRING ARRAY
			datumsDraaienString = new string[datumsDraaienArray.Length + 1];
			for(int i = 0; i < this.datumsDraaienArray.Length; i++)
			{
				CultureInfo netherlands = new CultureInfo("nl-NL");
				string dayOfWeek = netherlands.DateTimeFormat.GetDayName(datumsDraaienArray[i].DayOfWeek);
				dayOfWeek = char.ToUpper(dayOfWeek[0]) + dayOfWeek.Substring(1);
				datumsDraaienString[i] = $"{dayOfWeek}, {datumsDraaienArray[i].Day} {datumsDraaienArray[i].ToString("MMM")} {datumsDraaienArray[i].Year}, {datumsDraaienArray[i].Hour}:{datumsDraaienArray[i].ToString("mm")}";
			}
			datumsDraaienString[datumsDraaienArray.Length] = "Ga terug";

			return returnTekst;
		}

		//UPDATE LIJST MET GEDRAAIDE FILMS
		public void UpdateDraaienFilms()
		{
			var draaienFilms = GetDraaienFilms();
			var stringifiedDraaienFilms = JsonConvert.SerializeObject(draaienFilms, Formatting.Indented);
			File.WriteAllText("../../../DataFiles/gedraaideFilms.json", stringifiedDraaienFilms);
		}

		public List<draaienFilms> GetDraaienFilms()
        {
			return draaienFilmsList;
        }
	}
}