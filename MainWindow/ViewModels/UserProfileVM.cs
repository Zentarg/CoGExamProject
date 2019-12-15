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

namespace MainWindow.ViewModels
{
    public class UserProfileVM : INotifyPropertyChanged
    {
        private string _displayName;
        private string _profileImagePath;
        private int _gamesOwnedCount;
        private string _joinedDate;
        private int _thisYear = DateTime.Now.Year;
        private int _yearDifference;
        
        private GameList _gameList;
        private AccountPurchase _selectedPurchase;

        public UserProfileVM()
        {
            DisplayName = AccountHandler.SetDisplayNameForUI;
            _joinedDate = AccountHandler.AccountDetail.JoinDate;
            _yearDifference = _thisYear - Convert.ToInt32(_joinedDate.Split("/")[2]);
            _gamesOwnedCount = AccountHandler.AccountDetail.GamesOwnedCount;
            _profileImagePath = AccountHandler.SetProfileImagePathForUI;
            RecentPurchases = GetRecentPurchases();
            
            _gameList = GameList.Instance;
            LoadGames();
            _selectedPurchase = null;
        }

        private async void LoadGames()
        {
            await _gameList.LoadGames();
        }

        public string DisplayName
        {
            get { return _displayName; }
            set { 
                _displayName = value;
                OnPropertyChanged();
            }
        }

        public int GamesOwnedCount
        {
            get { return _gamesOwnedCount; }
            set { 
                _gamesOwnedCount = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<AccountPurchase> RecentPurchases { get; }

        public string JoinedDate
        {
            get { return _joinedDate; }
        }

        public int YearsSinceJoined { get { return _yearDifference; } }
        public string ProfileImagePath { get { return _profileImagePath; } set { _profileImagePath = value; OnPropertyChanged(); } }
        
        public AccountPurchase SelectedPurchase { get { return _selectedPurchase; } set { _selectedPurchase = value; OnPropertyChanged(); SelectedGame = GetGameFromPurchaseHistory(); } }
        public Game SelectedGame { set { _gameList.SelectedGame = value; OnPropertyChanged(); } }

        public ObservableCollection<AccountPurchase> GetRecentPurchases()
        {
            ObservableCollection<AccountPurchase> temp = new ObservableCollection<AccountPurchase>();

            if(AccountHandler.AccountDetail.PurchaseHistory.Count > 5)
            {
                for (int count = 0; count < 6; count++)
                {
                    temp.Add(AccountHandler.AccountDetail.PurchaseHistory[count]);
                }
                return temp;
            }
            else
            {
                temp = AccountHandler.AccountDetail.PurchaseHistory;
                return temp;
            }
        }

        public Game GetGameFromPurchaseHistory()
        {
            foreach (Game game in GameList.Instance.StoreGameCollection)
            {
                if (SelectedPurchase.GameName == game.Name)
                {
                    return game;
                }
            }
            return null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
