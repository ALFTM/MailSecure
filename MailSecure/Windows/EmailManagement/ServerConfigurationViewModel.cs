using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSecure.Core;
using System.Security;
using System.Windows;
using System.Windows.Input;
using System.Security.Cryptography;

namespace MailSecure
{
    class ServerConfigurationViewModel : BaseViewModel
    {
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
        public string Title
        {
            get => title;
            set
            {
                title = baseTitle + value;
                OnPropertyChanged(nameof(title));
            }
        }

        public string SmtpAddress
        {
            get => smtpAddress;
            set
            {
                smtpAddress = value;
                OnPropertyChanged(nameof(smtpAddress));
            }
        }

        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged(nameof(login));
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(name));
            }
        }

        public Visibility Visibility { get; set; }
        #endregion

        #region Private members
        private WindowResizer windowResizer;

        /// <summary>
        /// Outer Margin for shadow
        /// </summary>
        private int outerMargin = 10;

        /// <summary>
        /// Window Radius
        /// </summary>
        private int windowRadius = 0;

        private WindowDockPosition dockerPosition = WindowDockPosition.Undocked;

        private MailServerConfigurationWindow window;
        private string smtpAddress = "";
        private string login = "";
        private string name;
        private string title;
        private string baseTitle = "Configurer un serveur ";
        #endregion

        #region Commands
        public ICommand CloseCommand { get; set; }
        public ICommand AddCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// The Default Constructor
        /// </summary>
        public ServerConfigurationViewModel(MailServerConfigurationWindow window)
        {
            this.window = window;
            Title = "Imap";
            Visibility = Visibility.Visible;

            window.StateChanged += (sender, e) => {
                WindowResized();
            };


            InitCommands();

            windowResizer = new WindowResizer(window);
            windowResizer.WindowDockChanged += (dock) => {
                dockerPosition = dock;

                WindowResized();
            };
        }

        /// <summary>
        /// The Constructor predifined server
        /// </summary>
        /// <param name="title"></param>
        /// <param name="smtp"></param>
        public ServerConfigurationViewModel(MailServerConfigurationWindow window, string title, string smtp)
        {
            this.window = window;
            Title = title;
            SmtpAddress = smtp;
            Visibility = Visibility.Collapsed;

            window.StateChanged += (sender, e) => {
                WindowResized();
            };


            InitCommands();

            windowResizer = new WindowResizer(window);
            windowResizer.WindowDockChanged += (dock) => {
                dockerPosition = dock;

                WindowResized();
            };
        }
        #endregion

        #region Private Helper
        public Point GetMousePosition()
        {
            var position = Mouse.GetPosition(window);
            return new Point(position.X + window.Left, position.Y + window.Top);
        }

        public void CloseApp()
        {
            window.Close();
        }

        public void WindowResized()
        {
            OnPropertyChanged(nameof(Borderless));
            OnPropertyChanged(nameof(ResizeBorderThickness));
            OnPropertyChanged(nameof(OuterMarginSize));
            OnPropertyChanged(nameof(OuterMarginSizeThickness));
            OnPropertyChanged(nameof(WindowRadius));
            OnPropertyChanged(nameof(WindowCornerRadius));
        }
        #endregion

        #region Methods
        private void InitCommands()
        {
            // Command init
            CloseCommand = new RelayCommand(() => CloseApp());
            AddCommand = new RelayCommand(() => SaveConfiguration());
        }

        private void SaveConfiguration()
        {
            UserMailFacts userFacts = new UserMailFacts();

            this.CryptPassword(ref userFacts);

            userFacts.userName = Name;
            userFacts.login = Login;
            userFacts.smtpAdress = SmtpAddress;
            userFacts.email = Login;

            bool res = BinaryMCSFileManager.WriteStructInFile(userFacts);

            if (res) {
                App.CurrentUserData.CurrentUser = userFacts;
                App.CurrentUserData.DisplayedName = userFacts.userName;
            }
            CloseApp();
        }

        private void CryptPassword(ref UserMailFacts userFacts)
        {
            byte[] plainText = System.Text.Encoding.UTF8.GetBytes(window.passwordBox.Password);

            // Generate additional entropy (will be used as the Initialization vector)
            byte[] entropy = new byte[20];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(entropy);

            byte[] encodeText = ProtectedData.Protect(plainText, entropy, DataProtectionScope.CurrentUser);

            plainText.Initialize();

            userFacts.entropy = entropy;
            userFacts.encodingText = encodeText;
        }
        #endregion
    }
}
