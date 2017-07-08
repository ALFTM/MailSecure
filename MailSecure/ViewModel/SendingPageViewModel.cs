using MailSecure.Core;
using MailSecure.Security;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace MailSecure
{
    class SendingPageViewModel : BaseViewModel
    {
        #region Private members
        private Visibility copyFieldVisibility = Visibility.Collapsed;
        private Visibility attachementVisibility = Visibility.Collapsed;
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

        public Visibility AttachementVisibility
        {
            get => attachementVisibility;
            set
            {
                attachementVisibility = value;
                OnPropertyChanged(nameof(attachementVisibility));
            }
        }

        public ObservableCollection<AttachementsFacts> AttachementsList { get; set; }

        #endregion

        #region Commands
        public ICommand DisplayCopyFieldsCommand { get; set; }
        public ICommand AddAttachementCommand { get; set; }
        public ICommand RemoveAttachementCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// The default constructor
        /// </summary>
        public SendingPageViewModel()
        {
            AttachementsList = new ObservableCollection<AttachementsFacts>();
            DisplayCopyFieldsCommand = new RelayCommand(() => SetCopyVisibility());
            AddAttachementCommand = new RelayCommand(() => AddAttachement());
            RemoveAttachementCommand = new RelayParameterizedCommand(param => RemoveAttachement(param));
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

                    var file = fileToLoad.FileNames[i];

                    AttachementsList.Add(new AttachementsFacts() {
                        FileFullPath = file,
                        FileName = Utils.GetFileNameFromPath(file),
                        FileSize = new FileInfo(file).Length 
                    });
                }

                CheckIfttachementIsEmpty();
            }
        }

        private void RemoveAttachement(object param)
        {
            AttachementsList.Remove(param as AttachementsFacts);
            CheckIfttachementIsEmpty();
        }

        private void CheckIfttachementIsEmpty()
        {
            if (AttachementsList.Count != 0) {
                AttachementVisibility = Visibility.Visible;
            } else {
                AttachementVisibility = Visibility.Collapsed;
            }
        }
        #endregion
    }
}
