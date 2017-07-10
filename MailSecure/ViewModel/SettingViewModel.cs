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
        private LanguageManager settingLanguageManager;
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
                OnPropertyChanged(nameof(myLanguageSelected));
                settingLanguageManager.SwitchLanguage(myLanguageSelected);
            }
        }
        #endregion

        #region Content Language
        public string AddAccount { get { return settingLanguageManager.GetStringFromLanguage("optionLanguage_lbl"); } }
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
            settingLanguageManager = LanguageManager.GetInstance;
            AddLanguage();
            OpenAccountWindowCommand = new RelayCommand(() => OpenSettingWindow());
        }
        #endregion

        #region Private Methods
        private void AddLanguage()
        {
            LanguageItems = new ObservableCollection<string>();
            settingLanguageManager.GetLanguages().ForEach(str => LanguageItems.Add(str));
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
