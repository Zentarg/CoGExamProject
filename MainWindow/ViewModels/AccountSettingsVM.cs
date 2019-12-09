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
    class AccountSettingsVM : INotifyPropertyChanged
    {
        private string _tempDisplayName;

        public AccountSettingsVM()
        {
            AccountHandler.AccountList.LoadAccounts();

        }

        public string TempDisplayName
        {
            get { return _tempDisplayName; }
            set
            {
                _tempDisplayName = value;
            }
        }



        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
