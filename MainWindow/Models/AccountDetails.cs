using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainWindow.Models
{
    public class AccountDetails
    {
        private string _userName;
        //private Library _library;
        private ShoppingCart _shoppingCart;
        // private string _profilePicturePath;
        //private AccountStatistics _accountStatistics;
        private string _displayName;

        public AccountDetails(string userName)
        {
            _userName = userName;
            //_profilePicturePath = profilePicturePath;
        }

        #region Properties
        public string UserName { get { return _userName; } set { _userName = value; } }
        //public string ProfilePicturePatch { get; set; }
        public string DisplayName { get { return _displayName; } set { _displayName = value; } }
        public ShoppingCart AccountShoppingCart { get { return _shoppingCart; } set { _shoppingCart = value; } }


        #endregion

        #region Methods


        #endregion
    }
}
