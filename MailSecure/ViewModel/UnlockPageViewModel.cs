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

namespace MailSecure
{
    public class UnlockPageViewModel : BaseViewModel
    {
        #region Private Members
        private MailMessage selectedMessage;
        private string password;
        private Visibility loadingIsVisible;
        private Visibility imapListIsVisible;
        private Visibility unlockControlVisibility;
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

        #region Commands
        public ICommand SaveAttchmentsCommand { get; set; }
        public ICommand DisplayMessageCommand { get; set; }
        #endregion

        #region Constructor
        public UnlockPageViewModel()
        {
            LoadingIsVisible = Visibility.Visible;
            ImapListIsVisible = Visibility.Collapsed;
            UnlockControlVisibility = Visibility.Collapsed;
            DisplayMessageCommand = new RelayCommand(() => DisplayMessage());
            SaveAttchmentsCommand = new RelayCommand(() => SaveAttachment());
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

            foreach (MailMessage mailMessage in listMessage) {
                var from = mailMessage.From;

                ImapList.Add(mailMessage);
            }
            LoadingIsVisible = Visibility.Collapsed;
            ImapListIsVisible = Visibility.Visible;
            UnlockControlVisibility = Visibility.Visible;
        }

        private void DisplayMessage()
        {
            string cryptedMessage = SelectedMessage.Body;
            string html = Encryption.Decrypt(cryptedMessage, Password);
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
            FileEncryption decryptor = new FileEncryption();
            foreach (Attachment attachment in SelectedMessage.Attachments) {
                string source = DirectoryManager.tempfolderPath + attachment.Name;
                string resultFileName = attachment.Name.Replace(".lock", "");
                string destFile = dest + "\\" + resultFileName;

                decryptor.DecryptFile(source, destFile, Password);
            }

            DirectoryManager.ClearTempFolder();
            System.Windows.MessageBox.Show("File saved at " + dest);
        }

        
        #endregion
    }


}
