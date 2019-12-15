using GalaSoft.MvvmLight.Command;
using MainWindow.Annotations;
using MainWindow.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private bool _isAddedGamesEnabled;
        private GameList _gameList;
        private string _username;
        private string _pfpPath;
        private Game _selectedGame;

        public AccountSettingsVM()
        {
            AccountHandler.AccountList.LoadAccounts();
            DoSetDisplayName = new RelayCommand(SetDisplayName);
            _username = AccountHandler.Account.UserName;
            _pfpPath = AccountHandler.SetProfileImagePathForUI;
            _gameList = GameList.Instance;
            if (_gameList.StoreGameCollection.Count == 0)
                LoadGames();
            else
                FilterGames();
            SelectedGame = GamesMade.Count > 0 ? GamesMade[0] : null;
        }

        public string PFPPath { get { return _pfpPath; } }

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
        public string Username { get { return _username; } }
        public Game SelectedGame { get { return _selectedGame;} set { _selectedGame = value; OnPropertyChanged();} }

        public ObservableCollection<Game> GamesMade { get; set; } = new ObservableCollection<Game>();

        public async void SetDisplayName()
        {
            _currentDisplayName = _tempDisplayName;
            AccountHandler.Account.DisplayName = AccountHandler.DisplayNameAddTag(_tempDisplayName);
            AccountHandler.ChangeDisplayNameInAccountList(AccountHandler.Account.DisplayName);
            await AccountHandler.AccountList.AddAccountToFile(AccountHandler.Account);
            AccountHandler.SetDisplayNameForUI = AccountHandler.Account.DisplayName;
            AccountHandler.MainPageVm?.CallForAccountStatus();
            IsSetDisplayNameEnabled = false;

            //Below saves the changed display name to the <username>.json file under /AccountDetails/
            AccountHandler.AccountDetail.DisplayName = AccountHandler.Account.DisplayName;
            AccountHandler.AccountDetail.CreateUserDetailsFile(AccountHandler.AccountDetail, AccountHandler.Account.UserName);
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

        private async void LoadGames()
        {
            await _gameList.LoadGames();
            FilterGames();
        }

        private void FilterGames()
        {
            foreach (Game game in _gameList.StoreGameCollection)
            {
                if (game.Author.UserName == AccountHandler.Account.UserName)
                {
                    GamesMade.Add(game);
                }
            }
            OnPropertyChanged(nameof(GamesMade));
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
