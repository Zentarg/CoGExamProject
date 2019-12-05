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
using static MainWindow.Models.AccountHandler;

namespace MainWindow.ViewModels
{
    class CreateAccountVM : INotifyPropertyChanged
    {
        private string _tempUsername;
        private string _tempPassword;
        private string _tempDisplayName;
        private int _usernameCheck = 5;
        private string _getFlyoutText;
       

        public RelayCommand DoConfirm { get; set; }

        public CreateAccountVM()
        {
            AccountList.LoadAccounts();
            DoConfirm = new RelayCommand(Confirm);
        }

        private async void Confirm()
        {
            AccountHandler.Account = new Account(_tempUsername, TempPassword, TempDisplayName);
            await AccountList.CreateAccount(AccountHandler.Account);
        }

        #region Properties
        public  string TempUsername
        {
            get { return _tempUsername; }
            set { 
                _tempUsername = value; 
                OnPropertyChanged(); 
                UsernameCheck = UserNameCheck(_tempUsername);
                GetFlyOutText = UserNameResultString(UsernameCheck);
            }    
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

        public int UsernameCheck
        {
            get { return _usernameCheck; }
            set 
            { 
                _usernameCheck = value; 
                OnPropertyChanged(); 
            }
        }


        public string GetFlyOutText
        {
            get { return _getFlyoutText; }
            set
            {
                _getFlyoutText = value;
                OnPropertyChanged();
            }
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
