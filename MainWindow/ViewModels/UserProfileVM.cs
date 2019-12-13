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
        private GameDetails _domainObject;
        private GameCatalog _gameCatalog;
        private GameDetails _selectedGame;
        private int _gamesOwnedCount;
        private string _joinedDate;
        private int _thisYear = DateTime.Now.Year;
        private int _yearDifference;

        public UserProfileVM()
        {
            DisplayName = AccountHandler.SetDisplayNameForUI;
            _gameCatalog = new GameCatalog();
            DomainObject();
            _selectedGame = null;
            _joinedDate = AccountHandler.AccountDetail.JoinDate;
            _yearDifference = _thisYear - Convert.ToInt32(_joinedDate.Split("/")[2]);
            _gamesOwnedCount = AccountHandler.AccountDetail.GamesOwnedCount;
            _profileImagePath = AccountHandler.SetProfileImagePathForUI;
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

        public string JoinedDate
        {
            get { return _joinedDate; }
        }

        public int YearsSinceJoined { get { return _yearDifference; } }
        public string ProfileImagePath { get { return _profileImagePath; } set { _profileImagePath = value; OnPropertyChanged(); } }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        //Test Stuff

        public GameDetails SelectedGame
        {
            get
            {
                return _selectedGame;
            }

            set
            {
                _selectedGame = value;
                OnPropertyChanged();
                //_deletionCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<GameDetails> GamesCollection
        {
            get { return _gameCatalog.Games; }
        }

        public void DomainObject()
        {
            foreach (var c in _gameCatalog.Games)
            {
                _domainObject = c;
            }
        }
    }
}
