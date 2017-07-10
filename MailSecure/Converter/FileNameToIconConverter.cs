using System;
using System.Globalization;
using MailSecure.Core;

namespace MailSecure
{
    class FileNameToIconConverter : BaseConverter<FileNameToIconConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string fileName = (string)value;

            return IconManager.FindIconForFilename(fileName, true);   
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
