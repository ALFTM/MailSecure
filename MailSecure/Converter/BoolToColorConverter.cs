using System;
using System.Globalization;

namespace MailSecure
{
    class BoolToColorConverter : BaseConverter<BoolToColorConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((Boolean)value == true) {
                return App.Current.FindResource("SecondaryDarkBrush");
            }

            return App.Current.FindResource("BackgroundBrush");
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
