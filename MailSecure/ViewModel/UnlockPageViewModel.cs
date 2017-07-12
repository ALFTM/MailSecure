using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSecure.Core;
using System.Net.Mail;

namespace MailSecure
{
    class UnlockPageViewModel : BaseViewModel
    {
        #region Constructor
        public UnlockPageViewModel()
        {
            var mailReceiver = new MailReceiver(App.CurrentUserData.CurrentUser);
            mailReceiver.PrepareImap();

            var listMessage = mailReceiver.GetAllMessages();

            foreach(MailMessage mailMessage in listMessage)
            {
                var from = mailMessage.From;
            }
        }
        #endregion
    }
}
