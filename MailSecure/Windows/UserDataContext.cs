using MailSecure.Core;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security;

namespace MailSecure
{
    public class UserDataContext: INotifyPropertyChanged
    {
        private UserMailFacts currentUser;
        private PageType currentPage = PageType.SendingPage;
        private string displayedName;
        
        public SecureString PassHash { get; set; }

        public UserMailFacts CurrentUser {
            get { return this.currentUser; }
            set {
                this.currentUser = value;
            }
        }

        public string DisplayedName {
            get { return this.displayedName; }
            set {
                this.displayedName = value;
                OnPropertyChanged();
            }
        }

        public PageType CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
