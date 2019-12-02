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
        //private string _displayName;

        public Account(string userName, string passWord)
        {
            _userName = userName;
            _passWord = passWord;
            //_profilePicturePath = profilePicturePath;
            //_displayName = displayName;
        }

        #region Properties
        public string UserName { get; set; }
        public string PassWord { get; set; }
        //public string ProfilePicturePatch { get; set; }
        //public string DisplayName { get; set; }


        #endregion

        #region Methods


        #endregion

    }
}
