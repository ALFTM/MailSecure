using MailSecure.Core;
using System.Windows.Input;

namespace MailSecure
{
    class SideMenuViewModel : BaseViewModel
    {
        #region Commands
        public ICommand OpenSettingCommand { get; set; }
        public ICommand OpenSendingMailCommand { get; set; }
        public ICommand OpenUnlockCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// The default constructor
        /// </summary>
        public SideMenuViewModel()
        {
            OpenSettingCommand = new RelayCommand(() => OpenSettingWindow());
            OpenSendingMailCommand = new RelayCommand(() => ChangeCurrentPage(PageType.SendingPage));
            OpenUnlockCommand = new RelayCommand(() => ChangeCurrentPage(PageType.UnlockPage));
        }
        #endregion

        #region Methods

        private void OpenSettingWindow()
        {
            MailServerConfigurationWindow window = new MailServerConfigurationWindow();
            window.ShowDialog();
        }

        private void ChangeCurrentPage(PageType value)
        {
            App.CurrentUserData.CurrentPage = value;
        }

        #endregion

    }
}
