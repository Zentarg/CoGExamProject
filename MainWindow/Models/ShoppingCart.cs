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
        private float _totalPrice;

        private ShoppingCart()
        {
            _games = new ObservableCollection<Game>();
            _totalPrice = TotalPrice;
        }


        public void AddGame(Game game)
        {
            Games.Add(game);
            if (AccountHandler.Account != null)
            {
                AccountHandler.AccountDetail.AccountShoppingCart = this;
            }
        }

        public void AddGame(ObservableCollection<Game> games)
        {
            foreach (Game game in games)
            {
                Games.Add(game);
            }
            if (AccountHandler.Account != null)
            {
                AccountHandler.AccountDetail.AccountShoppingCart = this;
            }
        }

        public void RemoveGame(Game game)
        {
            Games.Remove(game);
            if (AccountHandler.Account != null)
            {
                AccountHandler.AccountDetail.AccountShoppingCart = this;
            }
        }


        public void RemoveGame(ObservableCollection<Game> games)
        {
            foreach (Game game in games)
            {
                Games.Remove(game);
            }
            if (AccountHandler.Account != null)
            {
                AccountHandler.AccountDetail.AccountShoppingCart = this;
            }
        }

        public void PurchaseGame()
        {
               
        }

        public static ShoppingCart Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ShoppingCart();
                    
                }
                else if (AccountHandler.Account != null)
                {
                    instance = AccountHandler.AccountDetail.AccountShoppingCart;
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
    }
}
