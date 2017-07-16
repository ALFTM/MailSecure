using System;
using System.Globalization;

namespace MailSecure
{
    class BoolToInvertBoolConverter : BaseConverter<BoolToInvertBoolConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((Boolean)value == true) {
                return false;
            }

            return true;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
