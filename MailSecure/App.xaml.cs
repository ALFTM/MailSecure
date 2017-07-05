﻿using System.Threading.Tasks;
using System.Windows;
using MailSecure.Core;

namespace MailSecure
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MailSender mailSender { get; set; }
        public static UserDataContext CurrentUserData { get; set; }

        private SplashScreen.SplashScreen splashScreen;

        public App()
        {
            splashScreen = new SplashScreen.SplashScreen();
            splashScreen.Show();
        }

        public void getCurrentUser()
        {
            CurrentUserData.CurrentUser = BinaryMCSFileManager.ReadStructInFile();
            CurrentUserData.DisplayedName = App.CurrentUserData.CurrentUser.userName;
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Longest loading
            await LongLoading();


            MainWindow = new MainWindow();
            FlatWindow FlatWindow = new FlatWindow();
            // Shortest loading

            if (!await this.ShortLoading()) {
               // MainWindow.Show();
                FlatWindow.Show();
                MailServerConfigurationWindow mailConfigurationWindow = new MailServerConfigurationWindow();
                mailConfigurationWindow.Show();
            }
            else {
                //MainWindow.Show();
                FlatWindow.Show();
            }

            splashScreen.Close();
            splashScreen = null;
        }

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
            if(BinaryMCSFileManager.IsFileEmpty()) {
                canGetUser = false;
            }

            await Task.Delay(500);

            splashScreen.SetProgress(2);
            mailSender = new MailSender(ServerFactConst.DEFAULT_SMTP_PORT_TLS);
            CurrentUserData = new UserDataContext();
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
    }
}
