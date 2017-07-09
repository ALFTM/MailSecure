using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSecure.Core;

namespace MailSecure
{
    class ContextViewModel : BaseViewModel
    {
        #region Private members
        private PageType currentPage = PageType.SettingPage;
        #endregion

        #region Public Members
        public PageType CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                OnPropertyChanged(nameof(currentPage));
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// The default constructor
        /// </summary>
        public ContextViewModel()
        {

        }
        #endregion
    }
}
