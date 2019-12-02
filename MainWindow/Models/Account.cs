using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainWindow.Models
{
    class Account
    {
        private string _userName;
        private string _passWord;
        //private Library _library;
        //private ShoppingCart _shoppingCart;
       // private string _profilePicturePath;
        //private AccountStatistics _accountStatistics;
        private string _displayName;

        public Account(string userName, string passWord, string displayName)
        {
            _userName = userName;
            _passWord = passWord;
            //_profilePicturePath = profilePicturePath;
            _displayName = displayName;
        }

        #region Properties
        public string UserName { get { return _userName; } set { _userName = value; } }
        public string PassWord { get { return _passWord; } set { _passWord = value; } }
        //public string ProfilePicturePatch { get; set; }
        public string DisplayName { get { return _displayName; } set { _displayName = value; } }


        #endregion

        #region Methods


        #endregion

    }
}
