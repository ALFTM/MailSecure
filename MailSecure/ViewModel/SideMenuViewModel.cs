using MailSecure.Core;
using System.Windows.Input;

namespace MailSecure
{
    class SideMenuViewModel : BaseViewModel
    {
        #region Private members
        private PageType currentPage;
        #endregion

        #region Public Members
        public PageType CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                OnPropertyChanged(nameof(currentPage));
            }
        }
        #endregion

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
            CurrentPage = App.CurrentUserData.CurrentPage;
            OpenSettingCommand = new RelayCommand(() => OpenSettingWindow());
            OpenSendingMailCommand = new RelayCommand(() => ChangeCurrentPage(PageType.SendingPage));
            OpenUnlockCommand = new RelayCommand(() => ChangeCurrentPage(PageType.UnlockPage));
        }
        #endregion

        #region Methods

        private void OpenSettingWindow()
        {
            ChangeCurrentPage(PageType.SettingPage);
            MailServerConfigurationWindow window = new MailServerConfigurationWindow();
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
