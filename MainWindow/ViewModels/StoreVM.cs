using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using MainWindow.Annotations;
using MainWindow.Models;

namespace MainWindow.ViewModels
{
    class StoreVM : INotifyPropertyChanged
    {
        private readonly Game _newGame;
        private readonly GameList _gameList;

        public StoreVM()
        {
            _newGame = new Game(new Account("username", "password", "displayName"), "Assets/Logo" , "gameName", 34.5f, 0, "description", "gamepath", new List<CarrouselItem>());
            _gameList = GameList.Instance;
            DoAddGame = new RelayCommand(AddGame);
            DoLoadGames = new RelayCommand(LoadGames);
        }

        public RelayCommand DoAddGame { get; set; }
        public RelayCommand DoLoadGames { get; set; }

        private void AddGame()
        {
            _gameList.AddGame(_newGame);
            OnPropertyChanged(nameof(StoreGameCollection));
        }

        private void LoadGames()
        {
            _gameList.LoadGames();
            OnPropertyChanged(nameof(StoreGameCollection));
        }

        public ObservableCollection<Game> StoreGameCollection {
            get { return _gameList.StoreGameCollection; }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
