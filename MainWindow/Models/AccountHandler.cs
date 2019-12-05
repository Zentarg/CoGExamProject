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


        private static bool StringinUseCheck(string _string)
        {
            foreach (Account acc in _accountList.AccountList)
            {
                if (acc.UserName == _string)
                {
                    //returns true if there is a username in the account list that matches the entered username
                    return true;
                }
            }
            //returns false if the username that was entered was not found in the account list
            return false;
        }

        private static bool StringContainsSpecialChar(string _string)
        {
            //Checks for if there is any special characters in the username and returns true or false depending
            return _string.Any(ch => !char.IsLetterOrDigit(ch));
        }

        private static int StringLengthCheck(string _string)
        {
            if (_string.Length < 8)
            {
                //When the length of the username is less than 8, return 2
                return 2;
            }
            else if (_string.Length > 16)
            {
                //When the length of the username is greater than 16, return 1
                return 1;
            }
            else
            {
                //When the length of the username is within the range of 8 <-> 16, return 0
                return 0;
            }
        }

        public static int UserNameCheck(string username)
        {
            if (username != "")
            {
                if (StringContainsSpecialChar(username) == true)
                {
                    //If the username contains special characters, return 4
                    
                    return 4;
                }
                else
                {
                    //Run this block if the username doesnt contain special characters
                    if (StringLengthCheck(username) == 2)
                    {
                        //If the username's length is less than 8, return 3
                        
                        return 3;
                    }
                    else if (StringLengthCheck(username) == 1)
                    {
                        //Return 2 if the username's length is greater than 16

                        return 2;
                    }
                    else
                    {
                        //Run this block if the length is fine
                        if (StringinUseCheck(username) == true)
                        {
                            //If the username is in use, return 1

                            return 1;
                        }
                        else
                        {
                            //If the username is not in use, return 0
                            return 0;
                        }
                    }
                }
            }
            //Default colour for enter username box, if the string is empty
            return 5;
        }

        public static string UserNameResultString(int resultFromCheck)
        {
            if (resultFromCheck == 4)
            {
                return "The entered username contains special Characters.";


            }
            else if (resultFromCheck == 3)
            {
                return "The entered username contains less than 8 Characters.";

            }
            else if (resultFromCheck == 2)
            {
                return "The entered username contains more than 16 Characters.";

            }
            else if (resultFromCheck == 1)
            {
                return "The entered username is already in use";

            }
            return "";
        }
    }
}
