namespace Reserveringssysteem
{
	public class Member : User
	{
		public Member(int id, string username, string password, string firstName, string surname, string gender, string birthDate, string emailAddress) :
			base(id, username, password, firstName, surname, gender, birthDate, emailAddress)
		{ }
	}
}
