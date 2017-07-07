using System;
using System.Globalization;
using System.Windows;

namespace MailSecure
{
    class VisibilityToColorConverter : BaseConverter<VisibilityToColorConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((Visibility)value == Visibility.Visible) {
                return App.Current.FindResource("SecondaryBrush");
            }

            return App.Current.FindResource("BackgroundBrush");
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
