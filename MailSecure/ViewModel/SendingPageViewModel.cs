using MailSecure.Core;
using MailSecure.FormatConverter;
using MailSecure.Security;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Mail;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

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

        #region Content Language
        public string SendLbl { get { return App.ApplicationLanguage.GetStringFromLanguage("send_lbl"); } }
        public string FromLbl { get { return App.ApplicationLanguage.GetStringFromLanguage("from_lbl"); } }
        public string ToLbl { get { return App.ApplicationLanguage.GetStringFromLanguage("to_lbl"); } }
        public string ObjectLbl { get { return App.ApplicationLanguage.GetStringFromLanguage("object_lbl"); } }
        public string CcLbl { get { return App.ApplicationLanguage.GetStringFromLanguage("carbonCopy_lbl"); } }
        public string BccLbl { get { return App.ApplicationLanguage.GetStringFromLanguage("blindCarbonCopy_lbl"); } }
        public string CcAndBccLbl { get { return App.ApplicationLanguage.GetStringFromLanguage("ccAndBcc_lbl"); } }
        public string AttachementLbl { get { return App.ApplicationLanguage.GetStringFromLanguage("attachement_lbl"); } }
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
            string password = Utils.RandomPassword(8);

            using (var mail = PrepareMessage(password))
            {
                App.mailSender.setMailMessage(mail);
                App.mailSender.setCurrentUser(App.CurrentUserData.CurrentUser);
                App.mailSender.SendMail();
            }        

            DisplayPassWordBox(password);

            ClearFields();
        }

        public MailMessage PrepareMessage(string password)
        {
            var user = App.CurrentUserData.CurrentUser.EmailAdress;
            var body = GetHtmlStringFromXaml(GetXamlString());
            

            MailMessage mail = MailPreparator.GetEncryptedMail(body, password, GetFullPathArray());
            mail.Subject = MessageObject + " [Locked]";

            MailAddress from = new MailAddress(user);
            mail.From = from;

            AddRecipient(ref mail);

            // mail.IsBodyHtml = true;
            
            AddCcAndCciInMail(ref mail);

            return mail;

        }

        private void AddRecipient(ref MailMessage mail)
        {
            foreach(var address in To.Split(new [] {";"}, StringSplitOptions.RemoveEmptyEntries)) {
                mail.To.Add(address);
            }
        }

        private void AddCcAndCciInMail(ref MailMessage mail)
        {
            if (Cc.Length != 0) {
                foreach (var address in Cc.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries)) {
                    mail.CC.Add(address);
                }
            }

            if (Cci.Length != 0) {
                foreach (var address in Cci.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries)) {
                    mail.Bcc.Add(address);
                }
            }
        }

        private Collection<string> GetFullPathArray()
        {
            Collection<string> path = new Collection<string>();

            for(int i = 0; i < AttachementsList.Count; i++) {
                var item = AttachementsList[i].FileFullPath;
                path.Add(item);
            }

            return path;
        }

        private string GetXamlString()
        {
            var richTextBox = RichTextBoxControler.rtbEditor;
            return XamlWriter.Save(richTextBox.Document);
        }

        private string GetHtmlStringFromXaml(string xamlString)
        {
            return HtmlFromXamlConverter.ConvertXamlToHtml(xamlString);
        }

        private void ClearFields()
        {
            To = "";
            Cc = "";
            Cci = "";
            MessageObject = "";
            AttachementsList.Clear();
            RichTextBoxControler.rtbEditor.Document.Blocks.Clear();
            DirectoryManager.ClearTempFolder();
        }

        private void DisplayPassWordBox(string password)
        {
            var passwordPopup = new PasswordPopup();
            passwordPopup.passwordGeneratedLabel.Content = "Password généré : " + password;
            passwordPopup.Show();
        }

        #endregion
    }
}
