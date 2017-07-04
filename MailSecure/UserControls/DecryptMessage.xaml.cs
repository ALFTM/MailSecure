using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using MailSecure.Security;

namespace MailSecure.UserControls
{
    /// <summary>
    /// Logique d'interaction pour DecryptMessage.xaml
    /// </summary>
    public partial class DecryptMessage : UserControl
    {
        private Stream fileToDecrypt;
        private Stream fileDecrypted;

        public DecryptMessage()
        {
            InitializeComponent();
        }

        private void decryptMessageButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(messageCryptedTextBox.Text))
            {
                messageDecryptedTextBox.Text = Encryption.Decrypt(messageCryptedTextBox.Text, passwordTextBox.Text);
            }
            
            if (fileToDecrypt != null)
            {
                string destPath = "";
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "All files(*.*)|(*.*)";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                if(saveFileDialog.ShowDialog() == true)
                {
                    destPath = saveFileDialog.FileName;
                }

                FileEncryption fileEncryption = new FileEncryption();

                fileEncryption.DecryptFile(filesLabel.Content.ToString(), destPath, passwordTextBox.Text);
            }
        }

        private void findFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileToLoad = new OpenFileDialog();
            fileToLoad.Filter = "All Files (*.*)|*.*";
            fileToLoad.FilterIndex = 1;
            fileToLoad.Multiselect = false;

            if (fileToLoad.ShowDialog() == true)
            {
                fileToDecrypt = new FileStream(fileToLoad.FileName, FileMode.Open, FileAccess.Read);
                filesLabel.Content = Utils.GetFileNameFromPath(fileToLoad.FileName);
            }
        }

        private void saveFileButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "All files (*.*)|(*.*)";
            saveFileDialog.Title = "Save File";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                FileStream fs = (FileStream) saveFileDialog.OpenFile();
                fileDecrypted.CopyTo(fs);
            }
        }
    }
}
