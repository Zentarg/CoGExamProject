using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainWindow.Models
{
     public class ShoppingCart
    {
        private ObservableCollection<Game> _games;


        public void AddGame(Game _game)
        {
            _games.Add(_game);
        }

        public void AddGame(ObservableCollection<Game> _games)
        {
            foreach (Game game in _games)
            {
                _games.Add(game);
            }
        }

        public void RemoveGame(Game _game)
        {
            _games.Remove(_game);
        }

        public void RemoveGame(ObservableCollection<Game> _games)
        {
            foreach (Game game in _games)
            {
                _games.Remove(game);
            }
        }

        public ObservableCollection<Game> GetGames()
        {
            return _games;
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
