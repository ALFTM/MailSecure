using MailSecure.Core;
using MailSecure.Security;
using System;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace MailSecure
{
    class LoginWindowViewModel: BaseViewModel
    {
        #region Private Members
        /// <summary>
        /// Outer Margin for shadow
        /// </summary>
        private int outerMargin = 10;

        /// <summary>
        /// Window Radius
        /// </summary>
        private int windowRadius = 0;

        private WindowDockPosition dockerPosition = WindowDockPosition.Undocked;
        private LoginWindow window;
        private SignUpControl control;
        private uint switchView;
        #endregion

        #region Public Members
        public bool Borderless => (window.WindowState == WindowState.Maximized || dockerPosition != WindowDockPosition.Undocked);

        /// <summary>
        /// Resize size
        /// </summary>
        public int ResizeBorder { get; set; } = 6;

        /// <summary>
        /// resize size with outer margin
        /// </summary>
        public Thickness ResizeBorderThickness => new Thickness(ResizeBorder + OuterMarginSize);

        /// <summary>
        /// Window padding
        /// </summary>
        public Thickness InnerContentPadding { get; set; } = new Thickness(0);

        /// <summary>
        /// Outer maring for shadow
        /// </summary>
        public int OuterMarginSize
        {
            get => Borderless ? 0 : outerMargin;
            set => outerMargin = value;
        }

        /// <summary>
        /// Outer maring for shadow
        /// </summary>
        public Thickness OuterMarginSizeThickness => new Thickness(OuterMarginSize);

        /// <summary>
        /// Window Radius
        /// </summary>
        public int WindowRadius
        {
            get => Borderless ? 0 : windowRadius;
            set => windowRadius = value;
        }

        /// <summary>
        /// Window Radius
        /// </summary>
        public CornerRadius WindowCornerRadius => new CornerRadius(WindowRadius);

        /// <summary>
        /// Header size / Caption size
        /// </summary>
        public int TitleHeight { get; set; } = 32;

        /// <summary>
        /// Header size / Caption size
        /// </summary>
        public GridLength TitleHeightGridLength => new GridLength(TitleHeight + ResizeBorder);

        public uint SwitchView
        {
            get { return switchView; }
            set
            {
                switchView = value;
                OnPropertyChanged(nameof(switchView));
            }
        }

        public SignUpControl Control
        {
            get { return control; }
            set { control = value; }
        }

        #endregion

        #region Commands
        public ICommand CloseCommand { get; set; }
        #endregion

        #region Constructor
        public LoginWindowViewModel(LoginWindow window)
        {
            this.window = window;
            switchView = 0;
            InitCommands();

        }
        #endregion

        public void Terminate(bool result)
        {
            if (result) {
                CheckIfPGPKeyExist();
                App.BaseWindow = new FlatWindow();
                App.BaseWindow.Show();
            }  
            System.Console.WriteLine(result);
            window.Close();
            
        }

        private void InitCommands()
        {
            CloseCommand = new RelayCommand(() => window.Close());
        }

        public void SwitchForm(uint value)
        {
            string mode = value == 0 ? "SIGN IN" : "SIGN UP";
            System.Console.WriteLine("SWTICH TO " + mode + " MODE");
            SwitchView = value;
        }

        private void CreateAccount()
        {
            var template = window.ContentTemplate;
            throw new System.NotImplementedException("This metohds is in progress...");
        }

        private void SignIn() => throw new System.NotImplementedException("This metohds is in progress...");

        private void CheckIfPGPKeyExist()
        {
            string folderPath = Environment.ExpandEnvironmentVariables(AppConst.APP_DATA_FOLDER_KEY);
            string currentUserPath = folderPath + "\\" + App.CurrentUserData.CurrentUser.EmailAdress;
            string keyFile = currentUserPath + "\\pubring.gpg";
            if (!Directory.Exists(folderPath)) {
                try {
                    Directory.CreateDirectory(folderPath);
                }
                catch (IOException e) {
                    Console.WriteLine(e.ToString());
                }
            }

            if (!Directory.Exists(currentUserPath)) {
                try {
                    Directory.CreateDirectory(currentUserPath);
                }
                catch (IOException e) {
                    Console.WriteLine(e.ToString());
                }
            }

            if (!File.Exists(keyFile)) {
                PgpKeyGen.generateKeys(App.CurrentUserData.CurrentUser.EmailAdress, Utils.ConvertToUnsecureString(App.CurrentUserData.PassHash), currentUserPath);
                PgpEncryptionKeys keys = new PgpEncryptionKeys(currentUserPath + "\\pubring.gpg",
                    currentUserPath + "\\secring.gpg", Utils.ConvertToUnsecureString(App.CurrentUserData.PassHash));
            }


        }
    }
}
