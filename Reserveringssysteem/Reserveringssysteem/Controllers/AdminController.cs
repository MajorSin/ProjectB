namespace Reserveringssysteem
{
	public class AdminController : UserController
	{
		public static Admin makeAdmin()
		{
			Admin admin = new Admin(1, "admin", "123", "Admin", "Admin", "unknown", "11-1-2001", "admin@hotmail.com");
			return admin;
		}
	}
}
