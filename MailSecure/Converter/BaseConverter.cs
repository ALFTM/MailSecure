using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace MailSecure
{
     public abstract class BaseConverter<T> : MarkupExtension, IValueConverter
         where T : class, new()
    {
        #region Private Member

        /// <summary>
        /// Static instance
        /// </summary>
        private static T mConverter = null;

        #endregion

        #region Markup Extension Methods

        /// <summary>
        /// Retourne une instance statique de ce convertiseur de valeur
        /// </summary>
        /// <param name="serviceProvider">The service provider</param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return mConverter ?? (mConverter = new T());
        }

        #endregion

        #region Value Converter Methods

        /// <summary>
        /// La meéthode pour convertir une valeur en une autre
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// Méthode pour convertir une valeur à sa source d'origine
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        #endregion

    }
}
