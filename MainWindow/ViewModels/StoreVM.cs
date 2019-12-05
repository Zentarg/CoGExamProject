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
        private readonly ShoppingCart _shoppingCart;

        public StoreVM()
        {
            List<string> categories = new List<string>
            {
                "Category 1",
                "Category 2",
                "Category 1",
                "Category 2",
                "Category 1",
                "Category 2",
                "Category 1",
                "Category 2",
                "Category 1",
                "Category 2",
                "Category 1",
                "Category 2",
                "Category 1",
                "Category 2",
                "Category 1",
                "Category 2"
            };
            string lorem =
                "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
            _newGame = new Game(new Account("username", "password", "displayName"), "https://helpx.adobe.com/content/dam/help/en/stock/how-to/visual-reverse-image-search/jcr_content/main-pars/image/visual-reverse-image-search-v2_intro.jpg", "gameName", 34.5f, 0, lorem, "gamepath", categories,new List<CarrouselItem>());
            _gameList = GameList.Instance;
            _shoppingCart = ShoppingCart.Instance;
            DoAddGame = new RelayCommand(AddGame);
            DoLoadGames = new RelayCommand(LoadGames);
            LoadGames();
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

        public void PurchaseSelectedGame()
        {
            _shoppingCart.AddGame(SelectedGame);
        }

        public ObservableCollection<Game> StoreGameCollection {
            get { return _gameList.StoreGameCollection; }
        }

        public Game SelectedGame
        {
            get { return _gameList.SelectedGame; }
            set
            {
                _gameList.SelectedGame = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
