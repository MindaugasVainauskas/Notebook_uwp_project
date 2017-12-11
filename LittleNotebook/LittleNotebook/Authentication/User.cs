using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace LittleNotebook.Authentication
{
    //User model class for user login details.
    public class User : IIdentity
    {
        public User(string uName, string email, string password)
        {
            Name = uName;
            Email = email;
            Password = password;
        }

        public User(string userName, string email)
        {
            Name = userName;
            Email = email;
        }

        //Have to implement the Name interface
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }


        //This method checks if user is authenticated
        public bool IsAuthenticated { get { return !string.IsNullOrEmpty(Name); } }

        //Implementing interface required by Security.Principal
        public string AuthenticationType
        {
            get { return "Notebook Authentication"; }
        }

    }
}
