using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainWindow.Models
{
    public class AccountPurchase
    {
        private string _gameName;
        private float _gamePrice;
        private DateTime _purchaseDate;
        private string _identifier;

        public AccountPurchase(string gameName, float gamePrice, DateTime purchaseDate, string identifier)
        {
            _gameName = gameName;
            _gamePrice = gamePrice;
            _purchaseDate = purchaseDate;
            _identifier = identifier;
        }

        public string GameName { get { return _gameName; } set { _gameName = value; } }
        public float GamePrice { get { return _gamePrice; } set { _gamePrice = value; } }
        public DateTime PurchaseDate { get { return _purchaseDate; } set { _purchaseDate = value; } }
        public string Identifier { get { return _identifier; } set { _identifier = value; } }
    }
}
