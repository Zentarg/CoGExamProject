using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MainWindow.ViewModels;

namespace MainWindow.Models
{
    /// <summary>
    /// A singleton class, which insures that it is the same object being dealt with everywhere, stores shopping cart games and related things
    /// </summary>
    public class ShoppingCart
    {
        #region Instance fields
        private ObservableCollection<Game> _games;
        private static ShoppingCart instance;
        #endregion

        #region Constructor
        private ShoppingCart()
        {
            _games = new ObservableCollection<Game>();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method for Adding a game to an account's shopping cart list and saving it
        /// </summary>
        /// <param name="game"></param>
        public void AddGame(Game game)
        {
            Games.Add(game);
            if (AccountHandler.Account != null)
            {
                AccountHandler.AccountDetail.AccountShoppingCart.Add(game);
            }
        }

        /// <summary>
        /// Method for adding a game to the shopping cart list
        /// </summary>
        /// <param name="games">A game</param>
        public void AddGame(ObservableCollection<Game> games)
        {
            foreach (Game game in games)
            {
                Games.Add(game);
            }
        }

        /// <summary>
        /// Method for removing a game from an account's shopping cart list and saving it
        /// </summary>
        /// <param name="game">A game</param>
        public void RemoveGame(Game game)
        {
            Games.Remove(game);
            if (AccountHandler.Account != null)
            {
                AccountHandler.AccountDetail.AccountShoppingCart.Remove(game);
            }
        }

        /// <summary>
        /// Method for removing a game from the shopping cart list
        /// </summary>
        /// <param name="games"></param>
        public void RemoveGame(ObservableCollection<Game> games)
        {
            foreach (Game game in games)
            {
                Games.Remove(game);
                if (AccountHandler.Account != null)
                {
                    AccountHandler.AccountDetail.AccountShoppingCart.Remove(game);
                }
            }
        }

        /// <summary>
        /// A method for purchasing a game(s) at the checkout and storing information to the account
        /// </summary>
        public void PurchaseGame()
        {
            if (AccountHandler.Account != null)
            {
                foreach (Game gamePurchase in Games)
                {
                    AccountHandler.AccountDetail.GamesOwned.Add(gamePurchase);
                    AccountHandler.AccountDetail.AddPurchaseToPurchaseHistory(gamePurchase.Name, gamePurchase.Price, DateTime.Now, gamePurchase.Identifier);
                }
                AccountHandler.AccountDetail.AccountShoppingCart.Clear();
                AccountHandler.AccountDetail.CreateUserDetailsFile(AccountHandler.AccountDetail, AccountHandler.Account.UserName);
                ClearShoppingCart();
            }
        }

        /// <summary>
        /// Method for calculating the total price of all games in the shopping cart
        /// </summary>
        /// <returns>The price of all games in the shopping cart</returns>
        public float GetCurrentPrice()
        {
            float price = 0;
            foreach (Game game in _games)
            {
               price += game.Price - (game.Price *  ((float)game.CurrentDiscountPercentage / 100));
            }
            return price;
        }

        /// <summary>
        /// Method for clearing the shopping cart and updating the total price
        /// </summary>
        public void ClearShoppingCart()
        {
            Games.Clear();
            GetCurrentPrice();
        }
        #endregion

        #region Properties
        public MainPageVM MainPageVm { get; set; }

        public static ShoppingCart Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ShoppingCart();
                }
                return instance;
            }
        }

        public ObservableCollection<Game> Games
        {
            get { return _games; }
        }

        public float TotalPrice
        {
            get { return GetCurrentPrice(); }
        }
        #endregion
    }
}
