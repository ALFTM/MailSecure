using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSecure
{
    public class UserMailFacts
    {
        public string userName { get; set; }
        public string email { get; set; }
        public string smtpAdress { get; set; }
        public string login { get; set; }
        public byte[] encodingText { get; set; }
        public byte[] entropy { get; set; }
    }

    
}
