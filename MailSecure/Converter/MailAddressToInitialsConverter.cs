using System;
using System.Globalization;
using System.Net.Mail;

namespace MailSecure
{
    class MailAddressToInitialsConverter : BaseConverter<MailAddressToInitialsConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var from = (MailAddress)value;

            if(from.DisplayName.Length != 0) {
                return from.DisplayName;
            }

            return from.User.Substring(0, 1).ToUpper();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
