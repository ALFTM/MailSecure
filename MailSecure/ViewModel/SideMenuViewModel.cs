using MailSecure.Core;
using System.Windows.Input;

namespace MailSecure
{
    class SideMenuViewModel : BaseViewModel
    {
        #region Commands
        public ICommand OpenSettingCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// The default constructor
        /// </summary>
        public SideMenuViewModel()
        {
            OpenSettingCommand = new RelayCommand(() => OpenSettingWindow());
        }
        #endregion

        #region Methods

        private void OpenSettingWindow()
        {
            MailServerConfigurationWindow window = new MailServerConfigurationWindow();
            window.ShowDialog();
        }

        #endregion

    }
}
