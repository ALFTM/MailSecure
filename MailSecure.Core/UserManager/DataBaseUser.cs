using System;
using System.Collections.Generic;

namespace MailSecure.Core
{
    public class DataBaseUser
    {
        public string Name { get; set; }
        public byte[] Hash { get; set; }
        public byte[] Pass { get; set; }

        public DataBaseUser()
        {
        }

        public DataBaseUser(string name, byte[] hash, byte[] pass)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Hash = hash ?? throw new ArgumentNullException(nameof(hash));
            Pass = pass ?? throw new ArgumentNullException(nameof(pass));
        }
    }
}
