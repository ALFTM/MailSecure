using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSecure.Core;

namespace MailSecure
{
    class PasswordPopupViewModel : BaseViewModel
    {
        #region ContentLanguage
        public string PasswordPopupTitleLbl
        {
            get => App.ApplicationLanguage.GetStringFromLanguage("passwordPopupTitle_lbl");
        }

        public string CopyLbl
        {
            get => App.ApplicationLanguage.GetStringFromLanguage("copy_lbl");
        }
        #endregion

        #region Constructor
        public PasswordPopupViewModel()
        {

        }
        #endregion
    }
}