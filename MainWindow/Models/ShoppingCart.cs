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
            _games.Add(game);
        }

        public void AddGame(ObservableCollection<Game> _games)
        {
            foreach (Game game in _games)
            {
                _games.Add(game);
            }
        }

        public void RemoveGame(Game game)
        {
            _games.Remove(game);
        }

        public void RemoveGame(ObservableCollection<Game> _games)
        {
            foreach (Game game in _games)
            {
                _games.Remove(game);
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
                    instance = new ShoppingCart();
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
