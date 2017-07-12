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
        public IEnumerable<MailMessage> GetRecentMessages()
        {
            var tenDaysBefore = DateTime.Now.AddDays(-10);
            var uids = imapClient.Search(SearchCondition.SentSince(tenDaysBefore));
            return imapClient.GetMessages(uids).OrderByDescending(m => m.Date());
        }

        public List<MailMessage> GetAllMessages()
        {
            var uids = imapClient.Search(SearchCondition.All());
            return imapClient.GetMessages(uids).ToList();
        }

        public void PrepareImap()
        {
            imapClient = new ImapClient(currentUser.ImapAdress, ServerFactConst.DEFAULT_IMAP_PORT, true);
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
