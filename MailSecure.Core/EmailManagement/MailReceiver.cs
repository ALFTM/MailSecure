using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S22.Imap;
using System.Security.Cryptography;
using System.Net.Mail;

namespace MailSecure.Core
{
    public class MailReceiver
    {
        #region Private Property
        private ImapClient imapClient;
        private UserMailFacts currentUser;
        #endregion

        #region Constructor
        public MailReceiver(UserMailFacts userAccount)
        {
            this.currentUser = userAccount;
        }
        #endregion

        #region Public Methods
        public List<MailMessage> GetAllMessages()
        {
            var uids = imapClient.Search(SearchCondition.LessThan(50));
            return imapClient.GetMessages(uids).ToList();
        }

        public void PrepareImap()
        {
            imapClient = new ImapClient(ServerFactConst.OUTLOOK_IMAP, ServerFactConst.DEFAULT_IMAP_PORT, true);
            LoginImap();
        }
        #endregion

        #region Private Methods
        private void LoginImap()
        {
            var username = currentUser.EmailAdress;

            byte[] plainText = ProtectedData.Unprotect(currentUser.EncodingText, currentUser.Entropy, DataProtectionScope.CurrentUser);
            var password = Encoding.UTF8.GetString(plainText).ToString();

            imapClient.Login(username, password, AuthMethod.Login);
        }
        #endregion
    }
}
