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
    class PurchaseHistoryVM : INotifyPropertyChanged
    {
        #region Instance Fields
        private AccountPurchase _selectedPurchase;
        private GameList _gameList;
        #endregion

        #region Constructor
        public PurchaseHistoryVM()
        { 
            _selectedPurchase = null;
            _gameList = GameList.Instance;
            LoadGames();
        }
        #endregion

        #region Properties
        public AccountPurchase SelectedPurchase { get { return _selectedPurchase; } set { _selectedPurchase = value; OnPropertyChanged(); SelectedGame = GetGameFromPurchaseHistory(); } }
        public Game SelectedGame { set { _gameList.SelectedGame = value; OnPropertyChanged(); } }
        public ObservableCollection<AccountPurchase> PurchaseHistory { get { return AccountHandler.AccountDetail.PurchaseHistory; } }
        public GameList ReturnInstance { get { return _gameList; } }
        #endregion

        #region Methods
        /// <summary>
        /// Method for getting a selected game's game object so that you can go to the store
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Method for loading the gamelist.json if it hasnt been loaded
        /// </summary>
        private async void LoadGames()
        {
            await _gameList.LoadGames();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Method for telling the view a property has been updated
        /// </summary>
        /// <param name="propertyName"></param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
