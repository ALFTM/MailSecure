using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace MailSecure
{
    /// <summary>
    /// Logique d'interaction pour RichTextBoxControl.xaml
    /// </summary>
    public partial class RichTextBoxControl : UserControl
    {
        public RichTextBoxControl()
        {
            InitializeComponent();
        }

        private void rtbEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            
            object temp = rtbEditor.Selection.GetPropertyValue(Inline.FontWeightProperty);
            btnBold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));
            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontStyleProperty);
            btnItalic.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));
            temp = rtbEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            btnUnderline.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));

            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            //cmbFontFamily.SelectedItem = temp;
            temp = rtbEditor.Selection.GetPropertyValue(Inline.FontSizeProperty);
            //cmbFontSize.Text = temp.ToString();
        }
    }
}
