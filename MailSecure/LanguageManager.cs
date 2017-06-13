using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace MailSecure
{
    public class LanguageManager
    {
        private static LanguageManager instance = null;
        public static string language { get; set; }
        private ResourceManager resManag;
        private CultureInfo ci;

        private LanguageManager() { }

        public static LanguageManager GetInstance
        {
            get
            {
                if (null == instance)
                {
                    instance = new LanguageManager();
                    language = "fr";
                }
                return instance;
            }
        }

        public void SwitchLanguage(string newLanguage)
        {
            switch (newLanguage)
            {
                case "fr":
                    ci = CultureInfo.CreateSpecificCulture("fr");
                    language = "fr";
                    break;
                case "en":
                    ci = CultureInfo.CreateSpecificCulture("en");
                    language = "en";
                    break;
            }
        }

        public string GetStringFromLanguage(string libelle)
        {
            if(null == resManag)
            {
                resManag = new ResourceManager("MailSecure.Resources.Global", typeof(LanguageManager).Assembly);
            }
            return resManag.GetString(libelle, ci);
        }
    }
}
