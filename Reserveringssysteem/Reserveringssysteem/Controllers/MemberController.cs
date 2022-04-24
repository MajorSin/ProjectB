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

            string questionOne = "\n   [1] Wat is uw naam?";
            Console.WriteLine(questionOne);
            string firstname = CheckErrors("voornaam", questionOne).ToLower();
            firstname = char.ToUpper(firstname[0]) + firstname.Substring(1);

            count++;
            currentPosition = Console.GetCursorPosition();
            UpdateBar(20, currentPosition, count);

            string questionTwo = "   [2] Wat is uw achternaam?";
            Console.WriteLine(questionTwo);
            string surname = CheckErrors("achternaam", questionTwo).ToLower();
            surname = char.ToUpper(surname[0]) + surname.Substring(1);

            count++;
            currentPosition = Console.GetCursorPosition();
            UpdateBar(40, currentPosition, count);

            string questionThree = "   [3] Wat is uw geslacht? Man/Vrouw";
            Console.WriteLine(questionThree);
            string gender = CheckErrors("geslacht", questionThree).ToLower();
            gender = char.ToUpper(gender[0]) + gender.Substring(1);

            string questionFour = "\n   [4] Wat is uw geboortedatum?";
            Console.WriteLine(questionFour);
            Console.Write("   Jaar: ");
            string year = CheckErrors("jaar", questionFour);

            Console.Write("   Maand: ");
            string month = CheckErrors("maand", questionFour);

            Console.Write("   Dag: ");
            string day = CheckErrors("dag", questionFour, "", int.Parse(year), int.Parse(month));


            Tuple<int, int, int> collection = Tuple.Create(int.Parse(year), int.Parse(month), int.Parse(day));
            DateTime birthDate = new(collection.Item1, collection.Item2, collection.Item3);

            count++;
            currentPosition = Console.GetCursorPosition();
            UpdateBar(60, currentPosition, count);

            count++;

            Console.Clear();
            showHeader();

            currentPosition = Console.GetCursorPosition();
            UpdateBar(0, currentPosition, count);
            UpdateBar(80, currentPosition, count);

            string questionFive = "\n   [5] Wat is uw e-mailaddres?";
            Console.WriteLine(questionFive);
            string emailAddress = CheckErrors("emailAddress", questionFive);

            string questionSix = "\n   [6] Welke gebruikersnaam wilt u?";
            Console.WriteLine(questionSix);
            string username = CheckErrors("gebruikersnaam", questionSix);

            string questionSeven = "\n   [7] Wat wordt uw wachtwoord?";
            Console.WriteLine(questionSeven);
            string password = CheckErrors("wachtwoord", questionSeven);

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

            router.SetCurrentScreen("Authorizatie");
        }

        public static List<Member> GetMembers()
        {
            return Members;
        }

        public static void SetMembers()
        {
            var json = File.ReadAllText("../../../DataFiles/members.json", Encoding.GetEncoding("utf-8"));
            Members = JsonConvert.DeserializeObject<List<Member>>(json);
        }

        public void UpdateMembers()
        {
            var members = GetMembers();
            var stringifiedMembers = JsonConvert.SerializeObject(members, Formatting.Indented);
            File.WriteAllText("../../../DataFiles/members.json", stringifiedMembers);
        }
    }
}
