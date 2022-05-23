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
		public int rijen;
		public int stoelen;
		public List<plattegrondJson> plattegronden;
		public List<reserveringenJson> reserveringen;
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
			this.rijen = plattegronden[zaalIndex].Rijen;
			this.stoelen = plattegronden[zaalIndex].Stoelen;
		}
		public class plattegrondJson
		{
			public int Plattegrond { get; set; }
			public int Rijen { get; set; }
			public int Stoelen { get; set; }
			public string Scherm { get; set; }
		}
		public void readJson()
		{
			//LEEST JSON
			string json = File.ReadAllText("../../../DataFiles/plattegrond.json", Encoding.GetEncoding("utf-8"));
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
		//PRINT DE ZAAL UIT MET JUISTE KLEUREN
		public void printZaal(string titel, DateTime datumVoorDraaien, int zaal)
		{
			readJson();
			leesJsonReserveringen();
			for (int rij = 0; rij < plattegronden?[zaalIndex].Rijen; rij++)
			{
				//PRINT DE STOELNUMMERS
				if (rij == 0)
				{
					Console.Write("   ");
					for (int i = 0; i < plattegronden?[zaalIndex].Stoelen; i++)
					{
						int adder = i + 1;
						if (adder > 9)
						{
							Console.Write((i + 1) + " ");
						} else
						{
							Console.Write(" " + (i + 1) + " ");
						}
					}
					Console.Write("\n");
				}
				if (rij < 9)
				{
					Console.Write((rij + 1) + "  ");
				} else
				{
					Console.Write(rij + 1 + " ");
				}
				//PRINT DE STOEL
				for (int stoel = 0; stoel < plattegronden?[zaalIndex].Stoelen; stoel++)
				{
					//KIJK OF STOEL IS GERESERVEERD
					foreach(reserveringenJson reservering in reserveringen)
					{
						if(reservering.Titel == titel && datumVoorDraaien == DateTime.Parse(reservering.datum) && zaal == reservering.zaal)
						{
							for(int reserveringAantal = 0; reserveringAantal < reservering.AantalPersonen; reserveringAantal++)
							{
								int stoelOpAnderReservering = int.Parse(reservering.stoelen[reserveringAantal].Split(':')[0]);
								int rijOpAnderReservering = int.Parse(reservering.stoelen[reserveringAantal].Split(':')[1]);
								if (stoelOpAnderReservering == stoel+1 && rijOpAnderReservering == rij+1)
								{
									Console.ForegroundColor = ConsoleColor.Red;
								}
							}
						}
					}
					Console.Write("[ ]");
					Console.ResetColor();
				}
				Console.Write("\n");
			}
			Console.Write(plattegronden?[zaalIndex].Scherm + "\n");
		}
		//CHECK DE RESERVERINGEN.JSON EN KIJK OF ARRAY VOORKOMT MET ZELFDE TITEL & DATUM
		public bool checkDubbeleStoelen(string titel, DateTime datum, int zaal, string[] reserveringsStoelen)
		{
			bool result = true;
			foreach(reserveringenJson reservering in reserveringen)
			{
				if (reservering.Titel == titel && datum == DateTime.Parse(reservering.datum) && zaal == reservering.zaal)
				{
					//LOOP DOOR RESERVERINGSSTOELEN
					for(int reserveringsStoelIndex = 0; reserveringsStoelIndex < reserveringsStoelen.Length; reserveringsStoelIndex++)
					{
						int stoelOpNieuweReservering = int.Parse(reserveringsStoelen[reserveringsStoelIndex].Split(':')[0]);
						int rijOpNieuweReservering = int.Parse(reserveringsStoelen[reserveringsStoelIndex].Split(':')[1]);
						//LOOP DOOR DE STOELEN DIE GERESERVEERD ZIJN
						for (int reserveringAantal = 0; reserveringAantal < reservering.AantalPersonen; reserveringAantal++)
						{
							int stoelOpAnderReservering = int.Parse(reservering.stoelen[reserveringAantal].Split(':')[0]);
							int rijOpAnderReservering = int.Parse(reservering.stoelen[reserveringAantal].Split(':')[1]);
							if(stoelOpNieuweReservering == stoelOpAnderReservering && rijOpNieuweReservering == rijOpAnderReservering)
							{
								result = false;
								return result;
							}
						}
					}
				}
			}
			return result;
		}
		//PRINT ZAAL MET EEN GESELECTEERDE KEUZE
		public void printZaalMetKeuze(string titel, DateTime datumVoorDraaien, int zaal, string[] keuzeArr)
		{
			readJson();
			leesJsonReserveringen();
			for (int rij = 0; rij < plattegronden?[zaalIndex].Rijen; rij++)
			{
				//PRINT DE STOELNUMMERS
				if (rij == 0)
				{
					Console.Write("   ");
					for (int i = 0; i < plattegronden?[zaalIndex].Stoelen; i++)
					{
						int adder = i + 1;
						if (adder > 9)
						{
							Console.Write((i + 1) + " ");
						} else
						{
							Console.Write(" " + (i + 1) + " ");
						}
					}
					Console.Write("\n");
				}
				if (rij < 9)
				{
					Console.Write((rij + 1) + "  ");
				} else
				{
					Console.Write(rij + 1 + " ");
				}
				//PRINT DE STOEL
				for (int stoel = 0; stoel < plattegronden?[zaalIndex].Stoelen; stoel++)
				{
					//KIJK OF STOEL IS GERESERVEERD
					foreach (reserveringenJson reservering in reserveringen)
					{
						if (reservering.Titel == titel && datumVoorDraaien == DateTime.Parse(reservering.datum) && zaal == reservering.zaal)
						{
							for (int reserveringAantal = 0; reserveringAantal < reservering.AantalPersonen; reserveringAantal++)
							{
								int stoelOpAnderReservering = int.Parse(reservering.stoelen[reserveringAantal].Split(':')[0]);
								int rijOpAnderReservering = int.Parse(reservering.stoelen[reserveringAantal].Split(':')[1]);
								if (stoelOpAnderReservering == stoel + 1 && rijOpAnderReservering == rij + 1)
								{
									Console.ForegroundColor = ConsoleColor.Red;
								}
							}
						}
					}
					//KIJK OF STOEL IN KEUZE VALT
					for(int i = 0; i < keuzeArr.Length; i++)
					{
						int keuzeStoel = int.Parse(keuzeArr[i].Split(':')[0]);
						int keuzeRij = int.Parse(keuzeArr[i].Split(':')[1]);
						if(stoel + 1 == keuzeStoel && rij + 1 == keuzeRij)
						{
							Console.ForegroundColor = ConsoleColor.Blue;
						}
					}
					Console.Write("[ ]");
					Console.ResetColor();
				}
				Console.Write("\n");
			}
			Console.Write(plattegronden?[zaalIndex].Scherm + "\n");
		}
		//LEES RESERVERINGEN.JSON UIT
		public void leesJsonReserveringen()
		{
			string jsonReserveringen = File.ReadAllText("../../../DataFiles/reserveringen.json", Encoding.GetEncoding("utf-8"));
			reserveringen = JsonConvert.DeserializeObject<List<reserveringenJson>>(jsonReserveringen);
		}
		public class reserveringenJson
		{
			public string Titel { get; set; }
			public int AantalPersonen { get; set; }
			public string datum { get; set; }
			public int zaal { get; set; }
			public List<string> Namen { get; set; }
			public string ReserveringDoor { get; set; }
			public List<int> Leeftijden { get; set; }
			public string[] stoelen { get; set; }
		}
	}
}
