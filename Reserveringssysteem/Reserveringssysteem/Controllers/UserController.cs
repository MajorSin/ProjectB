using System;
using System.Linq;
using static Reserveringssysteem.MemberController;
using static Reserveringssysteem.AdminController;

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
            Console.ResetColor();

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
                    }
                    else
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
            }
            else
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
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine("                                                                 \n");
                Console.ResetColor();
            }
            else
            {
                Console.CursorLeft = bar.Length;
                Console.BackgroundColor = ConsoleColor.Green;
                for (int i = 0; i < count; i++)
                {
                    Console.Write("             ");
                }
                Console.ResetColor();
            }

            Console.SetCursorPosition(cursorPosition.Item1, cursorPosition.Item2 + 1);
        }

        // Maakt het gebruiker invulveld leeg.
        private void EmptyField(string value, string field)
        {
            Console.CursorTop--;
            if (field == "jaar" || field == "maand" || field == "dag")
            {
                Console.CursorLeft = field.Length + 5;
            }
            else
            {
                Console.CursorLeft = 3;
            }
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(value);
            Console.ResetColor();
            Console.CursorLeft = 0;
        }

        // Laat de error zien aan de gebruiker.
        private void ShowError(string field, string question, string error)
        {
            if (field == "maand" || field == "dag")
            {
                if (field == "maand")
                {
                    Console.CursorTop = Console.CursorTop - 2;
                }
                else
                {
                    Console.CursorTop = Console.CursorTop - 3;
                }
            }
            else
            {
                Console.CursorTop--;
            }
            Console.CursorLeft = question.Length;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(error);
            Console.ResetColor();

            if (field == "maand" || field == "dag")
            {
                if (field == "maand")
                {
                    Console.CursorTop = Console.CursorTop + 2;
                }
                else
                {
                    Console.CursorTop = Console.CursorTop + 3;
                }
            }
            else
            {
                Console.CursorTop++;
            }
            Console.CursorLeft = 0;
        }

        // Verbergt de error.
        private void HideError(string field, string question, string error)
        {
            if (field == "maand" || field == "dag")
            {
                if (field == "maand")
                {
                    Console.CursorTop = Console.CursorTop - 3;
                }
                else
                {
                    Console.CursorTop = Console.CursorTop - 4;
                }
            }
            else
            {
                Console.CursorTop = Console.CursorTop - 2;
            }
            Console.CursorLeft = question.Length + 1;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(error);
            Console.ResetColor();

            if (field == "maand" || field == "dag")
            {
                if (field == "maand")
                {
                    Console.CursorTop = Console.CursorTop + 3;
                }
                else
                {
                    Console.CursorTop = Console.CursorTop + 4;
                }
            }
            else
            {
                Console.CursorTop = Console.CursorTop + 2;
            }
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
            }
            catch
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
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        // Controleert op errors in de input van de gebruiker.
        protected string CheckErrors(string field, string question, string username = "", int year = 0, int month = 0)
        {
            if (field == "jaar" || field == "maand" || field == "dag")
            {
                Console.CursorLeft = field.Length + 5;
            }
            else
            {
                Console.CursorLeft = 3;
            }

            int number = 0;
            string value = Console.ReadLine();
            bool errorsGone = false;
            string error = "";

            if (value == "q")
            {
                errorsGone = true;
            }
            else if ((value == "admin" && field == "username") || (value == "123" && field == "password"))
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
                    case string a when a.Any(c => !char.IsLetter(c)) && (field != "emailAddress" && field != "wachtwoord"
                        && field != "jaar" && field != "maand" && field != "dag" && field != "gebruikersnaam"
                        && field != "username" && field != "password"):
                        error = " Geen cijfers of andere symbolen";
                        break;
                    case string a when (a.ToLower() != "man" && a.ToLower() != "vrouw") && field == "geslacht":
                        error = " Geslacht niet juist";
                        break;
                    case string a when !ValidEmail(a) && field == "emailAddress":
                        error = " E-mail klopt niet";
                        break;
                    case string a when !EmailExists(a) && field == "emailAddress":
                        error = " E-mail is al in gebruik door iemand anders";
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
                    case string a when a.Any(c => char.IsLetter(c)) && (field == "jaar" || field == "maand" || field == "dag"):
                        error = " Dit veld mag geen letters bevatten";
                        break;
                    case string a when a.Any(c => !char.IsLetter(c)) && field == "jaar":
                        if (!int.TryParse(a, out number))
                        {
                            error = " Dit is geen getal";
                        }
                        else if (int.Parse(a) > DateTime.Now.Year)
                        {
                            error = " Jaar kan niet groter zijn dan het huidige jaar";
                        }
                        break;
                    case string a when a.Any(c => !char.IsLetter(c)) && field == "maand":
                        if (!int.TryParse(a, out number))
                        {
                            error = " Dit is geen getal";
                        }
                        else if (int.Parse(a) < 1 || int.Parse(a) > 12)
                        {
                            error = " Maand moet tussen 1 en 12 inclusief zijn";
                        }
                        break;
                    case string a when a.Any(c => !char.IsLetter(c)) && field == "dag":
                        if (!int.TryParse(a, out number))
                        {
                            error = " Dit is geen getal";
                        }
                        else if (int.Parse(a) >= 1 && int.Parse(a) <= 31)
                        {
                            if (month != 2)
                            {
                                if ((month == 4 || month == 6 || month == 9 ||
                                    month == 11) && int.Parse(a) == 31)
                                {
                                    error = " Deze dag komt niet voor in uw maand";
                                }
                            }
                            else
                            {
                                bool isLeap = false;
                                if (int.Parse(a) == 29)
                                {
                                    if (year % 4 == 0)
                                    {
                                        if (year % 100 == 0)
                                        {
                                            if (year % 400 == 0)
                                            {
                                                isLeap = true;
                                            }
                                            else
                                            {
                                                isLeap = false;
                                            }
                                        }
                                        else
                                        {
                                            isLeap = true;
                                        }
                                    }
                                    else
                                    {
                                        isLeap = false;
                                    }
                                    if (!isLeap)
                                    {
                                        error = " Uw geboortejaar is geen schrikkeljaar en dus klopt het niet";
                                    }
                                }
                                else if (int.Parse(a) > 28)
                                {
                                    error = " Deze maand heeft niet het aantal dagen.";
                                }
                            }
                        }
                        else
                        {
                            error = " Dag kan alleen tussen 1 en 31 inclusief zijn";
                        }
                        break;
                }
                if (error != "" && value != "q" && value != "admin" && value != "123")
                {
                    EmptyField(value, field);
                    ShowError(field, question, error);
                    if (field == "jaar" || field == "maand" || field == "dag")
                    {
                        Console.CursorLeft = field.Length + 5;
                    }
                    else
                    {
                        Console.CursorLeft = 3;
                    }
                    value = Console.ReadLine();
                    HideError(field, question, error);
                    error = "";
                }
                else
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
