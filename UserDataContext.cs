using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MailSecure
{
    public class UserDataContext: INotifyPropertyChanged
    {
        private UserMailFacts currentUser;
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
