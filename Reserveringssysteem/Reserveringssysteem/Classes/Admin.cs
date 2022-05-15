using System;

namespace Reserveringssysteem
{
    public class Admin : User
    {
        public Admin(int id, string username, string password, string firstName, string surname, string gender, string birthDate, string emailAddress) :
            base(id, username, password, firstName, surname, gender, birthDate, emailAddress) {}
    }
}
