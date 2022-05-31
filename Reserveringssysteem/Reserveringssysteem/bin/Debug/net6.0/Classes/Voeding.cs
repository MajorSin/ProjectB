using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Reserveringssysteem
{
	public class Voeding
	{
		public string naam;
		public Dictionary<string, Voedingswaarde> voedingswaardeList;

		public Voeding(string naamVoedsel)
		{
			naam = naamVoedsel;
		}

		public string returnVoedingswaarde()
		{
			readJson();
			string returnString = "";
			for (int i = 0; i < voedingswaardeList.Count; i++)
			{
				if (voedingswaardeList.ContainsKey(this.naam))
				{
					returnString = $"   Eenheid per {voedingswaardeList[naam].Eenheid}\n   - Energie: {voedingswaardeList[naam].Energie}\n   - Eiwitten: {voedingswaardeList[naam].Eiwitten}\n   - Koolhydraten: {voedingswaardeList[naam].Koolhydraten}\n     - Waarvan suikers: {voedingswaardeList[naam].waarvanSuikers}\n   - Vet: {voedingswaardeList[naam].Vet}\n     - Waarvan Verzadigd: {voedingswaardeList[naam].waarvanVerzadigd}\n   - Vezels: {voedingswaardeList[naam].Vezels}";
				}
				else
				{
					return "Er ging iets mis.";
				}
			}
			return returnString;
		}

		//JSON STUFF//LEES JSON
		public void readJson()
		{
			var json = File.ReadAllText("DataFiles/voeding.json", Encoding.GetEncoding("utf-8"));
			this.voedingswaardeList = JsonConvert.DeserializeObject<Dictionary<string, Voedingswaarde>>(json);
		}
		//WAARDEN VAN DE JSON
		public class Voedingswaarde
		{
			//public Dictionary<string, string> voedingsNaam { get; set; }
			public string Eenheid { get; set; }
			public string Energie { get; set; }
			public string Eiwitten { get; set; }
			public string Koolhydraten { get; set; }
			public string waarvanSuikers { get; set; }
			public string Vet { get; set; }
			public string waarvanVerzadigd { get; set; }
			public string Vezels { get; set; }
		}
	}
}
