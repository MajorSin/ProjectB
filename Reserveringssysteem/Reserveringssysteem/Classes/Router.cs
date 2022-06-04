using Reserveringssysteem.Classes;
using System;
using System.Collections.Generic;
using System.Globalization;
using static Reserveringssysteem.UserController;

namespace Reserveringssysteem
{
	public class Router
	{
		public string CurrentScreen { get; set; }

		// Toont het scherm waar je nu op bent.
		public void DisplayScreen()
		{
			switch (CurrentScreen)
			{
				case "Authorizatie":
					DisplayAuthorization();
					break;
				case "Inloggen":
					DisplayInloggen();
					break;
				case "Registreren":
					DisplayRegistreren();
					break;
				case "Home":
					DisplayHome();
					break;
				case "Admin":
					DisplayAdmin();
					break;
				case "Reserveren":
					DisplayReserveren();
					break;
				case "Films":
					DisplayFilms();
					break;
				case "Eten & Drinken":
					DisplayEtenDrinken();
					break;
				case "Informatie":
					DisplayInformatie();
					break;
				case "Draaiende films":
					DisplayDraaiendeFilms();
					break;
				default:
					SetCurrentScreen("Authorizatie");
					DisplayAuthorization();
					break;
			}
		}

		// Lay-out van de header teruggeven.
		private void ShowHeader(ConsoleColor color, string title)
		{
			Console.ForegroundColor = color;
			Console.WriteLine(@"{0}", title);
			Console.BackgroundColor = color;
			Console.ResetColor();
		}

		// Keuze van de gebruiker vaststellen.
		public string AwaitResponse(string[] options)
		{
			int currentSelected = 0;
			bool selectionMade = false;

			// Loopt door de opties en houdt bij welke keuze je maakt met pijltjestoetsen.
			while (!selectionMade)
			{
				for (int i = 0; i < options.Length; i++)
				{
					if (i == currentSelected)
					{
						Console.BackgroundColor = ConsoleColor.DarkYellow;
						Console.Write(" ");
						Console.ResetColor();
						Console.ForegroundColor = ConsoleColor.DarkYellow;
					} else
					{
						if ((options.Length - 1) == i)
						{
							Console.ForegroundColor = ConsoleColor.Red;
							Console.Write(" ");
						} else
						{
							Console.ForegroundColor = ConsoleColor.White;
							Console.Write(" ");
						}
					}
					Console.WriteLine("  {0}", options[i]);
					Console.ResetColor();
				}
				switch (Console.ReadKey(true).Key)
				{
					case ConsoleKey.UpArrow:
						if (currentSelected == 0)
						{
							break;
						} else
						{
							currentSelected -= 1;
						}
						break;
					case ConsoleKey.DownArrow:
						if (currentSelected == options.Length - 1)
						{
							break;
						} else
						{
							currentSelected += 1;
						}
						break;
					case ConsoleKey.Enter:
						selectionMade = true;
						break;
				}
				// Zorgt ervoor dat de keuzes niet met elkaar gaan overlappen.
				Console.CursorTop = Console.CursorTop - options.Length;
			}
			return options[currentSelected];
		}
		public int AwaitResponseInIndex(string[] options)
		{
			int currentSelected = 0;
			bool selectionMade = false;

			// Loopt door de opties en houdt bij welke keuze je maakt met pijltjestoetsen.
			while (!selectionMade)
			{
				for (int i = 0; i < options.Length; i++)
				{
					if (i == currentSelected)
					{
						Console.BackgroundColor = ConsoleColor.DarkYellow;
						Console.Write(" ");
						Console.ResetColor();
						Console.ForegroundColor = ConsoleColor.DarkYellow;
					} else
					{
						if ((options.Length - 1) == i)
						{
							Console.ForegroundColor = ConsoleColor.Red;
							Console.Write(" ");
						} else
						{
							Console.ForegroundColor = ConsoleColor.White;
							Console.Write(" ");
						}
					}
					Console.WriteLine("  {0}", options[i]);
					Console.ResetColor();
				}
				switch (Console.ReadKey(true).Key)
				{
					case ConsoleKey.UpArrow:
						if (currentSelected == 0)
						{
							break;
						} else
						{
							currentSelected -= 1;
						}
						break;
					case ConsoleKey.DownArrow:
						if (currentSelected == options.Length - 1)
						{
							break;
						} else
						{
							currentSelected += 1;
						}
						break;
					case ConsoleKey.Enter:
						selectionMade = true;
						break;
				}
				// Zorgt ervoor dat de keuzes niet met elkaar gaan overlappen.
				Console.CursorTop = Console.CursorTop - options.Length;
			}
			return currentSelected;
		}

