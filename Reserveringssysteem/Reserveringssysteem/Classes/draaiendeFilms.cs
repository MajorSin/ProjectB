using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Reserveringssysteem
{
	public class draaiendeFilms
	{
		public List<draaienFilms> draaienFilmsList;
		public List<DateTime> datumsDraaien;
		public DateTime[] datumsDraaienArray;
		public List<int> zaalWaarinFilmDraait;
		public string[] datumsDraaienString;

		public draaiendeFilms()
		{
			var jsonFilmsDraaien = File.ReadAllText("DataFiles/gedraaideFilms.json", Encoding.GetEncoding("utf-8"));
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
			for (int i = 0; i < draaienFilmsList.Count; i++)
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
			zaalWaarinFilmDraait = new List<int>();
			datumsDraaien = new List<DateTime>();
			string returnTekst = $"   {titel}\n\n";
			//LAAD JSON DATA IN OM DETAILS TE LATEN ZIEN
			var jsonFilms = File.ReadAllText("DataFiles/films.json", Encoding.GetEncoding("utf-8"));
			var filmsList = JsonConvert.DeserializeObject<List<Film>>(jsonFilms);

			for (int i = 0; i < filmsList.Count; i++)
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
			for (int i = 0; i < draaienFilmsList.Count; i++)
			{
				if (draaienFilmsList[i].Name == titel)
				{
					for (int j = 0; j < draaienFilmsList[i].Datum.Count; j++)
					{
						DateTime datumDraaien = DateTime.Parse(draaienFilmsList[i].Datum[j]);
						if (datumDraaien > DateTime.Now.AddDays(-1))
						{
							this.datumsDraaien.Add(datumDraaien);
							zaalWaarinFilmDraait.Add(draaienFilmsList[i].Zaal[j]);
						}
					}
				}
			}
			this.datumsDraaienArray = datumsDraaien.ToArray();
			//CONVERTEER NAAR STRING ARRAY
			datumsDraaienString = new string[datumsDraaienArray.Length + 1];
			for (int i = 0; i < this.datumsDraaienArray.Length; i++)
			{
				CultureInfo netherlands = new CultureInfo("nl-NL");
				string dayOfWeek = netherlands.DateTimeFormat.GetDayName(datumsDraaienArray[i].DayOfWeek);
				dayOfWeek = char.ToUpper(dayOfWeek[0]) + dayOfWeek.Substring(1);
				datumsDraaienString[i] = $"{dayOfWeek}, {datumsDraaienArray[i].Day} {datumsDraaienArray[i].ToString("MMM")} {datumsDraaienArray[i].Year}, {datumsDraaienArray[i].Hour}:{datumsDraaienArray[i].ToString("mm")}. In zaal: {zaalWaarinFilmDraait[i]}";
			}
			datumsDraaienString[datumsDraaienArray.Length] = "Ga terug";

			return returnTekst;
		}

		//UPDATE LIJST MET GEDRAAIDE FILMS
		public void UpdateDraaienFilms()
		{
			var draaienFilms = GetDraaienFilms();
			var stringifiedDraaienFilms = JsonConvert.SerializeObject(draaienFilms, Formatting.Indented);
			File.WriteAllText("DataFiles/gedraaideFilms.json", stringifiedDraaienFilms);
		}

		public List<draaienFilms> GetDraaienFilms()
		{
			return draaienFilmsList;
		}
		//KIJKT OF RESERVERING OVERLOOPT MET EEN ANDER RESERVERING
		public bool checkDubbeleReservering(string titel, string username, DateTime datumwanneerFilmDraait)
		{
			int? looptijd = 0;
			//KRIJG LOOPTIJD
			FilmController filmClass = new FilmController();
			FilmController.SetFilms();
			var films = filmClass.GetFilms();
			foreach(Film film in films)
			{
				if(film.Titel == titel)
				{
					looptijd = film.Looptijd;
					break;
				}
			}
			looptijd = looptijd!=null ? looptijd : 0;
			DateTime datumwanneerfilmeindigt = datumwanneerFilmDraait.AddMinutes((double)looptijd);
			//KIJK OF DATUM OVERLAPT
			string jsonReserveringen = File.ReadAllText("DataFiles/reserveringen.json", Encoding.GetEncoding("utf-8"));
			var reserveringen = JsonConvert.DeserializeObject<List<reserveringenJson>>(jsonReserveringen);
			foreach(reserveringenJson reservering in reserveringen)
			{
				if(reservering.ReserveringDoor == username)
				{
					DateTime datumOpReservering = DateTime.Parse(reservering.datum);
					//LOOP DOOR DE FILM TITEL VAN RESERVERING OM DIT OOK TE CHECKEN
					int? loopTijdOpReservering = 0;
					foreach (Film film in films)
					{
						if (film.Titel == reservering.titel)
						{
							loopTijdOpReservering = film.Looptijd;
							break;
						}
					}
					DateTime datumEindeOpReservering = datumOpReservering.AddMinutes((double)loopTijdOpReservering);
					//CHECK CONDITIES
					if ((datumwanneerFilmDraait <= datumOpReservering && datumOpReservering <= datumwanneerfilmeindigt) || (datumOpReservering <= datumwanneerFilmDraait && datumwanneerFilmDraait <=datumEindeOpReservering))
					{
						return true;
					}
				}
			}
			return false;
		}
		
		public class reserveringenJson
		{
			public string titel { get; set; }
			public int aantalPersonen { get; set; }
			public string datum { get; set; }
			public int zaal { get; set; }
			public List<string> namen { get; set; }
			public string ReserveringDoor { get; set; }
			public List<int> leeftijden { get; set; }
			public string[] stoelen { get; set; }
			public string randomCode { get; set; }
		}
	}
}