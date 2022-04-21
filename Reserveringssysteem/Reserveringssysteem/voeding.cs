using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.IO;

namespace Reserveringssysteem
{
	public class voeding
	{
		public string naam;
		public Dictionary<string, voedingswaarde> voedingswaardeList;

		public voeding(string naamVoedsel)
		{
			naam = naamVoedsel;
		}

		public string returnVoedingswaarde()
		{
			readJson();
			string returnString = "";
			for(int i = 0; i < voedingswaardeList.Count; i++)
			{
				if (voedingswaardeList.ContainsKey(this.naam))
				{
					returnString = $"Eenheid per {voedingswaardeList[naam].Eenheid}\n- Calorieën: {voedingswaardeList[naam].Calorieen}\n- Eiwitten: {voedingswaardeList[naam].Eiwitten}\n- Koolhydraten: {voedingswaardeList[naam].Koolhydraten}\n  - Waarvan suikers: {voedingswaardeList[naam].waarvanSuikers}\n- Vet: {voedingswaardeList[naam].Vet}\n  - {voedingswaardeList[naam].waarvanVerzadigd}\n- Vezels: {voedingswaardeList[naam].Vezels}";
				} else
				{
					return "Er ging iets mis.";
				}
			}
			return returnString;
		}

		//JSON STUFF//LEES JSON
		public void readJson()
		{
			var json = File.ReadAllText("../../../voeding.json", Encoding.GetEncoding("utf-8"));
			this.voedingswaardeList = JsonConvert.DeserializeObject<Dictionary<string, voedingswaarde>>(json);
		}
		//WAARDEN VAN DE JSON
		public class voedingswaarde
		{
			//public Dictionary<string, string> voedingsNaam { get; set; }
			public string Eenheid { get; set; }
			public string Calorieen { get; set; }
			public string Eiwitten { get; set; }
			public string Koolhydraten { get; set; }
			public string waarvanSuikers { get; set; }
			public string Vet { get; set; }
			public string waarvanVerzadigd { get; set; }
			public string Vezels { get; set; }
		}
	}
}
