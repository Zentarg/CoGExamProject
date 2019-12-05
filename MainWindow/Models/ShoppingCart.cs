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

        public float GetCurrentPrice()
        {
            float totalPrice = 0;

            foreach (Game game in _games)
            {
                totalPrice += game.Price * game.CurrentDiscountPercentage;
            }

            return totalPrice;
        }

    }
}
