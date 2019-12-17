using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using LinqToDB;
using MainWindow;
using MainWindow.Annotations;
using MainWindow.Models;
using Windows.UI.Xaml.Media.Imaging;
using static MainWindow.Models.AccountHandler;

namespace MainWindow.ViewModels
{
    class CreateAccountVM : INotifyPropertyChanged
    {
        #region Instance Fields
        private string _tempUsername;
        private string _tempPassword;
        private string _tempDisplayName;
        private string _tempProfilePicturePath;

        private int _usernameCheck = 5;
        private string _usernameTooltip;
        private BitmapImage _imagePathUsername;

        private int _passwordCheck = 5;
        private string _passwordTooltip;
        private BitmapImage _imagePathPassword;

        private int _displaynameCheck = 4;
        private string _displaynameTooltip;
        private BitmapImage _imagePathDisplayname;

        private string _confirmPassword;
        private int _passwordConfirmCheck = 2;
        private string _confirmPasswordTooltip;
        private BitmapImage _imagePathConfirmPassword;

        private bool _isConfirmButtonEnabled = false;
        #endregion

        #region Constructor
        public CreateAccountVM()
        {
            AccountList.LoadAccounts();
            DoConfirm = new RelayCommand(Confirm); 
        }
        #endregion

        #region Properties
        public RelayCommand DoConfirm { get; set; }
        public string TempProfilePicturePath { get { return _tempProfilePicturePath; } set { _tempProfilePicturePath = value; OnPropertyChanged(); } }
        public bool IsConfirmButtonEnabled { get { return _isConfirmButtonEnabled; } set { _isConfirmButtonEnabled = value; OnPropertyChanged(); } }

        public  string TempUsername
        {
            get { return _tempUsername; }
            set { 
                _tempUsername = value; 
                OnPropertyChanged();

                Username();
                IsConfirmButtonEnabled = EnableConfirmButton();
            }    
        }

        public string TempPassword
        {
            get { return _tempPassword; }
            set { 
                _tempPassword = value; 
                OnPropertyChanged();

                Password();
                CPassword();
                IsConfirmButtonEnabled = EnableConfirmButton();
            }
        }

        public string TempDisplayName
        {
            get { return _tempDisplayName; }
            set { 
                _tempDisplayName = value; 
                OnPropertyChanged();

                DisplayName();
                IsConfirmButtonEnabled = EnableConfirmButton();
            }
        }

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                OnPropertyChanged();

                CPassword();
                IsConfirmButtonEnabled = EnableConfirmButton();
            }
        }

        public int PasswordConfirmCheck
        {
            get { return _passwordConfirmCheck; }
            set
            {
                _passwordConfirmCheck = value;
                OnPropertyChanged();
            }
        }

        public string ConfirmPasswordTooltip
        {
            get { return _confirmPasswordTooltip; }
            set { _confirmPasswordTooltip = value; OnPropertyChanged(); }
        }

        public BitmapImage ImagePathConfirmPassword
        {
            get { return _imagePathConfirmPassword; }
            set
            {
                _imagePathConfirmPassword = value;
                OnPropertyChanged();
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
            get { return _imagePathDisplayname; }
            set { _imagePathDisplayname = value; OnPropertyChanged(); }
        }

        public BitmapImage ProfilePicture
        {
            get; set;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method that creates an account with the entered information and stores it into relevant files
        /// </summary>
        public void Confirm()
        {
            TempDisplayName = AccountHandler.DisplayNameAddTag(_tempDisplayName);
            AccountHandler.Account = new Account(_tempUsername, TempPassword, TempDisplayName);
            AccountHandler.CreateAccount(AccountHandler.Account, TempProfilePicturePath);
            AccountList.AddAccountToFile();
            SetDisplayNameForUI = AccountHandler.Account.DisplayName;
        }

        /// <summary>
        /// Method for enabling the confirm button
        /// </summary>
        /// <returns></returns>
        private bool EnableConfirmButton()
        {
            if(UsernameCheck == 0 && PasswordCheck == 00 && DisplaynameCheck == 0 && PasswordConfirmCheck == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks for username are done here
        /// </summary>
        private void Username()
        {
            UsernameCheck = UserNameCheck(_tempUsername);
            UsernameTooltip = UserNameResultStringForCreateAccount(UsernameCheck);
            ImagePathUsername = ReturnImagePathUsernamePassword(UsernameCheck);
        }

        /// <summary>
        /// Checks for password are done here
        /// </summary>
        private void Password()
        {
            PasswordCheck = PasswordCheck(_tempPassword);
            PasswordTooltip = PasswordResultString(PasswordCheck);
            ImagePathPassword = ReturnImagePathUsernamePassword(PasswordCheck);      
        }

        /// <summary>
        /// Checks for confirm password are done here
        /// </summary>
        private void CPassword()
        {
            PasswordConfirmCheck = ConfirmPasswordCheck(_confirmPassword, TempPassword);
            ConfirmPasswordTooltip = PasswordConfirmResultString(PasswordConfirmCheck);
            ImagePathConfirmPassword = ReturnImagePathConfirmPassword(PasswordConfirmCheck);
        }

        /// <summary>
        /// Checks for display name are done here
        /// </summary>
        private void DisplayName()
        {
            DisplaynameCheck = DisplayNameCheck(_tempDisplayName);
            DisplaynameTooltip = DisplayNameResultStrng(DisplaynameCheck);
            ImagePathDisplayname = ReturnImagePathDisplayName(DisplaynameCheck);
        }

        /// <summary>
        /// Method that lets the view know when a property has been updated
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
