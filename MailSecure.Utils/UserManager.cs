using MailSecure.Core;
using System;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace MailSecure.Security
{
    public class UserManager
    {

        public bool AddUserInDataBase(string login, SecureString secureString)
        {
            SQLiteHelper sqliteHelper = new SQLiteHelper();
            byte[] salt;
            // Define min and max salt sizes.
            int minSaltSize = 4;
            int maxSaltSize = 8;

            // Generate a random number for the size of the salt.
            Random random = new Random();
            int saltSize = random.Next(minSaltSize, maxSaltSize);

            // Allocate a byte array, which will hold the salt.
            salt = new byte[saltSize];

            // Initialize a random number generator.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            // Fill the salt with cryptographically strong byte values.
            rng.GetNonZeroBytes(salt);

            HashAlgorithm algorithm = new SHA512Managed();

            byte[] plainText = Encoding.ASCII.GetBytes(Utils.ConvertToUnsecureString(secureString));

            byte[] plainTextWithSaltBytes =
              new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++) {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++) {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            var algo = algorithm.ComputeHash(plainTextWithSaltBytes);

            if (sqliteHelper.CheckIfUserExist(login)) {
                return false;
            }

            sqliteHelper.AddUser(login, salt, algo);
            return true;
        }

        public bool AddUserInDataBaseFromString(string login, string pass)
        {
            SQLiteHelper sqliteHelper = new SQLiteHelper();
            byte[] salt;
            // Define min and max salt sizes.
            int minSaltSize = 4;
            int maxSaltSize = 8;

            // Generate a random number for the size of the salt.
            Random random = new Random();
            int saltSize = random.Next(minSaltSize, maxSaltSize);

            // Allocate a byte array, which will hold the salt.
            salt = new byte[saltSize];

            // Initialize a random number generator.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            // Fill the salt with cryptographically strong byte values.
            rng.GetNonZeroBytes(salt);

            HashAlgorithm algorithm = new SHA512Managed();

            byte[] plainText = Encoding.ASCII.GetBytes(pass);

            byte[] plainTextWithSaltBytes =
              new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++) {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++) {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            var algo = algorithm.ComputeHash(plainTextWithSaltBytes);
            sqliteHelper.AddUser(login, salt, algo);

            return true;
        }

    }
}