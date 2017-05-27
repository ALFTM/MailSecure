using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSecure
{
    public struct UserMailFacts
    {
        public string userName;
        public string email;
        public string smtpAdress;
        public string login;
        public byte[] encodingText;
        public byte[] entropy;
    }

    
}
