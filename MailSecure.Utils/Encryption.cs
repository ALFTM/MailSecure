using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MailSecure.Security
{
    public class Encryption
    {
        private const int keySize = 256;
        
        private const int derivationIterations = 1000;

        #region Encryption

        public static string Encrypt(string plainText, string passPhrase)
        {
            var saltStringBytes = GenerateRandomBytes();
            var ivStringBytes = GenerateRandomBytes();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, derivationIterations))
            {
                var keyBytes = password.GetBytes(keySize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();

                                var textBytes = saltStringBytes;
                                textBytes = textBytes.Concat(ivStringBytes).ToArray();
                                textBytes = textBytes.Concat(memoryStream.ToArray()).ToArray();

                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(textBytes);
                            }
                        }
                    }
                }
            }
        }

        // Function to Generate a 64 bits Key.
        static string GenerateKey()
        {
            // Create an instance of Symetric Algorithm. Key and IV is generated automatically.
            DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();

            // Use the Automatically generated key for Encryption. 
            return ASCIIEncoding.ASCII.GetString(desCrypto.Key);
        }
#endregion

        #region Decryption

        public static string Decrypt(string text, string passPhrase)
        {
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(text);
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(keySize / 8).ToArray();
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(keySize / 8).Take(keySize / 8).ToArray();
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((keySize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((keySize / 8) * 2)).ToArray();

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, derivationIterations))
            {
                var keyBytes = password.GetBytes(keySize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

#endregion

        private static byte[] GenerateRandomBytes()
        {
            var randomBytes = new byte[32];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }
    }
}
