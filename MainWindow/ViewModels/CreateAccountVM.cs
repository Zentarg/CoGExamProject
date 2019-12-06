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
using Windows.UI.Xaml.Media.Imaging;
using static MainWindow.Models.AccountHandler;

namespace MainWindow.ViewModels
{
    class CreateAccountVM : INotifyPropertyChanged
    {
        private string _tempUsername;
        private string _tempPassword;
        private string _tempDisplayName;

        private int _usernameCheck = 5;
        private string _usernameTooltip;
        private BitmapImage _imagePathUsername;

        private int _passwordCheck = 5;
        private string _passwordTooltip;
        private BitmapImage _imagePathPassword;

        private int _displaynameCheck = 4;
        private string _displaynameTooltip;
        private BitmapImage _ImagePathDisplayname;

        private bool _isConfirmButtonEnabled = false;


        
        public RelayCommand DoConfirm { get; set; }



        public CreateAccountVM()
        {
            AccountList.LoadAccounts();
            DoConfirm = new RelayCommand(Confirm);
        }

        public async void Confirm()
        {
            TempDisplayName = AccountHandler.DisplayNameAddTag(_tempDisplayName);
            AccountHandler.Account = new Account(_tempUsername, TempPassword, TempDisplayName);
            AccountHandler.CreateAccount(AccountHandler.Account);
            await AccountList.AddAccountToFile(AccountHandler.Account);
        }

        #region Properties
        public  string TempUsername
        {
            get { return _tempUsername; }
            set { 
                _tempUsername = value; 
                OnPropertyChanged(); 

                UsernameCheck = UserNameCheck(_tempUsername);
                UsernameTooltip = UserNameResultString(UsernameCheck);
                ImagePathUsername = ReturnImagePathUsernamePassword(UsernameCheck);
                IsConfirmButtonEnabled = EnableConfirmButton();
            }    
        }

        public string TempPassword
        {
            get { return _tempPassword; }
            set { 
                _tempPassword = value; 
                OnPropertyChanged();

                PasswordCheck = PasswordCheck(_tempPassword);
                PasswordTooltip = PasswordResultString(PasswordCheck);
                ImagePathPassword = ReturnImagePathUsernamePassword(PasswordCheck);
                IsConfirmButtonEnabled = EnableConfirmButton();
            }
        }

        public string TempDisplayName
        {
            get { return _tempDisplayName; }
            set { 
                _tempDisplayName = value; 
                OnPropertyChanged();

                DisplaynameCheck = DisplayNameCheck(_tempDisplayName);
                DisplaynameTooltip = DisplayNameResultStrng(DisplaynameCheck);
                ImagePathDisplayname = ReturnImagePathDisplayName(DisplaynameCheck);
                IsConfirmButtonEnabled = EnableConfirmButton();
            }
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

        public string UsernameTooltip
        {
            get { return _usernameTooltip; }
            set
            {
                _usernameTooltip = value;
                OnPropertyChanged();
            }
        }

        public BitmapImage ImagePathUsername
        {
            get { return _imagePathUsername; }
            set
            {
                _imagePathUsername = value;
                OnPropertyChanged();
            }
        }


        public int PasswordCheck
        {
            get { return _passwordCheck; }
            set
            {
                _passwordCheck = value;
                OnPropertyChanged();
            }
        }

        public string PasswordTooltip
        {
            get { return _passwordTooltip; }
            set
            {
                _passwordTooltip = value; 
                OnPropertyChanged();
            }
        }

        public BitmapImage ImagePathPassword
        {
            get { return _imagePathPassword; }
            set
            {
                _imagePathPassword = value;
                OnPropertyChanged();
            }
        }


        public int DisplaynameCheck
        {
            get { return _displaynameCheck; }
            set { _displaynameCheck = value; OnPropertyChanged(); }
        }

        public string DisplaynameTooltip
        {
            get { return _displaynameTooltip; }
            set { _displaynameTooltip = value; OnPropertyChanged(); }
        }

        public BitmapImage ImagePathDisplayname
        {
            get { return _ImagePathDisplayname; }
            set { _ImagePathDisplayname = value; OnPropertyChanged(); }
        }

        public BitmapImage ProfilePicture
        {
            get; set;
        }

        public bool IsConfirmButtonEnabled { get { return _isConfirmButtonEnabled; } set { _isConfirmButtonEnabled = value; OnPropertyChanged(); } }
        #endregion

        private bool EnableConfirmButton()
        {
            if(UsernameCheck == 0 && PasswordCheck == 00 && DisplaynameCheck == 0)
            {
                return true;
            }
            else
            {
                return false;
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
