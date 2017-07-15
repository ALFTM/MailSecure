using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSecure.Core
{
    public class UserMailFacts
    {
        public string UserName { get; set; }
        public string EmailAdress { get; set; }
        public string SmtpAdress { get; set; }
        public string ImapAdress { get; set; }
        public string Login { get; set; }
        public byte[] EncodingText { get; set; }
        public byte[] Entropy { get; set; }
    }
}
