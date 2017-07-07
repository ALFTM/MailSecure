using MailSecure.Core;
using Microsoft.Win32;
using System.Collections.ObjectModel;
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

        public ObservableCollection<string> AttachementsList;

        #endregion

        #region Commands
        public ICommand DisplayCopyFieldsCommand { get; set; }
        public ICommand AddAttachementCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// The default constructor
        /// </summary>
        public SendingPageViewModel()
        {
            AttachementsList = new ObservableCollection<string>();
            DisplayCopyFieldsCommand = new RelayCommand(() => SetCopyVisibility());
            AddAttachementCommand = new RelayCommand(() => AddAttachement());
        }
        #endregion

        #region Private methods
        private void SetCopyVisibility()
        {
            CopyFieldVisibility = CopyFieldVisibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }

        private void AddAttachement()
        {
            OpenFileDialog fileToLoad = new OpenFileDialog();
            fileToLoad.Filter = "All Files (*.*)|*.*";
            fileToLoad.FilterIndex = 1;
            fileToLoad.Multiselect = true;

            if (fileToLoad.ShowDialog() == true) {
                for(int i = 0; i < fileToLoad.FileNames.Length; i++) {
                    AttachementsList.Add(fileToLoad.FileNames[i]);
                }
            }
        }
        #endregion
    }
}
