using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using GalaSoft.MvvmLight.Command;
using MainWindow.Annotations;
using MainWindow.Models;

namespace MainWindow.ViewModels
{
    public class StoreVM : INotifyPropertyChanged
    {
        private readonly GameList _gameList;
        private readonly ShoppingCart _shoppingCart;
        private ObservableCollection<Game> _filteredGames = new ObservableCollection<Game>();

        public StoreVM()
        {
            _gameList = GameList.Instance;
            _shoppingCart = ShoppingCart.Instance;
            DoLoadGames = new RelayCommand(LoadGames);
            LoadGames();

            AccountHandler.StoreVm = this;

        }
        public RelayCommand DoLoadGames { get; set; }


        /// <summary>
        /// Calls LoadGames() on GameList singleton, then calls FilterGames() with an empty string.
        /// </summary>
        private async void LoadGames()
        {
            await _gameList.LoadGames();
            FilterGames("");
        }

        /// <summary>
        /// Filters all games based on search terms. (Updates FilteredGames property)
        /// </summary>
        /// <param name="searchTerms">The terms to be searched for.</param>
        public void FilterGames(string searchTerms)
        {
            List<Game> _tempList = new List<Game>();

            // Loops over every game in StoreGameCollection, to add the games that fit the search terms to the temporary list defined above.
            foreach (Game game in StoreGameCollection)
            {
                if (game.Name.ToLower().Contains(searchTerms.ToLower()) && game.ReleaseDate.Date <= DateTime.Now)
                    _tempList.Add(game);
            }

            // Orders the temp list in alphabetical order.
            _tempList = _tempList.OrderBy(item => item.Name).ToList();

            // Clears the FilteredGames list
            FilteredGames.Clear();

            // Adds all the games from the temp list to the FilteredList, if they don't exist in the FilteredList already.
            foreach (Game game in _tempList)
            {
                FilteredGames.Add(game);
            }


        }

        public bool LoggedIn
        {
            get { return AccountHandler.Account != null; }
        }

        /// <summary>
        /// Adds SelectedGame to the shopping cart, then refreshes it in the mainpage.
        /// </summary>
        public void PurchaseSelectedGame()
        {
            _shoppingCart.AddGame(SelectedGame);
            _shoppingCart.MainPageVm.RefreshPurchaseSelectedGame();
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

        /// <summary>
        /// Updates the LoggedIn property.
        /// </summary>
        public void OnAccountChanged()
        {
            OnPropertyChanged(nameof(LoggedIn));
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
