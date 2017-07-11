using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSecure.Core;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace MailSecure
{
    class SettingViewModel : BaseViewModel
    {
        #region Private
        private string myLanguageSelected;
        #endregion

        #region Binding
        public ObservableCollection<string> LanguageItems { get; set; }
        public string LanguageSelected
        {
            get { return myLanguageSelected; }
            set
            {
                myLanguageSelected = value;
                App.ApplicationLanguage.SwitchLanguage(myLanguageSelected);
                OnPropertyChanged(nameof(LanguageSelected));                
            }
        }
        #endregion

        #region Content Language
        public string AddAccount { get { return App.ApplicationLanguage.GetStringFromLanguage("addAcount_lbl"); } }
        public string Setting { get { return App.ApplicationLanguage.GetStringFromLanguage("settingMenu_lbl"); } }
        #endregion

        #region Commands
        public ICommand OpenAccountWindowCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// The default constructor
        /// </summary>
        public SettingViewModel()
        {
            AddLanguage();
            OpenAccountWindowCommand = new RelayCommand(() => OpenSettingWindow());
        }
        #endregion

        #region Private Methods
        private void AddLanguage()
        {
            LanguageItems = new ObservableCollection<string>();
            App.ApplicationLanguage.GetLanguages().ForEach(str => LanguageItems.Add(str));
            LanguageSelected = LanguageManager.Language;
        }

        private void OpenSettingWindow()
        {
            MailServerConfigurationWindow window = new MailServerConfigurationWindow();
            window.ShowDialog();
        }
        #endregion
    }
}
