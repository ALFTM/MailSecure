using MailSecure.Core;
using MailSecure.Security;
using System;
using System.Security;
using System.Windows;
using System.Windows.Input;

namespace MailSecure
{
    class LoginViewModel
    {
        #region Private Members
        LoginControl control;
        #endregion

        #region Public Members
        public LoginControl Control { get => control; set => control = value; }
        #endregion

        #region Content Language
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
        public ICommand SwitchToSignInFormCommand { get; set; }
        public ICommand SignInCommand { get; set; }
        #endregion

        #region Constructor
        public LoginViewModel(LoginControl control)
        {
            Control = control;
            SwitchToSignInFormCommand = new RelayCommand(() => SwitchForm(0));
            SignInCommand = new RelayParameterizedCommand((parameter) => Login(parameter));
        }
        #endregion

        #region Public Methods
        private void SwitchForm(uint value)
        {
        }

        private void Login(object parameter)
        {
            System.Console.WriteLine("jebozsbgienogineiogneziongezoingrioenoeing");
            IHavePassword passwordContainer = parameter as IHavePassword;
            if (passwordContainer != null)
            {
                SecureString secureString = passwordContainer.Password;
                UserManager userManager = new UserManager();
                string login = control.loginId.Text;
                bool result = userManager.getLogin(login, secureString);
                Window parent = Window.GetWindow(control) as LoginWindow;

                var controller = parent.DataContext as LoginWindowViewModel;
                controller.Terminate(result);
            }
        }
        #endregion
    }
}
