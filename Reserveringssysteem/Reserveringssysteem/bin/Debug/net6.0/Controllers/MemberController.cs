using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Reserveringssysteem
{
	public class MemberController : UserController
	{
		public static List<Member> Members { get; set; }

		// Account aanmaken proces.
		public void Register(Action showHeader, Router router)
		{
			Console.Clear();
			showHeader();

			int count = 0;
			(int, int) currentPosition = Console.GetCursorPosition();
			UpdateBar(0, currentPosition, count);

			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine("\n   Typ 'q' als u terug wilt gaan.");
			Console.ResetColor();

			string questionOne = "\n   [1] Wat is uw naam?";
			Console.WriteLine(questionOne);
			string firstname = CheckErrors("voornaam", questionOne).ToLower();
			firstname = char.ToUpper(firstname[0]) + firstname.Substring(1);

			if (firstname.ToLower() != "q")
			{
				count++;
				currentPosition = Console.GetCursorPosition();
				UpdateBar(20, currentPosition, count);

				string questionTwo = "   [2] Wat is uw achternaam?";
				Console.WriteLine(questionTwo);
				string surname = CheckErrors("achternaam", questionTwo).ToLower();
				surname = char.ToUpper(surname[0]) + surname.Substring(1);

				if (surname.ToLower() != "q")
				{
					count++;
					currentPosition = Console.GetCursorPosition();
					UpdateBar(40, currentPosition, count);

					string questionThree = "   [3] Wat is uw geslacht? Typ uw keuze in: 1) Man 2) Vrouw";
					Console.WriteLine(questionThree);
					string gender = CheckErrors("geslacht", questionThree);

					if (gender.ToLower() != "q")
					{
						count++;
						currentPosition = Console.GetCursorPosition();
						UpdateBar(60, currentPosition, count);

						string questionFour = "   [4] Wat is uw geboortedatum? Bv: 23-4-1998 (dag-maand-jaar)";
						Console.WriteLine(questionFour);
						string birthDate = CheckErrors("birthDate", questionFour);

						if (birthDate != "q")
						{
							count++;

							Console.Clear();
							showHeader();

							currentPosition = Console.GetCursorPosition();
							UpdateBar(0, currentPosition, count);
							UpdateBar(80, currentPosition, count);

							Console.ForegroundColor = ConsoleColor.DarkYellow;
							Console.WriteLine("\n   Typ 'q' als u terug wilt gaan.");
							Console.ResetColor();

							string questionFive = "\n   [5] Wat is uw e-mailaddres?";
							Console.WriteLine(questionFive);
							string emailAddress = CheckErrors("emailAddress", questionFive);

							if (emailAddress != "q")
							{
								string questionSix = "\n   [6] Welke gebruikersnaam wilt u?";
								Console.WriteLine(questionSix);
								string username = CheckErrors("gebruikersnaam", questionSix);

								if (username != "q")
								{
									string questionSeven = "\n   [7] Wat wordt uw wachtwoord?";
									Console.WriteLine(questionSeven);
									string password = CheckErrors("wachtwoord", questionSeven);

									if (password != "q")
									{
										count++;
										currentPosition = Console.GetCursorPosition();
										UpdateBar(100, currentPosition, count);

										string[] options = new string[]
										{
											"Bevestigen",
										};

										var members = GetMembers();
										Member member = new Member(members.Count + 1, username, password, firstname, surname, gender, birthDate, emailAddress);
										members.Add(member);

										UpdateMembers();

										Console.Clear();
										showHeader();

										Console.WriteLine("  Account is gemaakt!\n");

										options = new string[]
										{
											"Terug",
										};

										string choice = router.AwaitResponse(options);
									}
								}
							}
						}
					}
				}
			}
			router.SetCurrentScreen("Authorizatie");
		}

		public static List<Member> GetMembers()
		{
			return Members;
		}

		public static void SetMembers()
		{
			var json = File.ReadAllText("DataFiles/members.json", Encoding.GetEncoding("utf-8"));
			Members = JsonConvert.DeserializeObject<List<Member>>(json);
		}

		public void UpdateMembers()
		{
			var members = GetMembers();
			var stringifiedMembers = JsonConvert.SerializeObject(members, Formatting.Indented);
			File.WriteAllText("DataFiles/members.json", stringifiedMembers);
		}
	}
}