		// Authorisatie scherm.
		private void DisplayAuthorization()
		{
			ConsoleColor color = ConsoleColor.White;
			string title = @"
               __________
            ///^^^^{}^^^^\\\
          //..@----------@..\\
         ///&%&%&%&/\&%&%&%&\\\
         ||&%&%&_.'  '._&%&%&||
         ||&%'''        '''%&||
         ||&%&  Bioscoop  &%&||
         ||&%&             %&||
         ||&%&            &%&||
   ______||&%&&==========&&%&||______
                ";
			ShowHeader(color, title);

			Console.WriteLine("   Gebruik pijltjestoetsen ↑ en ↓ om te navigeren\n   en druk enter om een optie te kiezen.\n");

			string[] options;

			if (IsLoggedIn)
			{
				options = new string[]
				{
					"Uitloggen",
					"Doorgaan naar home",
					"Beeïndigen",
				};
			} else
			{
				options = new string[]
				{
					"Inloggen",
					"Registreren",
					"Doorgaan als gast",
					"Beeïndigen",
				};
			}

			string choice = AwaitResponse(options);

			switch (choice)
			{
				case "Inloggen":
					SetCurrentScreen("Inloggen");
					break;
				case "Uitloggen":
					if (IsLoggedIn)
					{
						LogOut();
						SetCurrentScreen("Authorizatie");
					}
					break;
				case "Registreren":
					SetCurrentScreen("Registreren");
					break;
				case "Doorgaan naar home":
					if (CurrentUser is Admin)
					{
						SetCurrentScreen("Admin");
					} else
					{
						SetCurrentScreen("Home");
					}
					break;
				case "Doorgaan als gast":
					SetCurrentScreen("Home");
					break;
				case "Beeïndigen":
					Environment.Exit(0);
					break;
				default:
					SetCurrentScreen("Authorizatie");
					break;
			}
		}

