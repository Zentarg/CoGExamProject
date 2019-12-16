using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainWindow.Models
{
    public class Account
    {
        #region Instance Fields
        private string _userName;
        private string _passWord;
        private string _displayName;
        #endregion

        #region Constructor
        public Account(string userName, string passWord, string displayName)
        {
            _userName = userName;
            _passWord = passWord;
            _displayName = displayName;
        }
        #endregion

        #region Properties
        public string UserName { get { return _userName; } set { _userName = value; } }
        public string PassWord { get { return _passWord; } set { _passWord = value; } }
        public string DisplayName { get { return _displayName; } set { _displayName = value; } }
        #endregion
    }
}
