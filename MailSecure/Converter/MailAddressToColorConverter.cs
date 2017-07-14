using System;
using System.Globalization;
using System.Net.Mail;

namespace MailSecure
{
    class MailAddressToColorConverter : BaseConverter<MailAddressToColorConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var from = (MailAddress)value;
            char letter = from.User[0];

            if(letter >= 97 && letter <= 102) {
                return App.Current.FindResource("InitialsAToFBrush");
            }

            if (letter > 102 && letter <= 110) {
                return App.Current.FindResource("InitialsGToNBrush");
            }

            return App.Current.FindResource("InitialsOToZBrush");

        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
