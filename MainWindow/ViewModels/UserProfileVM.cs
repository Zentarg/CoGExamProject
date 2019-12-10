using MainWindow.Annotations;
using MainWindow.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MainWindow.ViewModels
{
    public class UserProfileVM : INotifyPropertyChanged
    {
        private string _displayName;

        public UserProfileVM()
        {
            DisplayName = AccountHandler.SetDisplayNameForUI;
        }

        public string DisplayName
        {
            get { return _displayName; }
            set { 
                _displayName = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
