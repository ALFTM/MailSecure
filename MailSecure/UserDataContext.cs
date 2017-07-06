using MailSecure.Core;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MailSecure
{
    public class UserDataContext: INotifyPropertyChanged
    {
        private UserMailFacts currentUser;
        private PageType currentPage;
        private string displayedName;

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
