using MailSecure.Core;
using MailSecure.Security;
using System;
using System.Security;
using System.Windows;
using System.Windows.Input;

namespace MailSecure.ViewModel
{
    public class RegisterViewModel
    {
        #region Private Members
        SignUpControl control;
        #endregion

        #region Public Members
        public SignUpControl Control { get => control; set => control = value; }
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
        public ICommand CreateAccountCommand { get; set; }
        public ICommand SignInCommand { get; set; }
        #endregion

        #region Constructor
        public RegisterViewModel(SignUpControl control)
        {
            Control = control;
            SwitchToSignInFormCommand = new RelayCommand(() => SwitchForm(0));
            CreateAccountCommand = new RelayParameterizedCommand((parameter) => CreateAccount(parameter));
        }
        #endregion

        #region Public Methods
        private void SwitchForm(uint value)
        {
            Window parent = Window.GetWindow(control) as LoginWindow;
            var controller = parent.DataContext as LoginWindowViewModel;
            controller.SwitchForm(value);
        }

        private void CreateAccount(object parameter)
        {
            IHavePassword passwordContainer = parameter as IHavePassword;
            if (passwordContainer != null) {
                SecureString secureString = passwordContainer.Password;
                UserManager userManager = new UserManager();
                string login = control.loginId.Text;
                bool result = userManager.AddUserInDataBase(login, secureString);
                Window parent = Window.GetWindow(control) as LoginWindow;

                var controller = parent.DataContext as LoginWindowViewModel;
                controller.Terminate(result);
            }
        }
        #endregion
    }
}
