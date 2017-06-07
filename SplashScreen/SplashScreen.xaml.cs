using System.Windows;
using System.Timers;
using System;
using System.Threading.Tasks;

namespace MailSecure.SplashScreen
{
    /// <summary>
    /// Logique d'interaction pour SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        public void SetProgress(double value) {
            ProgressBar.IsIndeterminate = false;
            ProgressBar.Value = value;
        }

        public void SetProgress(double value, double maximum) {
            ProgressBar.IsIndeterminate = false;
            ProgressBar.Value = value;
            ProgressBar.Maximum = maximum;
        }
    }
}
