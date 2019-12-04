using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainWindow.Models
{
    public static class AccountHandler
    {
        private static Account _account;
        private static AccountLists _accountList = AccountLists.AccountListInstance;

        static  AccountHandler()
        {
            
            _account = new Account("", "", "");
        }

        public static AccountLists AccountList
        {
            get
            {
                return _accountList;
            }
            set
            {
                _accountList = value;
            }
        }

        public static Account Account
        {
            get
            {
                return _account;
            }
            set
            {
                _account = value;
            }
        }

        public static int UserNameCharCheck(string username)
        {
            if(username != "")
            {
                foreach (Account acc in _accountList.AccountList)
                {
                    if (acc.UserName == username)
                    {
                        return 2;
                    }
                }
                return 1;
            }
            return 0;
        }
    }
}
