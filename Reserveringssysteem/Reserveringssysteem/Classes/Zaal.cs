using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
			string json = File.ReadAllText("DataFiles/plattegrond.json", Encoding.GetEncoding("utf-8"));
			this.plattegronden = JsonConvert.DeserializeObject<List<plattegrondJson>>(json);
		}
		public string zaal()
		{
			//GEEFT DE ZAAL TERUG
			string plattegrond = "   ";
			readJson();
			for (int rij = 0; rij < plattegronden?[zaalIndex].Rijen; rij++)
			{
				if (rij == 0)
				{
					for (int i = 0; i < plattegronden?[zaalIndex].Stoelen; i++)
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
				if (rij < 9)
				{
					plattegrond += (rij + 1) + "  ";
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
					Console.Write("  → STOEL\n");
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
					string stoelDesign = "[ ]";
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
									stoelDesign = "[X]";
									Console.ForegroundColor = ConsoleColor.Red;
								}
							}
						}
					}
					Console.Write(stoelDesign);
					Console.ResetColor();
				}
				Console.Write("\n");
			}
			Console.WriteLine(" ↑\nRIJ");
			Console.WriteLine(plattegronden?[zaalIndex].Scherm + "\n");
		}
		//CHECK DE RESERVERINGEN.JSON EN KIJK OF ARRAY VOORKOMT MET ZELFDE TITEL & DATUM
		public bool checkDubbeleStoelen(string titel, DateTime datum, int zaal, string[] reserveringsStoelen)
		{
			bool result = true;
			foreach (reserveringenJson reservering in reserveringen)
			{
				if (reservering.Titel == titel && datum == DateTime.Parse(reservering.datum) && zaal == reservering.zaal)
				{
					//LOOP DOOR RESERVERINGSSTOELEN
					for (int reserveringsStoelIndex = 0; reserveringsStoelIndex < reserveringsStoelen.Length; reserveringsStoelIndex++)
					{
						int stoelOpNieuweReservering = int.Parse(reserveringsStoelen[reserveringsStoelIndex].Split(':')[0]);
						int rijOpNieuweReservering = int.Parse(reserveringsStoelen[reserveringsStoelIndex].Split(':')[1]);
						//LOOP DOOR DE STOELEN DIE GERESERVEERD ZIJN
						for (int reserveringAantal = 0; reserveringAantal < reservering.AantalPersonen; reserveringAantal++)
						{
							int stoelOpAnderReservering = int.Parse(reservering.stoelen[reserveringAantal].Split(':')[0]);
							int rijOpAnderReservering = int.Parse(reservering.stoelen[reserveringAantal].Split(':')[1]);
							if (stoelOpNieuweReservering == stoelOpAnderReservering && rijOpNieuweReservering == rijOpAnderReservering)
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
					string stoelDesign = "[ ]";
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
									stoelDesign = "[X]";
								}
							}
						}
					}
					//KIJK OF STOEL IN KEUZE VALT
					for (int i = 0; i < keuzeArr.Length; i++)
					{
						int keuzeStoel = int.Parse(keuzeArr[i].Split(':')[0]);
						int keuzeRij = int.Parse(keuzeArr[i].Split(':')[1]);
						if (stoel + 1 == keuzeStoel && rij + 1 == keuzeRij)
						{
							Console.ForegroundColor = ConsoleColor.Blue;
							stoelDesign = "[*]";
						}
					}
					Console.Write(stoelDesign);
					Console.ResetColor();
				}
				Console.Write("\n");
			}
			Console.Write(plattegronden?[zaalIndex].Scherm + "\n");
		}
		//LEES RESERVERINGEN.JSON UIT
		public void leesJsonReserveringen()
		{
			string jsonReserveringen = File.ReadAllText("DataFiles/reserveringen.json", Encoding.GetEncoding("utf-8"));
			reserveringen = JsonConvert.DeserializeObject<List<reserveringenJson>>(jsonReserveringen);
		}

		// Update reserveringen.
		public void UpdateReservations()
		{
			var reservations = reserveringen;
			var stringifiedReservations = JsonConvert.SerializeObject(reservations, Formatting.Indented);
			File.WriteAllText("DataFiles/reserveringen.json", stringifiedReservations);
		}

		public double krijgPrijs(string stoel, int zaal)
		{
			double Prijs = 7.50;
			int stoelNummer = int.Parse(stoel.Split(':')[0]);
			int rijNummer = int.Parse(stoel.Split(':')[1]);
			//BEREKEN DE PRIJS PER ZAAL
			if (zaal == 1)
			{
				if (rijNummer < 4 || rijNummer > 11)
				{
					return Prijs;
				} else if (stoelNummer > 3 && stoelNummer < 10)
				{
					Prijs = 10;
					return Prijs;
				}
			} else if (zaal == 2)
			{
				if (rijNummer < 4 || rijNummer > 16 || stoelNummer < 4 || stoelNummer > 15)
				{
					return Prijs;
				} else if (rijNummer < 7 || rijNummer > 13 || stoelNummer < 7 || stoelNummer > 12)
				{
					Prijs = 10;
					return Prijs;
				} else
				{
					Prijs = 12.50;
					return Prijs;
				}
			} else if (zaal == 3)
			{
				if (rijNummer < 5 || rijNummer > 16 || stoelNummer < 5 || stoelNummer > 26)
				{
					return Prijs;
				} else if (rijNummer < 8 || rijNummer > 13 || stoelNummer < 9 || stoelNummer > 22)
				{
					Prijs = 10;
					return Prijs;
				} else
				{
					Prijs = 12.50;
					return Prijs;
				}
			}
			return Prijs;
		}
		public class reserveringenJson
		{
			public int Id { get; set; }
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
