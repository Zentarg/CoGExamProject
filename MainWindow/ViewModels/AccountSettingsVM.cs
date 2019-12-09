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
    class AccountSettingsVM : INotifyPropertyChanged
    {
        private string _tempDisplayName;
        private string _oldDisplayName;
        private string _currentDisplayName = AccountHandler.Account.DisplayName.Split("#")[0];
        private int _displaynameCheck = 4;
        private string _displaynameTooltip;
        private BitmapImage _imagePathDisplayName;
        private bool _isSetDisplayNameEnabled;

        public AccountSettingsVM()
        {
            AccountHandler.AccountList.LoadAccounts();
            DoSetDisplayName = new RelayCommand(SetDisplayName);
            
        }

        public string TempDisplayName
        {
            get { return _tempDisplayName; }
            set
            {
                _tempDisplayName = value;
                OnPropertyChanged();

                DisplaynameCheck = AccountHandler.DisplayNameCheck(_tempDisplayName);
                DisplaynameTooltip = AccountHandler.DisplayNameResultStrng(DisplaynameCheck);
                ImagePathDisplayname = AccountHandler.ReturnImagePathDisplayName(DisplaynameCheck);
                IsSetDisplayNameEnabled = EnableSetDisplayName();
                
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
            get { return _imagePathDisplayName; }
            set { _imagePathDisplayName = value; OnPropertyChanged(); }
        }

        public bool IsSetDisplayNameEnabled { get { return _isSetDisplayNameEnabled; } set { _isSetDisplayNameEnabled = value; OnPropertyChanged(); } }
        public RelayCommand DoSetDisplayName { get; set; }


        public async void SetDisplayName()
        {
            _currentDisplayName = _tempDisplayName;
            AccountHandler.Account.DisplayName = AccountHandler.DisplayNameAddTag(_tempDisplayName);
            AccountHandler.ChangeDisplayNameInAccountList(AccountHandler.Account.DisplayName);
            await AccountHandler.AccountList.AddAccountToFile(AccountHandler.Account);
            AccountHandler.SetDisplayNameForUI = AccountHandler.Account.DisplayName;
            AccountHandler.MainPageVm?.CallForAccountStatus();
            IsSetDisplayNameEnabled = false;
        }

        private bool EnableSetDisplayName()
        {
            if (DisplaynameCheck == 0 && _currentDisplayName != _tempDisplayName && _oldDisplayName != _currentDisplayName)
            { 
                return true;
            }
            else
            {
                DisplaynameCheck = 5;
                DisplaynameTooltip = "You cannot set your display name to your current one";
                ImagePathDisplayname = new BitmapImage(new Uri("ms-appx:///Assets/RedX.png"));
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
