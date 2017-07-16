using System;
using System.Globalization;

namespace MailSecure
{
    class StringToBoolConverter : BaseConverter<StringToBoolConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null) {
                return false;
            }

            string content = (string)value;
            if (content.Length == 0) {
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
