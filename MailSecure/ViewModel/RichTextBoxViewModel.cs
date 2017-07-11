using MailSecure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSecure
{
    class RichTextBoxViewModel : BaseViewModel
    {
        #region Content Language
        public string SendLbl { get { return App.ApplicationLanguage.GetStringFromLanguage("send_lbl"); } }
        #endregion 

        #region Constructor
        public RichTextBoxViewModel()
        {

        }
        #endregion
    }
}
