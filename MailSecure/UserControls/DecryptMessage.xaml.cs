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
        private string fullPath;

        public DecryptMessage()
        {
            InitializeComponent();
        }

        private void decryptMessageButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(passwordTextBox.Text))
            {
                MessageBox.Show("Entrer le mot de passe.");
                return;
            }

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

                fileEncryption.DecryptFile(fullPath, destPath, passwordTextBox.Text);
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
                fullPath = fileToLoad.FileName;
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
                //fileDecrypted.CopyTo(fs);
            }
        }
    }
}
