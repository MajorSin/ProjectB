using System;
using System.Linq;
using System.Text.RegularExpressions;
using static Reserveringssysteem.AdminController;
using static Reserveringssysteem.MemberController;

namespace Reserveringssysteem
{
	public class UserController
	{
		public static User CurrentUser { get; set; }

		public static bool IsLoggedIn { get; set; }

		public void Login(Action showHeader, Router router)
		{
			Console.ForegroundColor = ConsoleColor.DarkYellow;
			Console.WriteLine("   Typ 'q' als u terug wilt gaan.\n");
        	Console.BackgroundColor = ConsoleColor.White;
        	Console.ForegroundColor = ConsoleColor.Black;

			string userCredential = "   Gebruikersnaam:";
			Console.WriteLine(userCredential);
			string username = CheckErrors("username", userCredential);

			if (username != "q")
			{
				string passwordCredential = "\n   Wachtwoord:";
				Console.WriteLine(passwordCredential);
				string password = CheckErrors("password", passwordCredential, username);

				if (password != "q")
				{
					VerifyLogin(username, password);

					Console.WriteLine();

					string[] options = new string[]
					{
						"Log in"
					};

					string choice = router.AwaitResponse(options);

					Console.Clear();
					showHeader();

					Console.WriteLine("   Ingelogd!\n");

					options = new string[]
					{
							"Terug",
					};

					choice = router.AwaitResponse(options);

					if (CurrentUser is Admin)
					{
						router.SetCurrentScreen("Admin");
					} else
					{
						router.SetCurrentScreen("Home");
					}
				} else
				{
					router.SetCurrentScreen("Authorizatie");
				}
			} else
			{
				router.SetCurrentScreen("Authorizatie");
			}
		}

		// Verifieert de gegeven gebruikersnaam en wachtwoord van de gebruiker.
		protected void VerifyLogin(string username, string password)
		{
			if (username == "admin" && password == "123")
			{
				CurrentUser = makeAdmin();
				IsLoggedIn = true;
			} else
			{

				var users = Members;

				for (int i = 0; i < users.Count; i++)
				{
					if (users[i].GetUsername() == username)
					{
						if (users[i].GetPassword() == password)
						{
							IsLoggedIn = true;
							CurrentUser = users[i];
						}
					}
				}
			}
		}

		// Logt de gebruiker uit van het systeem.
		public static void LogOut()
		{
			IsLoggedIn = false;
			SetUser(null);
		}

		// Bewerkt de balk die verteld hoever je bent met registreren
		protected void UpdateBar(int value, (int, int) cursorPosition, int count)
		{
			int procent = value;
			string bar = $"   {procent}% klaar:  ";
			Console.CursorTop = 11;
			Console.Write(bar);

			if (value == 0)
			{
				Console.BackgroundColor = ConsoleColor.Black;
				Console.WriteLine("                                                                 \n");
				
        		Console.BackgroundColor = ConsoleColor.White;
        		Console.ForegroundColor = ConsoleColor.Black;
			} else
			{
				Console.CursorLeft = bar.Length;
				Console.BackgroundColor = ConsoleColor.Green;
				for (int i = 0; i < count; i++)
				{
					Console.Write("             ");
				}
				
        		Console.BackgroundColor = ConsoleColor.White;
        		Console.ForegroundColor = ConsoleColor.Black;
			}

			Console.SetCursorPosition(cursorPosition.Item1, cursorPosition.Item2 + 1);
		}

		// Maakt het gebruiker invulveld leeg.
		private void EmptyField(string value, string field)
		{
			Console.CursorTop--;
			Console.CursorLeft = 3;
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write(value);
        	Console.BackgroundColor = ConsoleColor.White;
        	Console.ForegroundColor = ConsoleColor.Black;
			Console.CursorLeft = 0;
		}

		// Laat de error zien aan de gebruiker.
		private void ShowError(string field, string question, string error)
		{

			Console.CursorTop--;
			Console.CursorLeft = question.Length;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.Write(error);
        	Console.BackgroundColor = ConsoleColor.White;
        	Console.ForegroundColor = ConsoleColor.Black;
			Console.CursorTop++;
			Console.CursorLeft = 0;
		}

		// Verbergt de error.
		private void HideError(string field, string question, string error)
		{
			Console.CursorTop = Console.CursorTop - 2;
			Console.CursorLeft = question.Length + 1;
			Console.ForegroundColor = ConsoleColor.White;
			Console.Write(error);
        	Console.BackgroundColor = ConsoleColor.White;
        	Console.ForegroundColor = ConsoleColor.Black;
			Console.CursorTop = Console.CursorTop + 2;
			Console.CursorLeft = 0;
		}

		// Checkt of de e-mailadres geldig is
		private bool ValidEmail(string value)
		{
			var trimmedEmail = value.Trim();

			if (trimmedEmail.EndsWith("."))
			{
				return false;
			}
			try
			{
				var addr = new System.Net.Mail.MailAddress(value);
				return addr.Address == trimmedEmail;
			} catch
			{
				return false;
			}
		}

