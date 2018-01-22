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
        public int tries { get; set; }
        #endregion

        #region Content Language
        public string DoesntHaveAccountText
        {
            get => App.ApplicationLanguage.GetStringFromLanguage("doesntHaveAccount_lbl");
        }

        public string SignInText
        {
            get => App.ApplicationLanguage.GetStringFromLanguage("signIn_lbl");
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
            tries = 0;
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
            control.loginId.Focus();
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
                System.Threading.Thread.Sleep((int)Math.Pow(1000, tries));
                if (result)
                {
                    tries = 0;
                    App.CurrentUserData.PassHash = secureString;
                    controller.Terminate(result);
                }
                else
                    tries++;
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
