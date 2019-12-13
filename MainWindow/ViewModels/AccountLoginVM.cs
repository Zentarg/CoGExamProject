using GalaSoft.MvvmLight.Command;
using MainWindow.Annotations;
using MainWindow.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;
using static MainWindow.Models.AccountHandler;

namespace MainWindow.ViewModels
{
    public class AccountLoginVM : INotifyPropertyChanged
    {
        private string _tempUsername;
        private string _tempPassword;

        private int _usernameCheck = 5;
        private string _usernameTooltip;
        private BitmapImage _imagePathUsername;

        private int _passwordCheck = 3;
        private string _passwordTooltip;
        private BitmapImage _imagePathPassword;


        private bool _isConfirmButtonEnabled = false;
        public RelayCommand DoConfirm { get; set; }



        public AccountLoginVM()
        {
            AccountList.LoadAccounts();
            DoConfirm = new RelayCommand(Confirm);
        }

        public void Confirm()
        {
            Login(_tempUsername, _tempPassword);
            SetDisplayNameForUI = AccountHandler.Account.DisplayName;
        }


        public string TempUsername
        {
            get { return _tempUsername; }
            set
            {
                _tempUsername = value;
                OnPropertyChanged();

                UsernameCheck = UserNameCheck(_tempUsername);
                UsernameTooltip = UserNameResultStringForLogin(UsernameCheck);
                ImagePathUsername = ReturnImagePathUsernameForLogin(UsernameCheck);
                IsConfirmButtonEnabled = EnableConfirmButton();

                //Allows one to enter password first and then the username without having to click on the password field and retype to check
                if (_tempPassword != null)
                {
                    Password();
                }     
            }
        }

        public string TempPassword
        {
            get { return _tempPassword; }
            set
            {
                _tempPassword = value;
                OnPropertyChanged();

                Password();
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



        public bool IsConfirmButtonEnabled { get { return _isConfirmButtonEnabled; } set { _isConfirmButtonEnabled = value; OnPropertyChanged(); } }

        private bool EnableConfirmButton()
        {
            if (UsernameCheck == 1 && PasswordCheck == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Password()
        {
            PasswordCheck = PasswordCheckForLogin(_tempPassword, _tempUsername);
            PasswordTooltip = PasswordResultStringForLogin(PasswordCheck);
            ImagePathPassword = ReturnImagePathPasswordForLogin(PasswordCheck);
            IsConfirmButtonEnabled = EnableConfirmButton();
        }


        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
