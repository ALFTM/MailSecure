using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using MailSecure.Core;
using MailSecure.Security;


namespace MailSecure
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Public Property
        public static MailSender mailSender { get; set; }
        public static UserDataContext CurrentUserData { get; set; }
        public static LanguageManager ApplicationLanguage { get; private set; }
        public static SendingPageViewModel SendingPageViewModel { get; set; }
        public static UnlockPageViewModel UnlockPageViewModel { get; set; }
        public static NotificationSystem NotificationHelper { get; set; }
        public static Window BaseWindow { get; set; }
        
        #endregion

        #region Private Property
        private SplashScreen.SplashScreen splashScreen;
        #endregion

        #region Constructor
        public App()
        {
            ApplicationLanguage = LanguageManager.GetInstance;
            NotificationHelper = new NotificationSystem();
            splashScreen = new SplashScreen.SplashScreen();
            splashScreen.Show();
        }
        #endregion

        #region Public Methods
        public void getCurrentUser()
        {
            CurrentUserData.CurrentUser = BinaryMCSFileManager.ReadStructInFile();
            CurrentUserData.DisplayedName = App.CurrentUserData.CurrentUser.UserName;
        }
        #endregion

        #region Protected Methods
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Longest loading
            await LongLoading();

            BaseWindow = MainWindow;
            // Shortest loading

            if (!await this.ShortLoading()) {
                MainWindow.Show();
                AddUserWindow mailConfigurationWindow = new AddUserWindow();
                mailConfigurationWindow.Show();
            }
            else {
                LoginWindow loginWindow = new LoginWindow();
                splashScreen.Close();
                splashScreen = null;
                loginWindow.Show();
                Debug();
            }


        }
        #endregion

        #region Private Methods
        private async Task LongLoading()
        {
            bool canGetUser = true;
            splashScreen.SetProgress(0, 4);
            if (!BinaryMCSFileManager.CheckIfFolderExist()) {
                canGetUser = false;
                splashScreen.SetProgress(0.5);
                BinaryMCSFileManager.CreateConfigFolder();
                await Task.Delay(200);
            }

            splashScreen.SetProgress(1, 4);
            if (!BinaryMCSFileManager.CheckIfConfigFileExist()) {
                canGetUser = false;
                splashScreen.SetProgress(1.5);
                BinaryMCSFileManager.CreateConfigFile();
                await Task.Delay(200);
            }

            splashScreen.SetProgress(1.5, 4);
            if (BinaryMCSFileManager.IsFileEmpty()) {
                canGetUser = false;
            }

            await Task.Delay(500);

            splashScreen.SetProgress(2);
            mailSender = new MailSender(ServerFactConst.DEFAULT_SMTP_PORT_TLS);
            CurrentUserData = new UserDataContext();
            await Task.Delay(500);

            splashScreen.SetProgress(2.5);
            InitDatabase();
            await Task.Delay(500);

            splashScreen.SetProgress(3);
            if (canGetUser) {
                getCurrentUser();
            }

            await Task.Delay(500);

        }

        private async Task<bool> ShortLoading()
        {
            splashScreen.SetProgress(4);

            if (null == CurrentUserData.DisplayedName) {
                await Task.Delay(500);
                return false;
            }

            return true;
        }

        private void InitDatabase()
        {
            SQLiteHelper helper = new SQLiteHelper();

            if (!helper.CheckIfDataBaseExist()) {
                Console.WriteLine("CREATE DATABASE");
                helper.CreateFile();
                helper.InitTables();
                NotificationHelper.DisplayNotification("Database created");
            }

        }

        private void Debug()
        {
            SQLiteHelper helper = new SQLiteHelper();
            List<DataBaseUser> users = helper.ReadAll();

            users.ForEach((User) => {
                System.Console.WriteLine(User.Name);
            });
        }
        #endregion
    }
}