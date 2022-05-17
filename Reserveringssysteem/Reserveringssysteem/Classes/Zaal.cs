using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Reserveringssysteem.Classes
{
	public class Zaal
	{
		public int zaalNummer;
		public int zaalIndex;
		public string json;
		public List<plattegrondJson> plattegronden;
		public Zaal(int zaalNummer)
		{
			this.zaalNummer = zaalNummer;
			//ZOEK NAAR DE INDEX VAN PLATTEGROND.JSON
			readJson();
			int zaalIndex = 0;
			for (int i = 0; i < plattegronden?.Count; i++)
			{
				if (plattegronden[i].Plattegrond == zaalNummer)
				{
					zaalIndex = i;
					break;
				}
			}
			this.zaalIndex = zaalIndex;
		}
		public class plattegrondJson
		{
			public int? Plattegrond { get; set; }
			public int? Rijen { get; set; }
			public int? Stoelen { get; set; }
			public string Scherm { get; set; }
		}
		public void readJson()
		{
			//LEEST JSON
			this.json = File.ReadAllText("../../../DataFiles/plattegrond.json", Encoding.GetEncoding("utf-8"));
			this.plattegronden = JsonConvert.DeserializeObject<List<plattegrondJson>>(json);
		}
		public string zaal()
		{
			//GEEFT DE ZAAL TERUG
			string plattegrond = "   ";
			readJson();
			for (int rij = 0; rij < plattegronden?[zaalIndex].Rijen; rij++)
			{
				if(rij == 0)
				{
					for(int i = 0; i < plattegronden?[zaalIndex].Stoelen; i++)
					{
						int adder = i + 1;
						if (adder > 9)
						{
							plattegrond += (i + 1) + " ";
						} else 
						{ 
							plattegrond += " " + (i + 1) + " ";
						}
					}
					plattegrond += "\n";
				}
				if(rij < 9) 
				{ 
					plattegrond += (rij+1) + "  ";
				} else
				{
					plattegrond += rij + 1 + " ";
				}
				for (int stoel = 0; stoel < plattegronden?[zaalIndex].Stoelen; stoel++)
				{
					plattegrond += "[ ]";
				}
				plattegrond += "\n";
			}
			plattegrond += plattegronden?[zaalIndex].Scherm + "\n";
			return plattegrond;
		}
	}
}
