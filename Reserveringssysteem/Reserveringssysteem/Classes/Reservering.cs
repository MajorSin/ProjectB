using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Reserveringssysteem
{
	public class Reservering
	{
		public int Id { get; set; }
		public string Titel { get; set; }
		public int AantalPersonen { get; set; }
		public string Datum { get; set; }
		public int Zaal { get; set; }
		public string[] Namen { get; set; }
		public string Username { get; set; }
		public int[] Leeftijden { get; set; }
		public string[] Stoelen { get; set; }
		public string UniekeCode { get; set; }

		public List<reserveringenJson> reserveringen;

		public Reservering(int id, string titel, int aantalPersonen, DateTime datum, int zaal, string[] namen, string username, int[] leeftijden, string[] stoelen)
		{
			Id = id;
			Titel = titel;
			AantalPersonen = aantalPersonen;
			CultureInfo netherlands = CultureInfo.GetCultureInfo("nl-NL");
			Datum = datum.ToString("o", netherlands);
			Zaal = zaal;
			Namen = namen;
			Username = username;
			Leeftijden = leeftijden;
			Stoelen = stoelen;
			UniekeCode = RandomCode();
		}
		//ZET DE RESERVERING IN JSON BESTAND
		public void addToJsonFile()
		{
			readJson();
			reserveringenJson nieuwEntry = new reserveringenJson();
			nieuwEntry.id = Id;
			nieuwEntry.titel = Titel;
			nieuwEntry.aantalPersonen = AantalPersonen;
			nieuwEntry.datum = Datum;
			nieuwEntry.zaal = Zaal;
			nieuwEntry.namen = new List<string>(Namen);
			nieuwEntry.ReserveringDoor = Username;
			nieuwEntry.leeftijden = new List<int>(Leeftijden);
			nieuwEntry.stoelen = Stoelen;
			nieuwEntry.randomCode = UniekeCode;
			reserveringen.Add(nieuwEntry);
			var JsonData = JsonConvert.SerializeObject(reserveringen, Formatting.Indented);
			File.WriteAllText("DataFiles/reserveringen.json", JsonData, Encoding.UTF8);
		}
		public string RandomCode()
		{
			Random rand = new Random();
			string randomCode = "";
			int lengte = 7;
			for (int i = 0; i < lengte; i++)
			{
				var cijfer = rand.Next(48, 58);
				var randomGroteLetter = rand.Next(65, 91);
				var randomKleineLetter = rand.Next(97, 123);
				var keuze = rand.Next(1, 4);
				if (keuze == 1)
				{
					randomCode += ((char)cijfer);
				} else if (keuze == 2)
				{
					randomCode += ((char)randomGroteLetter);
				} else
				{
					randomCode += ((char)randomKleineLetter);
				}
			}
			return randomCode;
		}
		//JSON READERS
		public void readJson()
		{
			string jsonReserveringen = File.ReadAllText("DataFiles/reserveringen.json", Encoding.GetEncoding("utf-8"));
			this.reserveringen = JsonConvert.DeserializeObject<List<reserveringenJson>>(jsonReserveringen);
		}

		public static List<reserveringenJson> GetReserveringen()
        {
			string jsonReserveringen = File.ReadAllText("DataFiles/reserveringen.json", Encoding.GetEncoding("utf-8"));
			return JsonConvert.DeserializeObject<List<reserveringenJson>>(jsonReserveringen);
		}

		public class reserveringenJson
		{
			public int id { get; set; }
			public string titel { get; set; }
			public int aantalPersonen { get; set; }
			public string datum { get; set; }
			public int zaal { get; set; }
			public List<string> namen { get; set; }
			public string ReserveringDoor { get; set; }
			public List<int> leeftijden { get; set; }
			public string[] stoelen { get; set; }
			public string randomCode { get; set; }

			public override string ToString()
			{
				DateTime datumFilm = DateTime.Parse(datum);
				string volledigeDatum = "";
				CultureInfo netherlands = new CultureInfo("nl-NL");
				string dayOfWeek = netherlands.DateTimeFormat.GetDayName(datumFilm.DayOfWeek);
				dayOfWeek = char.ToUpper(dayOfWeek[0]) + dayOfWeek.Substring(1);
				volledigeDatum = $"{dayOfWeek}, {datumFilm.Day} {datumFilm.ToString("MMM")} {datumFilm.Year}, {datumFilm.Hour}:{datumFilm.ToString("mm")}.";

				string[] stoelenArr = stoelen;
				Tuple<string, string>[] plaatsen = new Tuple<string, string>[stoelenArr.Length];

				for (int i = 0; i < stoelenArr.Length; i++)
				{
					string[] plaats = stoelenArr[i].Split(':');
					plaatsen[i] = Tuple.Create(plaats[0], plaats[1]);
				}

				string rij = plaatsen[0].Item2;
				string stoelenStr = "";

				for (int i = 0; i < plaatsen.Length; i++)
				{
					if (plaatsen.Length > 1)
					{
						if (i == plaatsen.Length - 1)
						{
							stoelenStr += $"en {plaatsen[i].Item1}";
						}
						else
						{
							stoelenStr += plaatsen[i].Item1 + ", ";
						}
					}
					else
					{
						stoelenStr += plaatsen[i].Item1;
					}
				}

				return String.Format(
					"   Titel: {0} \n" +
					"   Aantal personen: {1}\n" +
					"   Datum: {2}\n" +
					"   Zaal: {3}\n" +
					"   Rij nummer: {4}\n" +
					"   Stoel nummer: {5}\n" +
					"   Code: {6}",
					titel, aantalPersonen, volledigeDatum, zaal, rij, stoelenStr, randomCode, "\n\n"
				);
			}
		}
	}
}