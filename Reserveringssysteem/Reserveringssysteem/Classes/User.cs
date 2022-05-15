using Newtonsoft.Json;
using System;

namespace Reserveringssysteem
{
    public class User
    {
        [JsonProperty("id")]
        private int Id { get; set; }

        [JsonProperty("username")]
        private string Username { get; set; }

        [JsonProperty("password")]
        private string Password { get; set; }

        [JsonProperty("firstName")]
        private string FirstName { get; set; }

        [JsonProperty("surname")]
        private string Surname { get; set; }

        [JsonProperty("gender")]
        private string Gender { get; set; }

        [JsonProperty("emailAddress")]
        private string EmailAddress { get; set; }

        [JsonProperty("birthDate")]
        private string BirthDate { get; set; }

        public User(int id, string username, string password, string firstName, string surname, string gender, string birthDate, string emailAddress)
        {
            this.Id = id;
            this.Username = username;
            this.Password = password;
            this.FirstName = firstName;
            this.Surname = surname;
            this.Gender = gender;
            this.BirthDate = birthDate;
            this.EmailAddress = emailAddress;
        }

        public string GetFirstName()
        {
            return this.FirstName;
        }

        public string GetUsername()
        {
            return this.Username;
        }

        public string GetPassword()
        {
            return this.Password;
        }

        public string GetEmailAddress()
        {
            return this.EmailAddress;
        }

        public string GetGender()
        {
            return this.Gender;
        }

        public override string ToString()
        {
            return String.Format(
                "  Gebruikersnaam: {0}\n" +
                "  Voornaam: {1}\n" +
                "  Achternaam: {2}\n" +
                "  Geslacht: {3}\n" +
                "  Geboortedatum: {4}\n" +
                "  Email Address: {5}",
                Username, FirstName, Surname, Gender, BirthDate, EmailAddress
            );
        }
    }
}
