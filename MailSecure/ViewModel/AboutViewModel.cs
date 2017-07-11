﻿using MailSecure.Core;
using System.Windows;
using System.Windows.Input;

namespace MailSecure
{
    class AboutViewModel : BaseViewModel
    {
        #region Public Members

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

        #region Content Language
        public string SendMail { get { return App.ApplicationLanguage.GetStringFromLanguage("sendMailMenu_lbl"); } }
        public string UnlockMail { get { return App.ApplicationLanguage.GetStringFromLanguage("unlockMailMenu_lbl"); } }
        public string Settings { get { return App.ApplicationLanguage.GetStringFromLanguage("settingMenu_lbl"); } }
        public string CloseLbl { get { return App.ApplicationLanguage.GetStringFromLanguage("aboutMenu_lbl"); } }
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

        #endregion

        #region Constructor
        /// <summary>
        /// Le constructor par défaut
        /// </summary>
        public AboutViewModel(Window window)
        {
            this.window = window;

            window.StateChanged += (sender, e) => {
                WindowResized();
            };


            // Command init


            CloseCommand = new RelayCommand(() => CloseApp());

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
    }
}
