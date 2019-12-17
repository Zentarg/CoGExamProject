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

namespace MainWindow.ViewModels
{
    class ChangePasswordContentDialogVM : INotifyPropertyChanged
    {
        #region Instance Fields
        private string _enteredOldPassword;
        private string _enteredNewPassword;
        private string _enteredConfirmPassword;

        private int _checkOldPassword = 2;
        private int _checkNewPassword = 6;
        private int _checkConfirmPassword = 2;

        private string _oldPasswordTooltip;
        private string _newPasswordTooltip;
        private string _confirmPasswordTooltip;

        private BitmapImage _imagePathOldPassword;
        private BitmapImage _imagePathNewPassword;
        private BitmapImage _imagePathConfirmPassword;

        private bool _isConfirmButtonEnabled;
        #endregion

        #region Constructor
        public ChangePasswordContentDialogVM()
        {
            AccountHandler.AccountList.LoadAccounts();
        }
        #endregion

        #region Properties
        public string EnteredOldPassword
        {
            get { return _enteredOldPassword; }
            set { 
                _enteredOldPassword = value;
                OnPropertyChanged();

                CheckPassword();
            }
        }

        public string EnteredNewPassword
        {
            get { return _enteredNewPassword; }
            set { 
                _enteredNewPassword = value;
                OnPropertyChanged();

                CheckPassword();
            }
        }

        public string EnteredConfirmPassword
        {
            get { return _enteredConfirmPassword; }
            set { 
                _enteredConfirmPassword = value;
                OnPropertyChanged();

                CheckPassword();
            }
        }

        public int CheckOldPassword
        {
            get { return _checkOldPassword; }
            set {
                _checkOldPassword = value;
                OnPropertyChanged();
            }
        }

        public int CheckNewPassword
        {
            get { return _checkNewPassword; }
            set {
                _checkNewPassword = value;
                OnPropertyChanged();
            }
        }

        public int CheckConfirmPassword
        {
            get { return _checkConfirmPassword; }
            set { 
                _checkConfirmPassword = value;
                OnPropertyChanged();
            }
        }

        public string OldPasswordTooltip
        {
            get { return _oldPasswordTooltip; }
            set { 
                _oldPasswordTooltip = value;
                OnPropertyChanged();
            }
        }

        public string NewPasswordTooltip
        {
            get { return _newPasswordTooltip; }
            set { 
                _newPasswordTooltip = value;
                OnPropertyChanged();
            }
        }

        public string ConfirmPasswordTooltip
        {
            get { return _confirmPasswordTooltip; }
            set { 
                _confirmPasswordTooltip = value;
                OnPropertyChanged();
            }
        }

        public BitmapImage ImagePathOldPassword
        {
            get { return _imagePathOldPassword; }
            set { 
                _imagePathOldPassword = value;
                OnPropertyChanged();
            }
        }

        public BitmapImage ImagePathNewPassword
        {
            get { return _imagePathNewPassword; }
            set {
                _imagePathNewPassword = value;
                OnPropertyChanged();
            }
        }

        public BitmapImage ImagepathConfirmPassword
        {
            get { return _imagePathConfirmPassword; }
            set { 
                _imagePathConfirmPassword = value;
                OnPropertyChanged();
            }
        }

        public bool IsConfirmButtonEnabled
        {
            get { return _isConfirmButtonEnabled; }
            set { 
                _isConfirmButtonEnabled = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// A method that enables the confirm button if all text boxes have a green check next to them
        /// </summary>
        /// <returns>returns true if all of the 3 checks result in 0, else return false</returns>
        public bool CheckConfirmButtonStatus()
        {
            if (CheckConfirmPassword == 0 && CheckNewPassword == 0 && CheckOldPassword == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks on the old password, new password and confirm new password are done here
        /// </summary>
        public void CheckPassword()
        {
            CheckOldPassword = AccountHandler.PasswordCheckVersusOldPassword(EnteredOldPassword);
            OldPasswordTooltip = AccountHandler.PasswordConfirmResultString(CheckOldPassword);
            ImagePathOldPassword = AccountHandler.ReturnImagePathConfirmPassword(CheckOldPassword);

            CheckNewPassword = AccountHandler.EnteredNewPasswordMatchesEnteredOldPassword(EnteredNewPassword);
            NewPasswordTooltip = AccountHandler.EnteredNewPasswordMatchesOldPasswordResultFromString(CheckNewPassword);
            ImagePathNewPassword = AccountHandler.ReturnImagePathChangePassword(CheckNewPassword);

            CheckConfirmPassword = AccountHandler.ConfirmPasswordCheck(EnteredConfirmPassword, EnteredNewPassword);
            ConfirmPasswordTooltip = AccountHandler.PasswordConfirmResultString(CheckConfirmPassword);
            ImagepathConfirmPassword = AccountHandler.ReturnImagePathConfirmPassword(CheckConfirmPassword);



            IsConfirmButtonEnabled = CheckConfirmButtonStatus();
        }

        /// <summary>
        /// Method that changes the password of the account and writes the account list to the file
        /// </summary>
        public void ChangePassword()
        {
            AccountHandler.Account.PassWord = EnteredConfirmPassword;
            AccountHandler.ChangeUserPassword(EnteredConfirmPassword);
            AccountHandler.AccountList.AddAccountToFile();
            IsConfirmButtonEnabled = false;
        }

        /// <summary>
        /// Method that tells the view when a property is updated
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
