using System;

namespace Reserveringssysteem
{
    public class Member : User
    {
        public Member(int id, string username, string password, string firstName, string surame, string gender, DateTime birthDate, string emailAddress) :
            base(id, username, password, firstName, surame, gender, birthDate, emailAddress) {}
    }
}
