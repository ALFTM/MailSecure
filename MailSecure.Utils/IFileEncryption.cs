namespace MailSecure.Security
{
    interface IFileEncryption
    {
        void EncryptFile(string sourceFilename, string destinationFilename, string password);
        void DecryptFile(string sourceFilename, string destinationFilename, string password);
    }
}
