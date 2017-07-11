using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailSecure.Core;
using System.Security;

namespace MailSecure
{
    class ServerConfigurationViewModel : BaseViewModel
    {

        #region Public Members
        public string Title
        {
            get => title;
            set
            {
                title = baseTitle + value;
                OnPropertyChanged(nameof(title));
            }
        }

        public string SmtpAddress
        {
            get => smtpAddress;
            set
            {
                smtpAddress = value;
                OnPropertyChanged(nameof(smtpAddress));
            }
        }

        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged(nameof(login));
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(name));
            }
        }

        public bool IsEnabled { get; set; }
        #endregion

        #region Private members
        private MailServerConfigurationWindow window;
        private string smtpAddress = "";
        private string login = "";
        private string name;
        private string title;
        private string baseTitle = "Configurer un serveur ";
        #endregion

        #region Commands
        #endregion

        #region Constructor
        /// <summary>
        /// The Default Constructor
        /// </summary>
        public ServerConfigurationViewModel(MailServerConfigurationWindow window)
        {
            this.window = window;
            Title = "Imap";
            IsEnabled = true;
        }

        /// <summary>
        /// The Constructor predifined server
        /// </summary>
        /// <param name="title"></param>
        /// <param name="smtp"></param>
        public ServerConfigurationViewModel(MailServerConfigurationWindow window, string title, string smtp)
        {
            this.window = window;
            Title = title;
            SmtpAddress = smtp;
            IsEnabled = false;
        }
        #endregion
    }
}
