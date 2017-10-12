using MailSecure.Core;
using System.Windows.Input;

namespace MailSecure
{
    class SignUpControlViewModel : BaseViewModel
    {
        #region Content Language
        public string SignUpText
        {
            get => App.ApplicationLanguage.GetStringFromLanguage("signUp_lbl");
        }

        public string CreateAccountText
        {
            get => App.ApplicationLanguage.GetStringFromLanguage("createAccount_lbl");
        }
    
        public string AlreadyHaveAccountText
        {
            get => App.ApplicationLanguage.GetStringFromLanguage("alreadyHaveAccount_lbl");
        }

        #endregion

        #region Commands
        public ICommand CreateNewAccountCommand { get; set; }
        public ICommand GoToLoginFormCommand { get; set; }
        #endregion

        #region Constructor
        public SignUpControlViewModel()
        {
            InitCommands();
        }
        #endregion

        #region Private Methods

        private void InitCommands()
        {
            CreateNewAccountCommand = new RelayCommand(() => CreateAccount());
            GoToLoginFormCommand = new RelayCommand(() => GoToLoginForm());
        }

        private void CreateAccount()
        {
            throw new System.NotImplementedException("This metohds is in progress...");
        }

        private void GoToLoginForm()
        {
            throw new System.NotImplementedException("This metohds is in progress...");
        }

        #endregion
    }
}
