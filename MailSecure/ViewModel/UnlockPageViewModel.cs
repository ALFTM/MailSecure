using MailSecure.Core;
using System.Collections.ObjectModel;
using System.Net.Mail;

namespace MailSecure
{
    class UnlockPageViewModel : BaseViewModel
    {
        #region Public Members
        public ObservableCollection<MailMessage> ImapList { get; set; }
        #endregion

        #region Constructor
        public UnlockPageViewModel()
        {
            ImapList = new ObservableCollection<MailMessage>();
            var mailReceiver = new MailReceiver(App.CurrentUserData.CurrentUser);
            mailReceiver.PrepareImap();

            var listMessage = mailReceiver.GetMessagesHeader();

            foreach(MailMessage mailMessage in listMessage)
            {
                var from = mailMessage.From;

                ImapList.Add(mailMessage);
            }
        }
        #endregion
    }
}
