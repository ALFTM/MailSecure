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
        public ICommand SwitchToCreateAccountCommand { get; set; }
        public ICommand SignInCommand { get; set; }
        #endregion

        #region Constructor
        public LoginViewModel(LoginControl control)
        {
            Control = control;
            SwitchToCreateAccountCommand = new RelayCommand(() => SwitchForm(1));
            SignInCommand = new RelayParameterizedCommand((parameter) => Login(parameter));
        }
        #endregion

        #region Public Methods
        private void SwitchForm(uint value)
        {
            Window parent = Window.GetWindow(control) as LoginWindow;
            var controller = parent.DataContext as LoginWindowViewModel;
            controller.SwitchForm(value);
        }

        private void Login(object parameter)
        {
            IHavePassword passwordContainer = parameter as IHavePassword;
            if (passwordContainer != null)
            {
                SecureString secureString = passwordContainer.Password;
                UserManager userManager = new UserManager();
                string login = control.loginId.Text;
                bool result = userManager.getLogin(login, secureString);
                Window parent = Window.GetWindow(control) as LoginWindow;

                var controller = parent.DataContext as LoginWindowViewModel;

                DisplayNotification(result);
                if (result)
                    controller.Terminate(result);
            }
        }

        private void DisplayNotification(bool value)
        {
            if (value)
                App.NotificationHelper.DisplayNotification("Welcome \"" + control.loginId.Text + "\"");
            App.NotificationHelper.DisplayNotification("Failed to login as user \"" + control.loginId.Text + "\"");
        }
        #endregion
    }
}
