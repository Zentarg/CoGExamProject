using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using GalaSoft.MvvmLight.Command;
using MainWindow.Annotations;
using MainWindow.Models;

namespace MainWindow.ViewModels
{
    public class StoreVM : INotifyPropertyChanged
    {
        private readonly Game _newGame;
        private readonly GameList _gameList;
        private readonly ShoppingCart _shoppingCart;
        private ObservableCollection<Game> _filteredGames = new ObservableCollection<Game>();
        private bool _isBuyButtonEnabled = false;
        private ReleaseDate _releaseDate;

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
            List<CarrouselItem> carrouselItems = new List<CarrouselItem>
            {
                new CarrouselItem(Constants.CarrouselItemType.Image, "https://helpx.adobe.com/content/dam/help/en/stock/how-to/visual-reverse-image-search/jcr_content/main-pars/image/visual-reverse-image-search-v2_intro.jpg"),
                new CarrouselItem(Constants.CarrouselItemType.Image, "https://images.pexels.com/photos/414612/pexels-photo-414612.jpeg"),
                new CarrouselItem(Constants.CarrouselItemType.Image, "https://media3.s-nbcnews.com/j/newscms/2019_41/3047866/191010-japan-stalker-mc-1121_06b4c20bbf96a51dc8663f334404a899.fit-760w.JPG"),
                new CarrouselItem(Constants.CarrouselItemType.Image, "https://images.pexels.com/photos/459225/pexels-photo-459225.jpeg"),
                new CarrouselItem(Constants.CarrouselItemType.YoutubeVideo, "https://www.youtube.com/embed/8xNmUNlJZtE")
            };
            string lorem =
                "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
            _newGame = new Game(new Account("username", "password", "displayName"), "https://helpx.adobe.com/content/dam/help/en/stock/how-to/visual-reverse-image-search/jcr_content/main-pars/image/visual-reverse-image-search-v2_intro.jpg", "gameName", 34.5f, 0, lorem, "gamepath", categories,carrouselItems,new ReleaseDate(22,12,2069));
            _gameList = GameList.Instance;
            _shoppingCart = ShoppingCart.Instance;
            DoLoadGames = new RelayCommand(LoadGames);
            LoadGames();

            AccountHandler.StoreVm = this;

        }

        public RelayCommand DoAddGame { get; set; }
        public RelayCommand DoLoadGames { get; set; }
  

        private async void LoadGames()
        {
            await _gameList.LoadGames();
            FilterGames("");
        }

        public void FilterGames(string searchTerms)
        {
            List<Game> _tempList = new List<Game>();
            foreach (Game game in StoreGameCollection)
            {
                if (game.Name.ToLower().Contains(searchTerms.ToLower()))
                    _tempList.Add(game);
            }

            for (int i = FilteredGames.Count; i > 0; i--)
            {
                if (!(_tempList.Contains(FilteredGames[i - 1])))
                    FilteredGames.Remove(FilteredGames[i - 1]);
            }

            foreach (Game game in _tempList)
            {
                if (!FilteredGames.Contains(game))
                    FilteredGames.Add(game);
            }

        }

        public bool LoggedIn
        {
            get { return AccountHandler.Account != null; }
        }

        public void PurchaseSelectedGame()
        {
            _shoppingCart.AddGame(SelectedGame);
        }

        public ObservableCollection<Game> StoreGameCollection {
            get { return _gameList.StoreGameCollection; }
        }

        public ObservableCollection<Game> FilteredGames
        {
            get { return _filteredGames; }
            set
            {
                _filteredGames = value;
                OnPropertyChanged();
            }
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

        public void OnAccountChanged()
        {
            OnPropertyChanged(nameof(LoggedIn));
        }

        public void CheckReleaseDate()
        {
            DateTime date = new DateTime(_releaseDate.Year, _releaseDate.Month, _releaseDate.Day);
            if (date <= DateTime.Now)
            {
                IsBuyButtonEnabled = true;
                OnPropertyChanged();
            } else if (date > DateTime.Now)
            {
                IsBuyButtonEnabled = false;
                OnPropertyChanged();
            }
        }
        public bool IsBuyButtonEnabled
        {
            get { return _isBuyButtonEnabled; }
            set { _isBuyButtonEnabled = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
