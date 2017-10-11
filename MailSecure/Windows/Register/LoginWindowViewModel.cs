using MailSecure.Core;
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
        #endregion

        #region Content Language
        public string SignInText
        {
            get => App.ApplicationLanguage.GetStringFromLanguage("signIn_lbl");
        }

        public string DoesntHaveAccountText
        {
            get => App.ApplicationLanguage.GetStringFromLanguage("doesntHaveAccount_lbl");
        }

        #endregion

        #region Commands
        public ICommand CloseCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        #endregion

        #region Constructor
        public LoginWindowViewModel(LoginWindow window)
        {
            this.window = window;
            InitCommands();

        }
        #endregion

        private void InitCommands()
        {
            CloseCommand = new RelayCommand(() => window.Close());
            LoginCommand = new RelayCommand(() => Login());
        }

        private void Login()
        {
            System.Console.WriteLine("LOG IN SUCCESSFULL");
        }
    }
}
