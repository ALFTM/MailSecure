using MailSecure.Core;
using System;
using System.Windows;
using System.Windows.Input;

namespace MailSecure
{
    /// <summary>
    /// View Model for th custom View
    /// </summary>
    class NewUserWindowViewModel : BaseViewModel
    {
        #region Public Members

        public string Outlook { get; set; } = "pack://application:,,,/Media/Email/outlook.png";
        public string Exchange { get; set; } = "pack://application:,,,/Media/Email/exchange.png";
        public string Gmail { get; set; } = "pack://application:,,,/Media/Email/google.png";
        public string Yahoo { get; set; } = "pack://application:,,,/Media/Email/yahoo.png";
        public string ICloud { get; set; } = "pack://application:,,,/Media/Email/icloud.png";
        public string Default { get; set; } = "pack://application:,,,/Media/Email/default.png";

        public int MinimumHeight { get; set; } = 200;

        public int MinimumWidth { get; set; } = 200;

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
        public int TitleHeight { get; set; } = 42;

        /// <summary>
        /// Header size / Caption size
        /// </summary>
        public GridLength TitleHeightGridLength => new GridLength(TitleHeight + ResizeBorder);

        #endregion

        #region Private members
        /// <summary>
        /// Window that this modelView control
        /// </summary>
        Window window;

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

        #endregion

        #region Commands

        public ICommand CloseCommand { get; set; }
        public ICommand MenuCommand { get; set; }
        public ICommand OpenServerConfigurationCommand{ get; set; }

        #endregion

        #region Constructor
        /// <summary>
        /// Le constructor par défaut
        /// </summary>
        public NewUserWindowViewModel(Window window)
        {
            this.window = window;

            window.StateChanged += (sender, e) => {
                WindowResized();
            };


            // Command init

            CloseCommand = new RelayCommand(() => CloseApp());
            MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(window, GetMousePosition()));
            OpenServerConfigurationCommand = new RelayParameterizedCommand(type => GoToConfiguration(type));

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

        private void GoToConfiguration(object value)
        {
            value = Int32.Parse(value.ToString());

            switch (value) {
                case 1:
                    Console.WriteLine("Outlook Account");
                    OpenWindow("Outlook", ServerFactConst.OUTLOOK_SMTP);
                    break;
                case 2:
                    Console.WriteLine("Exchange Account");
                    OpenWindow("OFFICE 365", ServerFactConst.OFFICE_365_SMTP);
                    break;
                case 3:
                    Console.WriteLine("Gmail Account");
                    OpenWindow("Gmail", ServerFactConst.GMAIL_SMTP);
                    break;
                case 4:
                    Console.WriteLine("Yahoo Account");
                    OpenWindow("Yahoo!", ServerFactConst.YAHOO_SMTP);
                    break;
                case 5:
                    Console.WriteLine("iCloud Account");
                    OpenWindow("iCloud", ServerFactConst.ICLOUD_SMTP);
                    break;
                default:
                    Console.WriteLine("Imap Account");
                    OpenWindow();
                    break;
            }
        }

        private void OpenWindow(string title, string smtp)
        {
            MailServerConfigurationWindow window = new MailServerConfigurationWindow(title, smtp);
            window.ShowDialog();
        }

        private void OpenWindow()
        {
            MailServerConfigurationWindow window = new MailServerConfigurationWindow();
            window.ShowDialog();
        }

        #endregion
    }
}
