using MailSecure.Core;
using System.Windows;
using System.Windows.Input;

namespace MailSecure
{
    class SideMenuViewModel : ContextViewModel
    {
        #region Content Language
        public string SendMail { get { return App.ApplicationLanguage.GetStringFromLanguage("sendMailMenu_lbl"); } }
        public string UnlockMail { get { return App.ApplicationLanguage.GetStringFromLanguage("unlockMailMenu_lbl"); } }
        public string Settings { get { return App.ApplicationLanguage.GetStringFromLanguage("settingMenu_lbl"); } }
        public string About { get { return App.ApplicationLanguage.GetStringFromLanguage("aboutMenu_lbl"); } }
        #endregion

        #region Commands
        public ICommand OpenSettingCommand { get; set; }
        public ICommand OpenSendingMailCommand { get; set; }
        public ICommand OpenUnlockCommand { get; set; }
        public ICommand OpenAboutCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// The default constructor
        /// </summary>
        public SideMenuViewModel()
        {
            CurrentPage = App.CurrentUserData.CurrentPage;
            OpenSettingCommand = new RelayCommand(() => ChangeCurrentPage(PageType.SettingPage));
            OpenSendingMailCommand = new RelayCommand(() => ChangeCurrentPage(PageType.SendingPage));
            OpenUnlockCommand = new RelayCommand(() => ChangeCurrentPage(PageType.UnlockPage));
            OpenAboutCommand = new RelayCommand(() => OpenAboutWindow());
        }
        #endregion

        #region Methods

        private void OpenSettingWindow()
        {
            ChangeCurrentPage(PageType.SettingPage);
            MailServerConfigurationWindow window = new MailServerConfigurationWindow();
            window.ShowDialog();
        }

        private void OpenAboutWindow()
        {
            AboutWindow window = new AboutWindow();
            window.ShowDialog();
        }

        private void ChangeCurrentPage(PageType value)
        {
            CurrentPage = value;
            App.CurrentUserData.CurrentPage = CurrentPage;
        }

        #endregion

    }
}
