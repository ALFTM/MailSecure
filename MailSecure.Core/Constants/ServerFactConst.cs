using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSecure
{
    public static class ServerFactConst
    {
        /* Default port for SMTP connexion (Used by Outlook, Gmail, Hotmail) */
        public static readonly int DEFAULT_SMTP_PORT_SSL = 465;
        public static readonly int DEFAULT_SMTP_PORT_TLS = 587;

        /* Default port for IMAP connexion (Used by Outlook, Gmail, Hotmail) */
        public static readonly int DEFAULT_IMAP_PORT = 993;
    }
}
