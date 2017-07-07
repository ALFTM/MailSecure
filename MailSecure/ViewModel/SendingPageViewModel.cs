using MailSecure.Core;
using System.Windows;
using System.Windows.Input;

namespace MailSecure
{
    class SendingPageViewModel : BaseViewModel
    {
        #region Private members
        private Visibility copyFieldVisibility = Visibility.Collapsed;
        #endregion

        #region Public members
        public Visibility CopyFieldVisibility
        {
            get => copyFieldVisibility;
            set
            {
                copyFieldVisibility = value;
                OnPropertyChanged(nameof(copyFieldVisibility));
            }
        }

        #endregion

        #region Commands
        public ICommand DisplayCopyFieldsCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// The default constructor
        /// </summary>
        public SendingPageViewModel()
        {
            DisplayCopyFieldsCommand = new RelayCommand(() => SetCopyVisibility());
        }
        #endregion

        #region Private methods
        private void SetCopyVisibility()
        {
            CopyFieldVisibility = CopyFieldVisibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }
        #endregion
    }
}
