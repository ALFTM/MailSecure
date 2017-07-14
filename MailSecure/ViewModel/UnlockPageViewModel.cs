using MailSecure.Core;
using System.Collections.ObjectModel;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MailSecure
{
    class UnlockPageViewModel : BaseViewModel
    {
        #region Private Members
        private string loadingIsVisible;
        private string imapListIsVisible;
        #endregion

        #region Public Members
        public ObservableCollection<MailMessage> ImapList { get; set; }
        public string LoadingIsVisible
        {
            get => loadingIsVisible;
            set
            {
                loadingIsVisible = value;
                OnPropertyChanged(nameof(loadingIsVisible));
            }
        }
        public string ImapListIsVisible
        {
            get => imapListIsVisible;
            set
            {
                imapListIsVisible = value;
                OnPropertyChanged(nameof(imapListIsVisible));
            }
        }
        #endregion

        #region Constructor
        public UnlockPageViewModel()
        {
            ImapList = new ObservableCollection<MailMessage>();
            FullFillMessageList(App.CurrentUserData.CurrentUser);
        }
        #endregion

        #region Private Methods
        private async void FullFillMessageList(UserMailFacts crtUser)
        {
            var mailReceiver = new MailReceiver(crtUser);
            mailReceiver.PrepareImap();

            var listMessage = await Task.Run(() => mailReceiver.GetMessagesHeader());

            foreach (MailMessage mailMessage in listMessage)
            {
                var from = mailMessage.From;

                ImapList.Add(mailMessage);
            }
            LoadingIsVisible = "false";
            ImapListIsVisible = "true";
        }
        #endregion
    }
}
