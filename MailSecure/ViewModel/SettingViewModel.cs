using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSecure.Core;
using System.Windows.Input;

namespace MailSecure
{
    class SettingViewModel : BaseViewModel
    {
        #region Commands
        public ICommand OpenAccountWindowCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// The default constructor
        /// </summary>
        public SettingViewModel()
        {
            OpenAccountWindowCommand = new RelayCommand(() => OpenSettingWindow());
        }
        #endregion

        #region Methods
        private void OpenSettingWindow()
        {
            MailServerConfigurationWindow window = new MailServerConfigurationWindow();
            window.ShowDialog();
        }
        #endregion
    }
}
