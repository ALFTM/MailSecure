using MailSecure.Core;
using MailSecure.Security;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Mail;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace MailSecure
{
    public class SendingPageViewModel : BaseViewModel
    {
        #region Private members
        private Visibility copyFieldVisibility = Visibility.Collapsed;
        private Visibility attachementVisibility = Visibility.Collapsed;
        private string to;
        private string cc = "";
        private string cci = "";
        private string messageObject = "";
        #endregion

        #region Public members
        public RichTextBoxControl RichTextBoxControler { get; set; }
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

        public string To
        {
            get => to;
            set
            {
                to = value;
                OnPropertyChanged(nameof(to));
            }
        }

        public string Cc
        {
            get => cc;
            set
            {
                cc = value;
                OnPropertyChanged(nameof(cc));
            }
        }

        public string Cci
        {
            get => cci;
            set
            {
                cci = value;
                OnPropertyChanged(nameof(cci));
            }
        }
        public string MessageObject
        {
            get => messageObject;
            set
            {
                messageObject = value;
                OnPropertyChanged(nameof(messageObject));
            }
        }

        public ObservableCollection<AttachementsFacts> AttachementsList { get; set; }

        #endregion

        #region Commands
        public ICommand DisplayCopyFieldsCommand { get; set; }
        public ICommand AddAttachementCommand { get; set; }
        public ICommand RemoveAttachementCommand { get; set; }
        public ICommand SendMailCommand { get; set; }
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
            SendMailCommand = new RelayCommand(() => SendMessage());
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

        private void SendMessage()
        {
            MailMessage mail = new MailMessage(App.CurrentUserData.CurrentUser.login, To) {
                Subject = MessageObject
            };

            var richTextBox = RichTextBoxControler.rtbEditor;
            string richText = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd).Text;

            System.Console.WriteLine("Body : " + richText);

            mail.Body = richText;

            System.Console.WriteLine("Object : " + MessageObject);

            if (Cc.Length != 0) {
                mail.CC.Add(Cc);
            }

            if(Cci.Length != 0) {
                mail.Bcc.Add(Cci);
            }
            

            for(int i  = 0; i < AttachementsList.Count; i++) {
                var file = AttachementsList[i].FileFullPath;
                Attachment item = new Attachment(file);
                mail.Attachments.Add(item);
            }

            App.mailSender.setMailMessage(mail);
            App.mailSender.setCurrentUser(App.CurrentUserData.CurrentUser);

            App.mailSender.SendMail();

            ClearFields();
        }

        private void ClearFields()
        {
            To = "";
            Cc = "";
            Cci = "";
            MessageObject = "";
            AttachementsList.Clear();

        }

        #endregion
    }
}
