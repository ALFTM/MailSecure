using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MailSecure.UserControls
{
    /// <summary>
    /// Logique d'interaction pour Option.xaml
    /// </summary>
    public partial class Option : UserControl
    {
        private LanguageManager optionLanguageManager;
        public string LanguageLabel { get { return optionLanguageManager.GetStringFromLanguage("optionLanguage_lbl"); } }
        public string CancelButton { get { return optionLanguageManager.GetStringFromLanguage("optionCancel_lbl"); } }
        public string OkButton { get { return optionLanguageManager.GetStringFromLanguage("optionOk_lbl"); } }
        public Option()
        {
            optionLanguageManager = LanguageManager.GetInstance;
            InitializeComponent();
            DataContext = this;

            AddLanguageToComboBox();
        }

        private void AddLanguageToComboBox()
        {
            Language_comboBox.Items.Clear();

            // Language FR
            var comboBoxItemFR = new ComboBoxItem();
            comboBoxItemFR.Content = "fr";

            // Language EN
            var comboBoxItemEN = new ComboBoxItem();
            comboBoxItemEN.Content = "en";

            // Add language to comboBox
            Language_comboBox.Items.Add(comboBoxItemFR);
            Language_comboBox.Items.Add(comboBoxItemEN);

            // Select current language
            for (int i = 0; i < Language_comboBox.Items.Count; i++)
            {
                if (LanguageManager.language.Equals(((ComboBoxItem)Language_comboBox.Items[i]).Content.ToString()))
                {
                    Language_comboBox.SelectedIndex = i;
                }
            }
        }

        private void ok_button_Click(object sender, RoutedEventArgs e)
        {
            var newLanguage = ((ComboBoxItem)Language_comboBox.SelectedItem).Content.ToString();
            if (!newLanguage.Equals(LanguageManager.language))
            {
                optionLanguageManager.SwitchLanguage(newLanguage);
                RefreshContent();
            }
        }
        private void RefreshContent()
        {
            Language.GetBindingExpression(Label.ContentProperty).UpdateTarget();
            cancel_button.GetBindingExpression(Button.ContentProperty).UpdateTarget();
        }
    }
}
