using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;

namespace LittleNotebook.Authentication
{
    public interface IAuthenticationService
    {
        User AuthenticateUser(string username, string password);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly IBuffer randBuffer;
        private readonly IBuffer randBufCBC;
        private readonly CryptographicKey cryptKey;

        private readonly string algName;
        private readonly SymmetricKeyAlgorithmProvider keyAlgProvider;

        public AuthenticationService()
        {
            algName = SymmetricAlgorithmNames.AesEcbPkcs7;
            keyAlgProvider = SymmetricKeyAlgorithmProvider.OpenAlgorithm(algName);
            randBuffer = CryptographicBuffer.GenerateRandom(keyAlgProvider.BlockLength);
            randBufCBC = null;
            cryptKey = keyAlgProvider.CreateSymmetricKey(randBuffer);
        }      
        /// <param name="randBuffer">The custom generated buffer</param>
        public AuthenticationService(IBuffer rBuffer)
            : this()
        {
            this.randBuffer = rBuffer;
            cryptKey = keyAlgProvider.CreateSymmetricKey(rBuffer);
        }
        public User AuthenticateUser(string username, string password)
        {
            throw new NotImplementedException();
        }
        private class InternalUserInfo
        {
            public InternalUserInfo(string email, string password)
            {
                Email = email;
                Password = password;
            }

            public string Email { get; private set; }
            public string Password { get; private set; }
        }
    }
}
