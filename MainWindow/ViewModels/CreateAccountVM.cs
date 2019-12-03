using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MainWindow;
using MainWindow.Annotations;

namespace MainWindow.ViewModels
{
    class CreateAccountVM : INotifyPropertyChanged
    {
        private string _tempUsername;
        private string _tempPassword;

        public CreateAccountVM()
        {
            AccountLists _accountLists = new AccountLists();
            _accountLists.LoadAccounts();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
