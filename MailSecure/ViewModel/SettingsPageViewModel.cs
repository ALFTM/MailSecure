using MailSecure.Core;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MailSecure
{
    class SettingsPageViewModel : BaseViewModel
    {
        #region Private Members
        private string myLanguageSelected;
        #endregion

        #region Public Members
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

        public string AddAccount { get { return App.ApplicationLanguage.GetStringFromLanguage("addAcount_lbl"); } }
        public string Setting { get { return App.ApplicationLanguage.GetStringFromLanguage("settingMenu_lbl"); } }
        #endregion

        #region Commands
        public ICommand AddNewUserCommand { get; set; }
        public ICommand OpenAccountWindowCommand { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// The default constructor
        /// </summary>
        public SettingsPageViewModel()
        {
            AddLanguage();
            OpenAccountWindowCommand = new RelayCommand(() => OpenSettingWindow());
            AddNewUserCommand = new RelayCommand(() => StartAddNewUserWindow());
        }
        #endregion

        #region Helper
        private void StartAddNewUserWindow()
        {
            AddUserWindow window = new AddUserWindow();
            window.ShowDialog();
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
