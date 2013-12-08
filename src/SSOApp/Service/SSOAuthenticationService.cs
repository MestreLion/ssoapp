using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;

namespace SSOApp.Service
{
    public static class SSOAuthenticationService
    {
        private const string salt = "tu89geji340t89u2";
        private const string pass = "8daGLnnxIJ46WCCRMkCl";

        private const int keysize = 256;

        public static string EncryptToken(string email)
        {
            var initVectorBytes = Encoding.UTF8.GetBytes(salt);
            var plainTextBytes = Encoding.UTF8.GetBytes(email);
            var password = new PasswordDeriveBytes(pass, null);
            var keyBytes = password.GetBytes(keysize / 8);
            var symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            var cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string DecryptToken(string token)
        {
            var initVectorBytes = Encoding.ASCII.GetBytes(salt);
            var cipherTextBytes = Convert.FromBase64String(token);
            var password = new PasswordDeriveBytes(pass, null);
            var keyBytes = password.GetBytes(keysize / 8);
            var symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            var decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            var plainTextBytes = new byte[cipherTextBytes.Length];
            var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }

        public static string GenerateRandomKey()
        {
            var cryptoProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[keysize];

            cryptoProvider.GetBytes(randomBytes);

            return Convert.ToBase64String(randomBytes);
        }

        public static HttpCookie CreateFormsAuthenticationCookie(string username, int timeout, bool isPersistent)
        {
            string userData = string.Empty;

            var ticket = new FormsAuthenticationTicket(
              1,                                     // ticket version
              username,                              // authenticated username
              DateTime.Now,                          // issueDate
              DateTime.Now.AddMinutes(timeout),           // expiryDate
              isPersistent,                          // true to persist across browser sessions
              userData,                              // can be used to store additional user data
              FormsAuthentication.FormsCookiePath);  // the path for the cookie

            // Encrypt the ticket using the machine key
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

            // Add the cookie to the request to save it
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            cookie.HttpOnly = true;

            return cookie;
        }
    }
}