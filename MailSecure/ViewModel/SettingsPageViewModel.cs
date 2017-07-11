using MailSecure.Core;
using System.Windows.Input;

namespace MailSecure
{
    class SettingsPageViewModel : BaseViewModel
    {
        #region Commands
        public ICommand AddNewUserCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// The default constructor
        /// </summary>
        public SettingsPageViewModel()
        {
            AddNewUserCommand = new RelayCommand(() => StartAddNewUserWindow());
        }
        #endregion

        #region Helper
        private void StartAddNewUserWindow()
        {
            AddUserWindow window = new AddUserWindow();
            window.ShowDialog();
        }
        #endregion
    }
}
