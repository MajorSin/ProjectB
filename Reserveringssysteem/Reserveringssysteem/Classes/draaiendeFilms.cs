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
				int removeAt = 0;
				int runFor = draaienFilmsList[i].Datum.Count;
				for (int j = 0; j < runFor; j++)
				{
					DateTime datumdraaien = DateTime.Parse(draaienFilmsList[i].Datum[removeAt]);
					if (datumdraaien < DateTime.Now)
					{
						draaienFilmsList[i].Datum.RemoveAt(removeAt);
						draaienFilmsList[i].Zaal.RemoveAt(removeAt);
					} else
					{
						removeAt++;
					}
				}
			}
			//ALS ER GEEN DATUMS MEER ZIJN, VERWIJDER VAN HELE LIST
			for (int i = 0; i < draaienFilmsList.Count; i++)
			{ 
				if (draaienFilmsList[i].Datum.Count <= 0)
				{
					draaienFilmsList.RemoveAt(i);
				}
			}
			return draaienFilmsList;
		}
	}
}
