using System;
using System.Security.Cryptography;

namespace ChatterBox.Core.Services
{
    public class CryptoService : ICryptoService
    {
        public string CreateSalt()
        {
            var data = new byte[0x10];

            using (var crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);

                return Convert.ToBase64String(data);
            }
        }
    }
}
