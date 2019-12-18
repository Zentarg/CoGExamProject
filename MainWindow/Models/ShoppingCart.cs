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
    public class ShoppingCart
    {
        private ObservableCollection<Game> _games;
        private static ShoppingCart instance;


        private ShoppingCart()
        {
            _games = new ObservableCollection<Game>();
        }


        public void AddGame(Game game)
        {
            Games.Add(game);
            if (AccountHandler.Account != null)
            {
                AccountHandler.AccountDetail.AccountShoppingCart.Add(game);
            }
        }

        public void AddGame(ObservableCollection<Game> games)
        {
            foreach (Game game in games)
            {
                Games.Add(game);
            }            
        }

        public void RemoveGame(Game game)
        {
            Games.Remove(game);
            if (AccountHandler.Account != null)
            {
                AccountHandler.AccountDetail.AccountShoppingCart.Remove(game);
            }
        }


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

        public float GetCurrentPrice()
        {
            float price = 0;
            foreach (Game game in _games)
            {
               price += game.Price - (game.Price *  ((float)game.CurrentDiscountPercentage / 100));
            }
            return price;
        }

        public void ClearShoppingCart()
        {
            Games.Clear();
            GetCurrentPrice();
        }
    }
}
