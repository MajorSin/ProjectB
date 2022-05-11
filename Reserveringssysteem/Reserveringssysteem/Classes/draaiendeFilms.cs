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
	}
}
