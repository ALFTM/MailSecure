using System.Text;

namespace MailSecure.Security
{
    public class FileEncryptionCBC : IFileEncryption
    {
        /// <summary>Decrypt a file with our CBC.</summary>
        /// <param name="sourceFilename">The full path and name of the file to be decrypted.</param>
        /// <param name="destinationFilename">The full path and name of the file to be output.</param>
        /// <param name="password">The password for the decryption.</param>
        public void DecryptFile(string sourceFilename, string destinationFilename, string password)
        {
            byte[] key = Encoding.ASCII.GetBytes(password);
            byte[] data = System.IO.File.ReadAllBytes(sourceFilename);
            CbcCypher cypher = new CbcCypher((uint)(key.Length));

            byte[] decodedData = cypher.decrypt(data, key);

            System.IO.File.WriteAllBytes(destinationFilename, decodedData);
        }

        /// <summary>Encrypt a file with our CBC.</summary>
        /// <param name="sourceFilename">The full path and name of the file to be encrypted.</param>
        /// <param name="destinationFilename">The full path and name of the file to be output.</param>
        /// <param name="password">The password for the encryption.</param>
        public void EncryptFile(string sourceFilename, string destinationFilename, string password)
        {
            byte[] key = Encoding.ASCII.GetBytes(password);
            byte[] data = System.IO.File.ReadAllBytes(sourceFilename);
            CbcCypher cypher = new CbcCypher((uint)(key.Length));

            byte[] encodedData = cypher.encrypt(data, key);

            System.IO.File.WriteAllBytes(destinationFilename, encodedData);
        }
    }
}
