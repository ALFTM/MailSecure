using System;
using System.Globalization;

namespace MailSecure
{
    class CurrentPageTypeToColor : BaseConverter<CurrentPageTypeToColor>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((PageType)value == PageType.SendingPage && (string)parameter == "send") {
                return App.Current.FindResource("SecondaryBrush");
            }

            if ((PageType)value == PageType.UnlockPage && (string)parameter == "unlock") {
                return App.Current.FindResource("SecondaryBrush");
            }

            if ((PageType)value == PageType.SettingPage && (string)parameter == "setting") {
                return App.Current.FindResource("SecondaryBrush");
            }

            return App.Current.FindResource("PrimaryDarkBrush");
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
