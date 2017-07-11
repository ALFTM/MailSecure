namespace MailSecure.Core
{
    public static class ServerFactConst
    {
        /* Default port for SMTP connexion (Used by Outlook, Gmail, Hotmail) */
        public static readonly int DEFAULT_SMTP_PORT_SSL = 465;
        public static readonly int DEFAULT_SMTP_PORT_TLS = 587;

        /* Default port for IMAP connexion (Used by Outlook, Gmail, Hotmail) */
        public static readonly int DEFAULT_IMAP_PORT = 993;

        /* Default server address */
        /* Outlook / Live / Hotmail SMTP & IMAP */
        public static readonly string OUTLOOK_SMTP = "smtp-mail.outlook.com";
        public static readonly string OUTLOOK_IMAP = "imap-mail.outlook.com";

        /* Office365 SMTP & IMAP */
        public static readonly string OFFICE_365_SMTP = "smtp.office365.com";
        public static readonly string OFFICE_365_IMAP = "outlook.office365.com";

        /* Gmail SMTP & IMAP */
        public static readonly string GMAIL_SMTP = "smtp.gmail.com";
        public static readonly string GMAIL_IMAP = "imap.gmail.com";

        /* Yahoo SMTP & IMAP */
        public static readonly string YAHOO_SMTP = "smtp.mail.yahoo.com";
        public static readonly string YAHOO_IMAP = "imap.mail.yahoo.com";

        /* iCloud SMTP & IMAP */
        public static readonly string ICLOUD_SMTP = "smtp.mail.me.com";
        public static readonly string ICLOUD_IMAP = "imap.mail.me.com";
    }
}