		// Inlog scherm.
		private void DisplayInloggen()
		{
			ConsoleColor color = ConsoleColor.Cyan;
			string title = @"
    _____           _                                        
   |_   _|         | |                                       
     | |    _ __   | |   ___     __ _    __ _    ___   _ __  
     | |   | '_ \  | |  / _ \   / _` |  / _` |  / _ \ | '_ \ 
    _| |_  | | | | | | | (_) | | (_| | | (_| | |  __/ | | | |
   |_____| |_| |_| |_|  \___/   \__, |  \__, |  \___| |_| |_|
                               __/ |   __/ |               
                              |___/   |___/
                ";
			ShowHeader(color, title);

			Action showHeader = () => ShowHeader(color, title);
			UserController controller = new();
			controller.Login(showHeader, this);
		}

		// Registratie scherm.
		private void DisplayRegistreren()
		{
			ConsoleColor color = ConsoleColor.Cyan;
			string title = @"
    _____                   _         _                                       
   |  __ \                 (_)       | |                                      
   | |__) |   ___    __ _   _   ___  | |_   _ __    ___   _ __    ___   _ __  
   |  _  /   / _ \  / _` | | | / __| | __| | '__|  / _ \ | '__|  / _ \ | '_ \ 
   | | \ \  |  __/ | (_| | | | \__ \ | |_  | |    |  __/ | |    |  __/ | | | |
   |_|  \_\  \___|  \__, | |_| |___/  \__| |_|     \___| |_|     \___| |_| |_|
                     __/ |                                                    
                    |___/    

                ";
			ShowHeader(color, title);

			Action showHeader = () => ShowHeader(color, title);
			MemberController controller = new();
			controller.Register(showHeader, this);
		}

		// Hoofdscherm.
		private void DisplayHome()
		{
			int currentHour = DateTime.Now.Hour;
			Func<int, string> greeting = hour =>
			{
				switch (hour)
				{
					case < 12:
						return "Goedemorgen,";
					case >= 12 and < 18:
						return "Goedemiddag,";
					default:
						return "Goedenavond,";
				}
			};

			ConsoleColor color = ConsoleColor.White;
			string result = greeting(currentHour);
			string name = "";

			if (IsLoggedIn)
			{
				name = CurrentUser.GetFirstName();
			} else
			{
				name = "gast";
			}

			string title = @$"   
             
   {result} {name}!    
                             ___________I____________
                            ( _____________________ ()
                          _.-'|                    ||
                      _.-'   ||                    || 
     ______       _.-'       ||                    ||
    |      |_ _.-'           ||      Welkom op     ||
    |      |_|_              ||         het        ||
    |______|   `-._          ||     hoofdscherm.   ||
       /\          `-._      ||                    ||
      /  \             `-._  ||                    ||
     /    \                `-||____________________||
    /      \                 ------------------------
   /________\___________________/________________\______
                ";
			ShowHeader(color, title);

			Console.WriteLine("   Gebruik pijltjestoetsen ↑ en ↓ om te navigeren\n   en druk enter om een optie te kiezen.\n");

			string[] options = new string[] {
				"Reserveren",
				"Films",
				"Eten & Drinken",
				"Informatie",
				"Terug"
			};

			string choice = AwaitResponse(options);

			switch (choice)
			{
				case "Reserveren":
					if (IsLoggedIn)
					{
						SetCurrentScreen(choice);
					} else
					{
						SetCurrentScreen("Inloggen");
					}
					break;
				case "Terug":
					SetCurrentScreen("Authorizatie");
					break;
				default:
					SetCurrentScreen(choice);
					break;
			}
		}

		private void DisplayAdmin()
		{
			ConsoleColor color = ConsoleColor.White;
			string title = @"                                                           
                                                                 
                  &&&&&&&&                            
               &&&&&&&&&&&&&&                         
               &&&&&&&&&&&&&&                         
               &&&&&&&&&&&&&&                         
               &&&&&&&&&&&&&&                         
                &&&&&&&&&&&&                          
                 &&&&&&&&&&                           
      &&&&&&&&&&&  &&&&&& &&&&&&&&&&&&                
     &&         &&&&&&&&&&&&         &&               
     &&    @@@@@@@@@@@@@@@@@@@@@@    &&               
     &&                              &&               
     &&     @@  & & &@ @( & @& &     &&               
     &&    &  & &@( & & ( @ & .&     &&               
     &&                              &&               
     &&                              &&               
      &&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&                
   @&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&                              
            ";
			ShowHeader(color, title);

			Console.WriteLine("   Gebruik pijltjestoetsen ↑ en ↓ om te navigeren\n   en druk enter om een optie te kiezen.\n");

			string[] options = new string[] {
				"Draaiende films",
				"Films",
				"Terug"
			};

			string choice = AwaitResponse(options);

			switch (choice)
			{
				case "Terug":
					SetCurrentScreen("Authorizatie");
					break;
				default:
					SetCurrentScreen(choice);
					break;
			}
		}

		// Reserveer scherm.
		private void DisplayReserveren()
		{
			ConsoleColor color = ConsoleColor.Magenta;
			string title = @"
    _____                                                                  
   |  __ \                                                                 
   | |__) |   ___   ___    ___   _ __  __   __   ___   _ __    ___   _ __  
   |  _  /   / _ \ / __|  / _ \ | '__| \ \ / /  / _ \ | '__|  / _ \ | '_ \ 
   | | \ \  |  __/ \__ \ |  __/ | |     \ V /  |  __/ | |    |  __/ | | | |
   |_|  \_\  \___| |___/  \___| |_|      \_/    \___| |_|     \___| |_| |_|
                                                                                       
                ";
			ShowHeader(color, title);
			string[] options;
			//LAAT ALLE FILMS ZIEN DIE BINNEKORT DRAAIEN
			draaiendeFilms draaienClass = new draaiendeFilms();
			var filmsDieBinnekortDraaien = draaienClass.laatDraaiendeFilmsZien();
			if (filmsDieBinnekortDraaien.Count <= 0)
			{
				Console.WriteLine("   Er draaien binnenkort geen films\n\n");
				options = new string[]
				{
					"Terug",
				};
			} else
			{
				Console.WriteLine("   De volgende films kunt u reserveren:\n");
				options = new string[filmsDieBinnekortDraaien.Count + 1];
				for (int i = 0; i < filmsDieBinnekortDraaien.Count; i++)
				{
					options[i] = filmsDieBinnekortDraaien[i].Name;
				}
				options[options.Length - 1] = "Terug";
			}
			//MAAK EEN OPTIE VOOR FILM
			string choice = AwaitResponse(options);
			string titel = choice;
			if (choice == "Terug")
			{
				SetCurrentScreen("Home");
			} else
			{
				bool laatDatumEnZaalZien = true;
				while (laatDatumEnZaalZien)
				{
					Console.Clear();
					ShowHeader(color, title);
					Console.WriteLine(draaienClass.filmDatumDetails(choice));
					//LAAT DE DATUMS KIEZEN
					int keuzeVoorDatumEnZaal = AwaitResponseInIndex(draaienClass.datumsDraaienString);
					if (keuzeVoorDatumEnZaal == (draaienClass.datumsDraaienString.Length - 1))
					{
						laatDatumEnZaalZien = false;
					} else
					{
						//CHECK OF ER EEN RESERVERING IS DIE OVERLOOPT
						bool dubbel = draaienClass.checkDubbeleReservering(titel, CurrentUser.GetUsername(), draaienClass.datumsDraaienArray[keuzeVoorDatumEnZaal]);
						int aantalPersonen = 0;
						string[] gekozenStoelen = new string[aantalPersonen];
						for (int i = 0; i < gekozenStoelen.Length; i++)
						{
							gekozenStoelen[i] = "";
						}
						bool persoonlijkeGegevensIngevuld = false;
						//LAAT DE DETAILS VAN DE FILMS ZIEN ZOALS TIJD EN ZAAL
						Console.Clear();
						ShowHeader(color, title);
						if (dubbel)
						{
							Console.WriteLine("   U heeft al een reservering dat overlapt met deze datum!\n");
							string[] GaTerug = { "Ga terug" };
							AwaitResponse(GaTerug);
							laatDatumEnZaalZien = true;
						} else
						{
							CultureInfo netherlands = new CultureInfo("nl-NL");
							string dayOfWeek = netherlands.DateTimeFormat.GetDayName(draaienClass.datumsDraaienArray[keuzeVoorDatumEnZaal].DayOfWeek);
							dayOfWeek = char.ToUpper(dayOfWeek[0]) + dayOfWeek.Substring(1);
							int zaal = draaienClass.zaalWaarinFilmDraait[keuzeVoorDatumEnZaal];
							Console.WriteLine($"U heeft gekozen voor {titel} op {dayOfWeek}, {draaienClass.datumsDraaienArray[keuzeVoorDatumEnZaal].Day} {draaienClass.datumsDraaienArray[keuzeVoorDatumEnZaal].ToString("MMM")} {draaienClass.datumsDraaienArray[keuzeVoorDatumEnZaal].Year}, {draaienClass.datumsDraaienArray[keuzeVoorDatumEnZaal].Hour}:{draaienClass.datumsDraaienArray[keuzeVoorDatumEnZaal].ToString("mm")}. In zaal: {zaal}\nHet zaal ziet er als volgt uit:\n\n");
							Zaal zaalvoorkeuzeFilm = new Zaal(zaal);
							// LAAT DE PLATTEGROND ZIEN
							//Console.WriteLine(zaalvoorkeuzeFilm.zaal());
							zaalvoorkeuzeFilm.printZaal(titel, draaienClass.datumsDraaienArray[keuzeVoorDatumEnZaal], zaal);
							//KIJK OF DE AANTAL PERSONEN VALID IS
							string aantalPersonenString = "";
							aantalPersonen = 0;
							bool aantalPersonenGekozen = false;
							Console.WriteLine("Voor hoeveel personen wilt u een reservering maken? Klik op q om keuze te beïndigen en terug te gaan naar de datums");
							Console.WriteLine("Reserveert u voor meer dan 7 personen? Bel dan naar ons voor een reservering");
							while (!aantalPersonenGekozen)
							{
								aantalPersonenString = Console.ReadLine();
								if (Int32.TryParse(aantalPersonenString, out aantalPersonen))
								{
									if (aantalPersonen <= 7 && aantalPersonen > 0)
									{
										aantalPersonenGekozen = true;
									} else
									{
										Console.WriteLine("\nVul een geldig nummer in. Voor meer dan 7 personen kunt u naar ons bellen om een reservering te maken.");
									}
								} else if (aantalPersonenString == "q" || aantalPersonenString == "Q")
								{
									break;
								} else
								{
									Console.WriteLine("\nVul een geldig nummer in.");
								}
							}
							//KIES STOEL
							if (aantalPersonenGekozen)
							{
								string[] namen = new string[aantalPersonen];
								for (int i = 0; i < namen.Length; i++)
								{
									namen[i] = "";
								}
								int[] leeftijdArr = new int[aantalPersonen];
								Console.WriteLine($"\nU heeft gekozen voor {aantalPersonen}. U krijgt nu de keuze om stoelen te kiezen. Indien u reserveert voor meer dan 1 persoon, dient u de meest linker stoel te kiezen. De andere personen worden naast elkaar ingedeeld. Als eerst wordt gevraagd uit welk rij u de stoel wil boeken, daarna wordt gevraagd voor de stoel. Een rij loopt van boven naar beneden, een stoel is van links naar rechts.");
								Console.WriteLine("Als u de keuze wil beïndigen, kunt u `q` invoeren.\n");
								Console.Write("   Kies een rij: ");
								var gekozenRij = Console.ReadLine();
								int gekozenRijInt = 0;
								bool rijgekozen = false;
								bool stoelgekozen = false;
								string gekozenStoel = "";
								while (!rijgekozen)
								{
									if (Int32.TryParse(gekozenRij, out gekozenRijInt) && 0 < gekozenRijInt && gekozenRijInt <= zaalvoorkeuzeFilm.rijen)
									{
										rijgekozen = true;
										//KIJK OF RIJ VALID IS
										Console.Write("   Kies een stoel: ");
										gekozenStoel = Console.ReadLine();
									} else if (gekozenRij == "q")
									{
										aantalPersonenGekozen = false;
										break;
									} else
									{
										Console.Write("   Vul een geldig rij in: ");
										gekozenRij = Console.ReadLine();
									}
								}
								//KIJK OF STOEL VALID IS
								int gekozenStoelInt = 0;
								if (rijgekozen)
								{
									while (!stoelgekozen)
									{
										if (Int32.TryParse(gekozenStoel, out gekozenStoelInt) && 0 < gekozenStoelInt && gekozenStoelInt <= zaalvoorkeuzeFilm.stoelen)
										{
											stoelgekozen = true;
										} else if (gekozenStoel == "q")
										{
											aantalPersonenGekozen = false;
											break;
										} else
										{
											Console.Write("   Vul een geldig stoel in: ");
											gekozenStoel = Console.ReadLine();
										}
									}
								}
								Console.WriteLine("");
								//MAAK ARRAY VAN STOELEN
								bool stoelenGoedGekozen = false;
								if (rijgekozen && stoelgekozen)
								{
									bool checkVoorStoelen = false;
									while (!checkVoorStoelen)
									{
										gekozenStoelen = new string[aantalPersonen];
										for (int i = 0; i < aantalPersonen; i++)
										{
											gekozenStoelen[i] = $"{gekozenStoelInt + i}:{gekozenRijInt}";
										}
										//KIJK OF DE OVERIGE STOELEN OOK IN DE ZAAL ZITTEN
										for (int i = 0; i < aantalPersonen; i++)
										{
											int stoelOpReservering = int.Parse(gekozenStoelen[i].Split(':')[0]);
											if (stoelOpReservering > zaalvoorkeuzeFilm.stoelen)
											{
												Console.WriteLine("   Een of meerdere stoelen bevinden zich buiten de zaal. Zorg ervoor dat iedereen een stoel binnen de zaal heeft.\n");
												string[] terug = { "Ga terug" };
												AwaitResponse(terug);
												checkVoorStoelen = true;
												break;
											}
										}
										//KIJK OF STOELEN AL GERESERVEERD ZIJN
										if (!checkVoorStoelen)
										{
											if (zaalvoorkeuzeFilm.checkDubbeleStoelen(titel, draaienClass.datumsDraaienArray[keuzeVoorDatumEnZaal], zaal, gekozenStoelen))
											{
												stoelenGoedGekozen = true;
												checkVoorStoelen = true;
											} else
											{
												Console.WriteLine("   Een of meerdere stoelen zijn al bezet. Zorg ervoor dat iedereen een stoel naast elkaar heeft.\n");
												checkVoorStoelen = true;
												string[] terug = { "Ga terug" };
												AwaitResponse(terug);
												break;
											}
										}
									}
								}
								//VRAAG VOOR PERSOONLIJKE GEGEVENS
								if (stoelenGoedGekozen)
								{
									string voornaam = "";
									string[] voornaamArr = new string[aantalPersonen];
									string achternaam = "";
									string[] achternaamArr = new string[aantalPersonen];
									namen = new string[aantalPersonen];
									string leeftijdInput = "";
									int leeftijd;
									leeftijdArr = new int[aantalPersonen];
									Console.Clear();
									ShowHeader(color, title);
									Console.WriteLine("   De stoelen zijn gekozen, op dit scherm dient u de persoonlijke gegevens in te vullen.\n");
									for (int persoon = 0; persoon < aantalPersonen; persoon++)
									{
										Console.WriteLine($"   Persoon {persoon + 1}");
										Console.Write($"   - Voer de voornaam van persoon {persoon + 1} in: ");
										voornaam = Console.ReadLine();
										while (String.IsNullOrEmpty(voornaam))
										{
											Console.WriteLine("     Input kan niet leeg zijn!");
											Console.Write("     ");
											voornaam = Console.ReadLine();
										}
										Console.Write($"\n   - Voer de achternaam van persoon {persoon + 1} in: ");
										achternaam = Console.ReadLine();
										while (String.IsNullOrEmpty(achternaam))
										{
											Console.WriteLine("     Input kan niet leeg zijn!");
											Console.Write("     ");
											achternaam = Console.ReadLine();
										}
										Console.Write($"\n   - Voer de leeftijd van persoon {persoon + 1} in: ");
										leeftijdInput = Console.ReadLine();
										while (!Int32.TryParse(leeftijdInput, out leeftijd))
										{
											Console.WriteLine("     Vul een geldig nummer in!");
											Console.Write("     ");
											leeftijdInput = Console.ReadLine();
											if (Int32.TryParse(leeftijdInput, out leeftijd))
											{
												break;
											}
										}
										voornaamArr[persoon] = voornaam;
										achternaamArr[persoon] = achternaam;
										namen[persoon] = voornaam + " " + achternaam;
										leeftijdArr[persoon] = leeftijd;
										Console.Write("\n");
									}
									persoonlijkeGegevensIngevuld = true;
								}//LAAT DETAILS ZIEN EN GA DOOR NAAR BETALEN
								if (persoonlijkeGegevensIngevuld)
								{
									string[] opties = { "Betalen", "Annuleer bestelling en ga terug" };
									bool notPaid = false;

									while (!notPaid)
									{
										Console.Clear();
										ShowHeader(color, title);

										//LAAT PLATTEGROND ZIEN MET KEUZE STOELEN
										Console.WriteLine("   Uw persoonlijke gegevens zijn ingevuld. Uw stoelkeuze(s) zijn hieronder te zien.\n");
										zaalvoorkeuzeFilm.printZaalMetKeuze(titel, draaienClass.datumsDraaienArray[keuzeVoorDatumEnZaal], zaal, gekozenStoelen);
										Console.WriteLine("\nEr wordt een prijsbepaling toegepast, waardoor 'optimale' stoelen duurder kunnen zijn dan niet-optimale stoelen. Hierdoor kan het zijn dat de ene ticker duurder is dan de ander.");
										//BEPAAL PRIJZEN
										double[] prijzenArr = new double[aantalPersonen];
										for (int i = 0; i < aantalPersonen; i++)
										{
											prijzenArr[i] = zaalvoorkeuzeFilm.krijgPrijs(gekozenStoelen[i], zaal);
											Console.WriteLine($"- {namen[i]}: \u20AC{prijzenArr[i]}");
										}
										Console.Write("\n");

										// --------

										var keuze = AwaitResponse(opties);
										if (keuze == "Betalen")
										{
											//BETAALSCHERM
											Console.WriteLine("BETALEN");

											Console.Clear();
											ShowHeader(color, title);
											string result = Betalen.betaalOpties();
											if (result != "Back")
											{
												//BETALING GELUKT
												Console.Clear();
												ShowHeader(color, title);
												var username = CurrentUser.GetUsername();
												Reservering reserveringDoorGebruiker = new Reservering(titel, aantalPersonen, draaienClass.datumsDraaienArray[keuzeVoorDatumEnZaal], zaal, namen, username, leeftijdArr, gekozenStoelen);
												reserveringDoorGebruiker.addToJsonFile();
												Console.WriteLine("De reservering is gelukt, er is een bevestiging naar uw e-mail gestuurd waar u de ticket kunt vinden.\n");
												string[] gaterug = { "Ga terug" };
												AwaitResponse(gaterug);
												laatDatumEnZaalZien = false;
												notPaid = true;
											}
										} else
										{
											notPaid = true;
										}
									}
									SetCurrentScreen("Home");
								}
							}
						}
					}
				}
			}
		}

		// Films scherm.
		private void DisplayFilms()
		{
			ConsoleColor color = ConsoleColor.Blue;
			string title = @"
    ______   _   _                   
   |  ____| (_) | |                  
   | |__     _  | |  _ __ ___    ___ 
   |  __|   | | | | | '_ ` _ \  / __|
   | |      | | | | | | | | | | \__ \
   |_|      |_| |_| |_| |_| |_| |___/   

                ";
			ShowHeader(color, title);

			Action showHeader = () => ShowHeader(color, title);
			FilmController filmController = new();
			AdminController adminController = new();
			List<Film> films = filmController.GetFilms();

			filmController.ShowFilms(films);

			string[] options = new string[0];

			if (CurrentUser is Admin)
			{
				options = new string[]
				{
					"Bekijk film details",
					"Filter films",
					"Film aanpassen",
					"Film verwijderen",
					"Terug"
				};
			}
			else
			{
				options = new string[]
				{
					"Bekijk film details",
					"Filter films",
					"Terug"
				};
			}

			string choice = AwaitResponse(options);

			if (choice == "Bekijk film details")
			{
				Film film = filmController.ShowFilm(films, showHeader);

				if (film != null)
				{
					Console.Clear();
					ShowHeader(color, title);

					Console.WriteLine("   Hieronder staan de details van de film:\n");
					Console.WriteLine(film);

					options = new string[]
					{
						"Terug",
					};

					choice = AwaitResponse(options);
				}
			}
			else if (choice == "Filter films")
			{
				string result = filmController.ShowList(showHeader, this);
				films = filmController.FilteredFilms;

				Console.Clear();
				ShowHeader(color, title);

				if (result != "Back")
				{
					if (films.Count > 0)
					{
						Console.ForegroundColor = ConsoleColor.White;
						Console.WriteLine("   Uw zoekopdracht heeft de volgende resultaat(en) opgeleverd:\n");
						Console.ResetColor();
						filmController.ShowFilms(films);

						if (CurrentUser is Admin)
						{
							options = new string[]
							{
								"Bekijk film details",
								"Film aanpassen",
								"Film verwijderen",
								"Terug",
							};
						}
						else
						{
							options = new string[]
							{
								"Bekijk film details",
								"Terug"
							};
						}


						choice = AwaitResponse(options);

						if (choice == "Bekijk film details")
						{
							Film film = filmController.ShowFilm(films, showHeader);

							if (film != null)
							{
								Console.Clear();
								ShowHeader(color, title);

								Console.WriteLine("   Hieronder staan de details van de film:\n");
								Console.WriteLine(film);

								options = new string[]
								{
									"Terug",
								};

								choice = AwaitResponse(options);
							}
						}
						else if (choice == "Film aanpassen")
						{
							string editFilm = adminController.EditFilm(this, showHeader, filmController, films);

							if (editFilm != "Back")
							{
								Console.Clear();
								ShowHeader(color, title);

								Console.WriteLine("   Film is succesvol aangepast!\n");

								options = new string[]
								{
									"Terug",
								};

								choice = AwaitResponse(options);
							}
						}
						else if (choice == "Film verwijderen")
						{
							string removeFilm = adminController.RemoveFilm(this, showHeader, filmController, films);

							if (removeFilm != "Back")
							{
								Console.Clear();
								ShowHeader(color, title);

								Console.WriteLine("   Film is succesvol verwijderd!\n");

								options = new string[]
								{
									"Terug",
								};

								choice = AwaitResponse(options);
							}
						}
					}
					else
					{
						Console.Clear();
						ShowHeader(color, title);

						Console.WriteLine("   Er zijn geen films gevonden\n");
						options = new string[]
						{
							"Terug",
						};
						choice = AwaitResponse(options);
					}
				}
			}
			else if (choice == "Film aanpassen")
			{
				string editFilm = adminController.EditFilm(this, showHeader, filmController, films);

				if (editFilm != "Back")
				{
					Console.Clear();
					ShowHeader(color, title);

					Console.WriteLine("   Film is succesvol aangepast!\n");

					options = new string[]
					{
						"Terug",
					};

					choice = AwaitResponse(options);
				}
			}
			else if (choice == "Film verwijderen")
			{
				string removeFilm = adminController.RemoveFilm(this, showHeader, filmController, films);

				if (removeFilm != "Back")
				{
					Console.Clear();
					ShowHeader(color, title);

					Console.WriteLine("   Film is succesvol verwijderd!\n");

					options = new string[]
					{
						"Terug",
					};

					choice = AwaitResponse(options);
				}
			}
			else
			{
				if (CurrentUser is Admin)
				{
					SetCurrentScreen("Admin");
				}
				else
				{
					SetCurrentScreen("Home");
				}
			}
		}

		// Eten & Drinken scherm.
		private void DisplayEtenDrinken()
		{
			ConsoleColor color = ConsoleColor.Red;
			string title = @"
    ______   _                                 _____           _           _                   
   |  ____| | |                      ___      |  __ \         (_)         | |                  
   | |__    | |_    ___   _ __      ( _ )     | |  | |  _ __   _   _ __   | | __   ___   _ __  
   |  __|   | __|  / _ \ | '_ \     / _ \/\   | |  | | | '__| | | | '_ \  | |/ /  / _ \ | '_ \ 
   | |____  | |_  |  __/ | | | |   | (_>  <   | |__| | | |    | | | | | | |   <  |  __/ | | | |
   |______|  \__|  \___| |_| |_|    \___/\/   |_____/  |_|    |_| |_| |_| |_|\_\  \___| |_| |_|
                                                                                                      
                ";
			ShowHeader(color, title);

			//OVERZICHT ETEN EN DRINKEN
			Console.WriteLine("   Dit is een overzicht van het menu. U kunt op een item klikken om de voedingswaarde te zien.\n");
			string[] options = new string[] { "Zout popcorn", "Zoet popcorn", "Nachos", "Paprika chips", "Naturel chips", "Cola", "Sprite", "Spa blauw", "Spa rood", "Terug" };
			string choice = AwaitResponse(options);

			if (choice == "Terug")
			{
				if (CurrentUser is Admin)
				{
					SetCurrentScreen("Admin");
				} else
				{
					SetCurrentScreen("Home");
				}
			} else
			{
				Voeding product = new(choice);
				Console.Clear();
				ShowHeader(color, title);

				Console.WriteLine($"   {choice.ToUpper()}\n");
				Console.WriteLine(product.returnVoedingswaarde());
				Console.WriteLine("\n");

				options = new string[]
				{
					"Terug",
				};

				AwaitResponse(options);
			}

			Console.Clear();
			DisplayScreen();
		}

		// Informatie scherm.
		private void DisplayInformatie()
		{
			ConsoleColor color = ConsoleColor.Green;
			string title = @"
    _____            __                                      _     _        
   |_   _|          / _|                                    | |   (_)       
     | |    _ __   | |_    ___    _ __   _ __ ___     __ _  | |_   _    ___ 
     | |   | '_ \  |  _|  / _ \  | '__| | '_ ` _ \   / _` | | __| | |  / _ \
    _| |_  | | | | | |   | (_) | | |    | | | | | | | (_| | | |_  | | |  __/
   |_____| |_| |_| |_|    \___/  |_|    |_| |_| |_|  \__,_|  \__| |_|  \___|
                                                                                 
                ";
			ShowHeader(color, title);

			Console.WriteLine(@"   Hier vindt u de algemene informatie over de bioscoop.

   Adres:                           Wijnhaven 107, Rotterdam
   Postcode:                        3011 WN
   E-mail:                          Info@Bioscoop.nl
   Telefoon nummer klantenservice:  (010) 123 45 67
   Aantal zalen:		    3
    
   Openingstijden:
        Maandag:      09:00 - 22:00
        Dinsdag:      09:00 - 22:00
        Woensdag:     09:00 - 22:00
        Donderdag:    09:00 - 22:00
        Vrijdag:      09:00 - 00:00
        Zaterdag:     09:00 - 00:00
        Zondag:       09:00 - 22:00
            ");

			string[] options = new string[]
			{
					"Terug",
			};

			string choice = AwaitResponse(options);

			if (CurrentUser is Admin)
			{
				SetCurrentScreen("Admin");
			} else
			{
				SetCurrentScreen("Home");
			}
		}

		// Huidige films laten zien.
		private void DisplayDraaiendeFilms()
		{
			ConsoleColor color = ConsoleColor.DarkBlue;
			string title = @"
                   _               _         
       /\         | |             (_)        
      /  \      __| |  _ __ ___    _   _ __  
     / /\ \    / _` | | '_ ` _ \  | | | '_ \ 
    / ____ \  | (_| | | | | | | | | | | | | |
   /_/    \_\  \__,_| |_| |_| |_| |_| |_| |_|
                                           
            ";

			ShowHeader(color, title);
			string[] options;

			options = new string[] {
				"Draaiende films bekijken",
				"Film draaien",
				"Terug"
			};

			draaiendeFilms draaienClass = new draaiendeFilms();
			var filmsDieBinnekortDraaien = draaienClass.laatDraaiendeFilmsZien();

			string choice = AwaitResponse(options);

			Console.Clear();
			ShowHeader(color, title);

			if (choice == "Draaiende films bekijken")
			{
				if (filmsDieBinnekortDraaien.Count <= 0)
				{
					Console.WriteLine("   Er draaien binnenkort geen films\n\n");
					options = new string[]
					{
						"Terug",
					};
				}
				else
				{
					Console.WriteLine("   De volgende films draaien vandaag/binnenkort:\n");
					options = new string[filmsDieBinnekortDraaien.Count + 1];

					for (int i = 0; i < filmsDieBinnekortDraaien.Count; i++)
					{
						options[i] = filmsDieBinnekortDraaien[i].Name;
					}

					options[options.Length - 1] = "Terug";

					choice = AwaitResponse(options);

					if (choice == "Terug")
					{
						SetCurrentScreen("Draaiende films");
					}
					else
					{
						Console.Clear();
						ShowHeader(color, title);
						Console.WriteLine(draaienClass.filmDatumDetails(choice));

						for (int i = 0; i < draaienClass.datumsDraaienString.Length - 1; i++)
						{
							Console.WriteLine("   " + draaienClass.datumsDraaienString[i]);
						}

						Console.WriteLine();

						options = new string[]
						{
							"Terug",
						};

						//LAAT DE DATUMS ZIEN
						choice = AwaitResponse(options);
					}
				}
			}
			else if (choice == "Film draaien")
			{
				Action showHeader = () => ShowHeader(color, title);
				FilmController filmController = new();
				AdminController adminController = new();
				List<Film> films = filmController.GetFilms();

				filmController.ShowFilms(films);

				options = new string[]
				{
					"Draai een film",
					"Filter films",
					"Terug"
				};

				choice = AwaitResponse(options);

				Console.Clear();
				ShowHeader(color, title);

				if (choice == "Draai een film")
				{
					filmController.ShowFilms(films);
					string result = adminController.DraaiFilm(this, draaienClass, showHeader, filmController, films);

					if (result != "Back")
					{
						Console.Clear();
						ShowHeader(color, title);

						Console.WriteLine("   Film is toegevoegd aan de films die worden gedraaid!\n");

						options = new string[]
						{
							"Terug"
						};

						choice = AwaitResponse(options);
					}

					SetCurrentScreen("Draaiende films");
				}
				else if (choice == "Filter films")
				{
					string result = filmController.ShowList(showHeader, this);
					films = filmController.FilteredFilms;

					if (result != "Back")
					{
						if (films.Count > 0)
						{
							Console.Clear();
							ShowHeader(color, title);

							Console.ForegroundColor = ConsoleColor.White;
							Console.WriteLine("   Uw zoekopdracht heeft de volgende resultaat(en) opgeleverd:\n");
							Console.ResetColor();
							filmController.ShowFilms(films);

							options = new string[]
							{
								"Draai een film",
								"Terug",
							};

							choice = AwaitResponse(options);

							Console.Clear();
							ShowHeader(color, title);

							if (choice == "Draai een film")
							{
								result = adminController.DraaiFilm(this, draaienClass, showHeader, filmController, films);

								if (result != "Back")
								{
									Console.Clear();
									ShowHeader(color, title);

									Console.WriteLine("   Film is toegevoegd aan de films die worden gedraaid!\n");

									options = new string[]
									{
									"Terug"
									};

									choice = AwaitResponse(options);
								}

								SetCurrentScreen("Draaiende films");
							}
						}
						else
						{
							Console.Clear();
							ShowHeader(color, title);

							Console.WriteLine("   Er zijn geen films gevonden\n");
							options = new string[]
							{
								"Terug",
							};
							choice = AwaitResponse(options);

							SetCurrentScreen("Draaiende films");
						}
					}
				}
				else
				{
					SetCurrentScreen("Draaiende films");
				}
			}
			else
			{
				SetCurrentScreen("Admin");
			}
		}

		public void SetCurrentScreen(string screen)
		{
			CurrentScreen = screen;
		}
	}
}
