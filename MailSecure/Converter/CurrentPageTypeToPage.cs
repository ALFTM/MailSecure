using System;
using System.Globalization;

namespace MailSecure
{
    class CurrentPageTypeToPage : BaseConverter<CurrentPageTypeToPage>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((PageType)value == PageType.SendingPage)
            {
                return "pack://application:,,,/Pages/SendingPage.xaml";
            }

            if ((PageType)value == PageType.UnlockPage)
            {
                return "pack://application:,,,/Pages/SendingPage.xaml";
            }

            if ((PageType)value == PageType.SettingPage)
            {
                return "pack://application:,,,/Pages/Settings.xaml";
            }

            return "pack://application:,,,/Pages/SendingPage.xaml";
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
