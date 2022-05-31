using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Reserveringssysteem.Classes
{
	public class Reservering
	{
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

		public Reservering(string titel, int aantalPersonen, DateTime datum, int zaal, string[] namen, string username, int[] leeftijden, string[] stoelen)
		{
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
			System.IO.File.WriteAllText("DataFiles/reserveringen.json", JsonData, Encoding.UTF8);
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