		// Checkt of de e-mailadres al bestaat
		private bool EmailExists(string value)
		{
			var members = GetMembers();

			for (int i = 0; i < members.Count; i++)
			{
				if (members[i].GetEmailAddress() == value)
				{
					return false;
				}
			}

			return true;
		}

		// Checkt of de gebruikersnaam al bestaat.
		private bool UsernameExists(string value)
		{
			var members = GetMembers();

			for (int i = 0; i < members.Count; i++)
			{
				if (members[i].GetUsername() == value)
				{
					return false;
				}
			}

			return true;
		}

		// Controleert of de gebruiker wel bestaat.
		private bool UserExists(string value)
		{
			var members = GetMembers();

			for (int i = 0; i < members.Count; i++)
			{
				if (members[i].GetUsername() == value)
				{
					return true;
				}
			}
			return false;
		}

		public bool IsDateTime(string value)
		{
			DateTime tempDate;
			return DateTime.TryParse(value, out tempDate);
		}

		private bool CheckCredentials(string username, string password)
		{
			var members = GetMembers();

			for (int i = 0; i < members.Count; i++)
			{
				if (members[i].GetUsername() == username)
				{
					if (members[i].GetPassword() == password)
					{
						return true;
					} else
					{
						return false;
					}
				}
			}
			return false;
		}

		// Controleert op errors in de input van de gebruiker.
		protected string CheckErrors(string field, string question, string username = "")
		{
			Console.CursorLeft = 3;

			string value = Console.ReadLine();
			bool errorsGone = false;
			string error = "";

			if (value == "q")
			{
				errorsGone = true;
			} else if ((value == "admin" && field == "username") || (value == "123" && field == "password"))
			{
				errorsGone = true;
			}
			while (!errorsGone)
			{
				switch (value)
				{
					case "":
						error = " Het mag niet leeg zijn";
						break;
					case string a when a.Contains(" "):
						error = " Geen spaties";
						break;
					case string a when a.Any(c => !char.IsLetter(c)) && (field != "emailAddress" && field != "wachtwoord" && field != "gebruikersnaam"
					&& field != "username" && field != "password" && field != "birthDate" && field != "geslacht"):
						error = " Geen cijfers of andere symbolen";
						break;
					case string a when (a.Any(c => char.IsLetter(c)) || a.Any(c => !char.IsLetter(c))) && field == "geslacht":
						if (value == "1")
						{
							value = "Man";
						} else if (value == "2")
						{
							value = "Vrouw";
						} else
						{
							error = " U moet kiezen uit een van de keuzes.";
						}
						break;
					case string a when !ValidEmail(a) && field == "emailAddress":
						error = " E-mail klopt niet";
						break;
					case string a when !EmailExists(a) && field == "emailAddress":
						error = " E-mail is al in gebruik door iemand anders";
						break;
					case string a when (a.Any(c => !char.IsLetter(c))) && field == "gebruikersnaam":
						bool hasLetters = false;
                        if (!Regex.IsMatch(value, @"^[a-zA-Z0-9]+$"))
                        {
							error = " Symbolen kunnen niet erin.";
                        }

						if (a.Any(c => char.IsLetter(c)))
						{
							hasLetters = true;
						}

						if (!hasLetters)
						{
							error = " Symbols of alleen cijfers kan niet.";
						}

						break;
					case string a when !UsernameExists(a) && field == "gebruikersnaam":
						error = " Gebruikersnaam bestaat al kies iets anders";
						break;
					case string a when !UserExists(a) && field == "username":
						error = " Gebruikersnaam bestaat niet";
						break;
					case string a when !CheckCredentials(username, a) && field == "password":
						error = " Wachtwoord is incorrect.";
						break;
					case string a when !IsDateTime(a) && field == "birthDate":
						error = " Geboortedatum klopt niet.";
						break;
					case string a when IsDateTime(a) && field == "birthDate":
						if (Regex.Match(value, @"\d{4}$").Success)
						{
							DateTime tempDate = DateTime.Parse(value);
							DateTime current = DateTime.Now;

							if (tempDate > current)
							{
								error = " Datum kan niet groter zijn dan dit jaar.";
								break;
							} else if ((current.Year - tempDate.Year) > 60)
							{
								error = " Datum is ongeldig.";
								break;
							} else if (!(current.Year - tempDate.Year >= 13))
							{
								error = " Minstens 13 jaar of ouder.";
								break;
							}
						} else
						{
							error = " Ongeldige jaar.";
							break;
						}
						break;
				}
				if (error != "" && value != "q" && value != "admin" && value != "123")
				{
					EmptyField(value, field);
					ShowError(field, question, error);
					Console.CursorLeft = 3;
					value = Console.ReadLine();
					HideError(field, question, error);
					error = "";
				} else
				{
					errorsGone = true;
				}
			}
			return value.ToString();
		}

		public static User GetUser()
		{
			if (CurrentUser != null)
			{
				return CurrentUser;
			}

			return null;
		}

		public static void SetUser(User user)
		{
			CurrentUser = user;
		}
	}
}
