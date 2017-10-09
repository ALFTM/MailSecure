using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSecure.Security
{
    class FileEncryptionCBC : IFileEncryption
    {
        public void DecryptFile(string sourceFilename, string destinationFilename, string password)
        {
            throw new NotImplementedException();
        }

        public void EncryptFile(string sourceFilename, string destinationFilename, string password)
        {
            throw new NotImplementedException();
        }
    }
}
