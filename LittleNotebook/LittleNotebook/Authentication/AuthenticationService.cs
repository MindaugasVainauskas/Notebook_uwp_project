using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LittleNotebook.Authentication
{
    public interface IAuthenticationService
    {
        User AuthenticateUser(string username, string password);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private class InternalUserInfo
        {
            public InternalUserInfo(string uName, string email, string password)
            {
                UserName = uName;
                Email = email;
                Password = password;
            }

            public string Email { get; private set; }
            public string Password { get; private set; }
            public string UserName { get; private set; }

            private static readonly List<InternalUserInfo> _users = new List<InternalUserInfo>()
        {
            new InternalUserInfo("Mark", "mark@company.com",     "MB5PYIsbI2YzCUe34Q5ZU2VferIoI4Ttd+ydolWV0OE="),
            new InternalUserInfo("John", "john@company.com", "hMaLizwzOQ5LeOnMuj+C6W75Zl5CXXYbwDSHWW9ZOXc=")
        };

            public User AuthenticateUser(string username, string clearTextPassword)
            {
                InternalUserInfo userData = _users.FirstOrDefault(u => u.Username.Equals(username)
                    && u.HashedPassword.Equals(CalculateHash(clearTextPassword, u.Username)));
                if (userData == null)
                    throw new UnauthorizedAccessException("Access denied. Please provide some valid credentials.");

                return new User(userData.Username, userData.Email, userData.Roles);
            }
        }
    }
}
