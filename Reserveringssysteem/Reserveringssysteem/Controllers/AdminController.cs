using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Reserveringssysteem
{
    public class AdminController : UserController
    {
        public static Admin makeAdmin()
        {
            Admin admin = new Admin(1, "admin", "123", "Admin", "Admin", "unknown", DateTime.Now, "admin@hotmail.com");
            return admin;
        }
    }
}
