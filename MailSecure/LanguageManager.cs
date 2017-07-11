using System.Collections.Generic;
using System.Globalization;
using System.Resources;

namespace MailSecure
{
    public class LanguageManager
    {
        #region Private Property
        private static LanguageManager instance = null;
        private ResourceManager resManag;
        private CultureInfo ci;
        #endregion

        #region Public Property
        public static string Language { get; private set; }
        #endregion

        #region Constructor
        private LanguageManager() { }
        #endregion

        #region Methods public static
        public static LanguageManager GetInstance
        {
            get
            {
                if (null == instance) {
                    instance = new LanguageManager();
                    Language = "fr";
                }
                return instance;
            }
        }
        #endregion

        #region Methods public
        public void SwitchLanguage(string newLanguage)
        {
            switch (newLanguage) {
                case "FR":
                    ci = CultureInfo.CreateSpecificCulture("fr");
                    Language = "FR";
                    break;
                case "EN":
                    ci = CultureInfo.CreateSpecificCulture("en");
                    Language = "EN";
                    break;
            }
        }

        public string GetStringFromLanguage(string libelle)
        {
            if (null == resManag) {
                resManag = new ResourceManager("MailSecure.Resources.Global", typeof(LanguageManager).Assembly);
            }
            return resManag.GetString(libelle, ci);
        }

        public List<string> GetLanguages()
        {
            return new List<string>() { "FR", "EN" };
        }
        #endregion
    }
}