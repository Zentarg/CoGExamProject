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
        #region Instance Fields
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
        #endregion

        #region Constructor
        public AccountLoginVM()
        {
            AccountList.LoadAccounts();
            DoConfirm = new RelayCommand(Confirm);
        }
        #endregion

        #region Properties
        public string TempUsername
        {
            get { return _tempUsername; }
            set
            {
                _tempUsername = value;
                OnPropertyChanged();

                Username();
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
        #endregion

        #region Methods
        /// <summary>
        /// Method that logs the user into the system, calls AccountHandler.Login(_tempUsername, _tempPassword)
        /// </summary>
        public void Confirm()
        {
            Login(_tempUsername, _tempPassword);
            SetDisplayNameForUI = AccountHandler.Account.DisplayName;
        }

        /// <summary>
        /// A method that is called whenever something is typed into the login text fields in the view, used to enable the confirm button
        /// </summary>
        /// <returns>returns true if username exists and has a password match, returns false otherwise</returns>
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

        /// <summary>
        /// All password checks are done here
        /// </summary>
        private void Password()
        {
            PasswordCheck = PasswordCheckForLogin(_tempPassword, _tempUsername);
            PasswordTooltip = PasswordResultStringForLogin(PasswordCheck);
            ImagePathPassword = ReturnImagePathPasswordForLogin(PasswordCheck);
            IsConfirmButtonEnabled = EnableConfirmButton();
        }

        /// <summary>
        /// All username checks are done here
        /// </summary>
        private void Username()
        {
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

        /// <summary>
        /// Method for telling the view a property has been updated
        /// </summary>
        /// <param name="propertyName"></param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
