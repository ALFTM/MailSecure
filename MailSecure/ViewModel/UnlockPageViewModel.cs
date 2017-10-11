using System.Collections.ObjectModel;
using System.Net.Mail;
using System.Windows.Input;
using MailSecure.Core;
using MailSecure.Security;
using MailSecure.FormatConverter;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.IO;
using System;

namespace MailSecure
{
    public class UnlockPageViewModel : BaseViewModel
    {
        #region Private Members
        private MailMessage selectedMessage;
        private MailMessage previousMessage;
        private string password;
        private Visibility loadingIsVisible;
        private Visibility imapListIsVisible;
        private Visibility unlockControlVisibility;
        private Visibility noMessageFound;
        #endregion

        #region Public Members
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(password));
            }
        }
        public ObservableCollection<MailMessage> ImapList { get; set; }
        public Visibility LoadingIsVisible
        {
            get => loadingIsVisible;
            set
            {
                loadingIsVisible = value;
                OnPropertyChanged(nameof(loadingIsVisible));
            }
        }
        public Visibility ImapListIsVisible
        {
            get => imapListIsVisible;
            set
            {
                imapListIsVisible = value;
                OnPropertyChanged(nameof(imapListIsVisible));
            }
        }
        public Visibility UnlockControlVisibility
        {
            get => unlockControlVisibility;
            set
            {
                unlockControlVisibility = value;
                OnPropertyChanged(nameof(unlockControlVisibility));
            }
        }
        public Visibility NoMessageFound
        {
            get => noMessageFound;
            set
            {
                noMessageFound = value;
                OnPropertyChanged(nameof(noMessageFound));
            }
        }
        public MailMessage SelectedMessage
        {
            get => selectedMessage;
            set
            {
                selectedMessage = value;
                OnPropertyChanged(nameof(selectedMessage));
            }
        }
        public UnlockControl Control { get; set; }
        #endregion

        #region Content Language
        public string DisplayMessageText
        {
            get => App.ApplicationLanguage.GetStringFromLanguage("displayMessage_lbl");
        }
        public string FileSavedAtText
        {
            get => App.ApplicationLanguage.GetStringFromLanguage("fileSavedAt_lbl");
        }

        public string SaveAttachmentsText
        {
            get => App.ApplicationLanguage.GetStringFromLanguage("saveAttchments_lbl");
        }

        public string NoMessageText
        {
            get => App.ApplicationLanguage.GetStringFromLanguage("noMessage_lbl");
        }

        public string WrongpasswordText
        {
            get => App.ApplicationLanguage.GetStringFromLanguage("wrongPassword_lbl");
        }
        #endregion

        #region Commands
        public ICommand SaveAttchmentsCommand { get; set; }
        public ICommand DisplayMessageCommand { get; set; }

        public ICommand ManageVisibilityCommand { get; set; }
        #endregion

        #region Constructor
        public UnlockPageViewModel()
        {
            LoadingIsVisible = Visibility.Visible;
            ImapListIsVisible = Visibility.Collapsed;
            UnlockControlVisibility = Visibility.Collapsed;
            NoMessageFound = Visibility.Collapsed;
            DisplayMessageCommand = new RelayCommand(() => DisplayMessage());
            SaveAttchmentsCommand = new RelayCommand(() => SaveAttachment());
            ManageVisibilityCommand = new RelayCommand(() => ManageControlVisibility());
            ImapList = new ObservableCollection<MailMessage>();
            FullFillMessageList(App.CurrentUserData.CurrentUser);
        }
        #endregion

        #region Private Methods
        private async void FullFillMessageList(UserMailFacts crtUser)
        {
            var mailReceiver = new MailReceiver(crtUser);
            mailReceiver.PrepareImap();

            var listMessage = await Task.Run(() => mailReceiver.GetMessagesHeader());

            if (!listMessage.IsAny())
            {
                NoMessageFound = Visibility.Visible;
            }
            else
            {
                foreach (MailMessage mailMessage in listMessage)
                {
                    var from = mailMessage.From;

                    ImapList.Add(mailMessage);
                }
                ImapListIsVisible = Visibility.Visible;
            }
            LoadingIsVisible = Visibility.Collapsed;
        }

        private void DisplayMessage()
        {
            string cryptedMessage = SelectedMessage.Body;
            var html = string.Empty;
            try
            {
                html = Encryption.Decrypt(cryptedMessage, Password);
            }
            catch (Exception)
            {
                System.Windows.MessageBox.Show(WrongpasswordText);
                return;
            }
            string xaml = HtmlToXamlConverter.ConvertHtmlToXaml(html, true);

            Control.rtb_content.Document = XamlReader.Parse(xaml) as FlowDocument;
        }

        private void SaveAttachment()
        {
            string folderPath;
            var dialog = new FolderBrowserDialog();
            if(dialog.ShowDialog() == DialogResult.OK) {
                folderPath = dialog.SelectedPath;
                SaveFiles();
                UnlockAttachments(folderPath);
            }
            
        }

        private void SaveFiles()
        {
            if (!DirectoryManager.CheckIfTempFolderExist()) {
                DirectoryManager.CreateTempFolder();
            }
            DirectoryManager.ClearTempFolder();

            foreach (Attachment attachment in SelectedMessage.Attachments) {
                byte[] allBytes = new byte[attachment.ContentStream.Length];
                int bytesRead = attachment.ContentStream.Read(allBytes, 0, (int)attachment.ContentStream.Length);
                string destinationFile = DirectoryManager.tempfolderPath + attachment.Name;
                BinaryWriter writer = new BinaryWriter(new FileStream(destinationFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None));
                writer.Write(allBytes);
                writer.Close();
            }
        }

        private void UnlockAttachments(string dest)
        {
            IFileEncryption decryptor = new FileEncryptionCBC();
            foreach (Attachment attachment in SelectedMessage.Attachments) {
                string source = DirectoryManager.tempfolderPath + attachment.Name;
                string resultFileName = attachment.Name.Replace(".lock", "");
                string destFile = dest + "\\" + resultFileName;
                
                decryptor.DecryptFile(source, destFile, Password);
            }

            DirectoryManager.ClearTempFolder();
            System.Windows.MessageBox.Show(FileSavedAtText + dest);
        }

        private void ManageControlVisibility()
        {
            System.Console.WriteLine("Trigger Event");
            if (previousMessage == null) {
                UnlockControlVisibility = Visibility.Visible;
            }
            else if (previousMessage == SelectedMessage) {
                UnlockControlVisibility = UnlockControlVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            } else {
                UnlockControlVisibility = Visibility.Visible;
            }

            previousMessage = SelectedMessage;  
        }

        
        #endregion
    }


}
