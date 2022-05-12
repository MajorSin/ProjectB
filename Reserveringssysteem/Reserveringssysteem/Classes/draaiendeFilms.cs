using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Reserveringssysteem.Classes
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
			public List<string> Zaal { get; set; }
		}
		public class film
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
					if (datumdraaien < DateTime.Now)
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
			return draaienFilmsList;
		}

		//LAAT FILMS DETAILS ZIEN ZOALS DATUMS EN ZAAL
		public string filmDatumDetails(string titel)
		{
			string returnTekst = $"{titel}\n\n";
			//List<DateTime> datumWanneerFilmDraait;
			//LAAD JSON DATA IN OM DETAILS TE LATEN ZIEN
			var jsonFilms = File.ReadAllText("../../../DataFiles/films.json", Encoding.GetEncoding("utf-8"));
			var filmsList = JsonConvert.DeserializeObject<List<film>>(jsonFilms);
			for(int i = 0; i < filmsList.Count; i++)
			{
				if (filmsList[i].Titel == titel)
				{
					returnTekst += filmsList[i].Plot;
				}
			}
			returnTekst += "\n\n\nBeschikbare Datums:\n";
			//LAAD DE DATUM IN
			for(int i = 0; i < draaienFilmsList.Count; i++)
			{
				if (draaienFilmsList[i].Name == titel)
				{
					for(int j = 0; j < draaienFilmsList[i].Datum.Count; j++)
					{
						DateTime datumDraaien = DateTime.Parse(draaienFilmsList[i].Datum[j]);
						if(datumDraaien > DateTime.Now)
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
				datumsDraaienString[i] = $"{datumsDraaienArray[i].DayOfWeek}, {datumsDraaienArray[i].Day} {datumsDraaienArray[i].ToString("MMM")} {datumsDraaienArray[i].Year}, {datumsDraaienArray[i].Hour}:{datumsDraaienArray[i].Minute}";
			}
			datumsDraaienString[datumsDraaienArray.Length] = "Ga terug";
			return returnTekst;
		}
	}
}