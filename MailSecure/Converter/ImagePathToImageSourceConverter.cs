using System;
using System.Globalization;
using MailSecure.Core;
using System.Windows.Media;

namespace MailSecure
{
    class ImagePathToImageSourceConverter : BaseConverter<ImagePathToImageSourceConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string packUri = value.ToString();
            ImageSource Source = new ImageSourceConverter().ConvertFromString(packUri) as ImageSource;
            return Source;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
