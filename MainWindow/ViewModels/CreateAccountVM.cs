using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using MainWindow;
using MainWindow.Annotations;
using MainWindow.Models;

namespace MainWindow.ViewModels
{
    class CreateAccountVM : INotifyPropertyChanged
    {
        private string _tempUsername;
        private string _tempPassword;
        private string _tempDisplayName;

        private AccountLists _accountList = AccountLists.AccountListInstance;

        public RelayCommand DoConfirm { get; set; }

        public CreateAccountVM()
        {
            
            _accountList.LoadAccounts();
            DoConfirm = new RelayCommand(Confirm);
        }

        private async void Confirm()
        {
            Account tempAccount = new Account(_tempUsername, _tempPassword, _tempDisplayName);
            await _accountList.CreateAccount(tempAccount);
        }

        #region Properties
        public string TempUsername
        {
            get { return _tempUsername; }
            set { _tempUsername = value; OnPropertyChanged(); }
            
        }

        public string TempPassword
        {
            get { return _tempPassword; }
            set { _tempPassword = value; OnPropertyChanged(); }
        }

        public string TempDisplayName
        {
            get { return _tempDisplayName; }
            set { _tempDisplayName = value; OnPropertyChanged(); }
        }
        #endregion

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
