using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleNotebook.Authentication
{
    //User model class for user login details.
    public class User
    {
        public User(string uName, string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; private set; }
        public string Password { get; private set; }

        //This method checks if user is authenticated
        public bool IsAuthenticated { get { return !string.IsNullOrEmpty(Email); } }

    }
}
