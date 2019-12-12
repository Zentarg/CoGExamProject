using LinqToDB;
using MainWindow.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace MainWindow.Models
{
    public static class AccountHandler 
    {
        private static Account _account;

        private static AccountLists _accountList = AccountLists.AccountListInstance;

        private static string _displaynameForUI;

        private static AccountDetails _accountDetails;

        private static DateTime _joinDateBeforeFormat;

        static  AccountHandler()
        {
            
            _account = null;
            _accountDetails = null;
            
        }

        public static MainPageVM MainPageVm { get; set; }
        public static StoreVM StoreVm { get; set; }
        public static GameTemplateVM GameTemplateVm { get; set; }

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

        public static AccountDetails AccountDetail { 
            get { return _accountDetails; } 
            set { _accountDetails = value; } 
        }


        public static async void Login(string username, string password)
        {
            foreach (Account account in _accountList.AccountList)
            {
                if(username == account.UserName && password == account.PassWord)
                {
                    _account = new Account(account.UserName, account.PassWord, account.DisplayName);
                    SetDisplayNameForUI = _account.DisplayName;
                    AccountDetail = new AccountDetails();
                    _accountDetails = await AccountDetail.LoadUserDetailsFile(username);
                    MainPageVm?.CallForAccountStatus();
                    StoreVm?.OnAccountChanged();
                    GameTemplateVm?.OnAccountChanged();
                    /*foreach (Game game in AccountDetail.AccountShoppingCart.Games)
                    {
                        ShoppingCart.Instance.AddGame(game);
                    }*/
                    
                    break;
                }
            }
        }

        public static void LogOff()
        {
            _accountDetails.CreateUserDetailsFile(_accountDetails, _account.UserName);

            //Bellow needs implementing without errors
            /*foreach (Game game in AccountDetail.AccountShoppingCart.Games)
            {
                ShoppingCart.Instance.RemoveGame(game);
            }*/


            _account = null;
            _displaynameForUI = null;
            _accountDetails = null;
            MainPageVm?.CallForAccountStatus();
            StoreVm?.OnAccountChanged();
            GameTemplateVm?.OnAccountChanged();
        }

        public static void CreateAccount(Account account)
        {

            _accountList.AccountList.Add(account);
            SetDisplayNameForUI = _account.DisplayName;
            AccountDetail = new AccountDetails();
            _accountDetails.DisplayName = account.DisplayName;
            _accountDetails.UserName = account.UserName;
            _joinDateBeforeFormat = DateTime.Now;
            _accountDetails.JoinDate = _joinDateBeforeFormat.Date.ToShortDateString();
            _accountDetails.CreateUserDetailsFile(_accountDetails, account.UserName);
            MainPageVm?.CallForAccountStatus();
            StoreVm?.OnAccountChanged();
            GameTemplateVm?.OnAccountChanged();

            //_accountDetails = new AccountDetails(_account.UserName);
        }

        public static string SetDisplayNameForUI
        {
            get { return _displaynameForUI; }
            set { _displaynameForUI = value; }
        }


        #region Check Username, password and display name methods

        private static bool UsernameinUseCheck(string _string)
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

        private static bool DoesPasswordHaveMatchToUsername(string username, string password)
        {
            foreach (Account acc in _accountList.AccountList)
            {
                if (acc.UserName == username)
                {
                    //If there is a username in the account list that matches the entered username, check if has matching password
                    if (acc.PassWord == password)
                    {
                        //Return true if matching password was found
                        return true;
                    }
                }
            }
            //Returns false if there was no username password match
            return false;
        }

        private static bool StringContainsSpecialChar(string _string)
        {
            //Checks for if there is any special characters in the username and returns true or false depending
            return _string.Any(ch => !char.IsLetterOrDigit(ch));
        }

        private static bool StringDoesNotContainOneNumberAndOneCapitalLetter(string _string)
        {
            //Checks the entered password to see if it does not contain at least 1 number and 1 letter
            if (!_string.Any(ch => char.IsDigit(ch)) == true || !_string.Any(ch => char.IsUpper(ch)) == true)
            {
                return true;
            }
            else
            {
                return false;
            }           
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

        private static int DisplayNameLengthCheck(string _string)
        {
            if (_string.Length < 1)
            {
                //When the length of the display name is less than 1, return 2
                return 2;
            }
            else if (_string.Length > 32)
            {
                //When the length of the display name is greater than 32, return 1
                return 1;
            }
            else
            {
                //When the length of the display name is within the range of 1 <-> 32, return 0
                return 0;
            }
        }

        private static bool DisplayNameNumberinUseCheck(string _string)
        {
            foreach (Account acc in _accountList.AccountList)
            {
                if (acc.DisplayName == _string)
                {
                    //returns true if there is a display name in the account list that matches the entered display name
                    return true;
                }
            }
            //returns false if the display name that was entered was not found in the account list
            return false;
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
                        if (UsernameinUseCheck(username) == true)
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

        public static string UserNameResultStringForCreateAccount(int resultFromCheck)
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
            return "The entered username is ok";
        }

        public static string UserNameResultStringForLogin(int resultFromCheck)
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
            else if (resultFromCheck == 0)
            {
                return "The entered username does not exist";
            }
            return "The entered username is exists";
        }



        public static int PasswordCheck(string password)
        {
            if (password != null)
            {
                if (StringContainsSpecialChar(password) == true)
                {
                    //If the password contains special characters, return 4

                    return 4;
                }
                else
                {
                    //Run this block if the password doesnt contain special characters
                    if (StringLengthCheck(password) == 2)
                    {
                        //If the password's length is less than 8, return 3

                        return 3;
                    }
                    else if (StringLengthCheck(password) == 1)
                    {
                        //Return 2 if the password's length is greater than 16

                        return 2;
                    }
                    else
                    {
                        //Run this block if the length is fine
                        if (StringDoesNotContainOneNumberAndOneCapitalLetter(password) == true)
                        {
                            //If the password does not contain at least 1 number and 1 capital letter

                            return 1;
                        }
                        else
                        {
                            //If the password is fine, return 0
                            return 0;
                        }
                    }
                }
            }
            //Default colour for enter password box, if the string is empty
            return 5;
        }

        public static int PasswordCheckForLogin(string password, string username)
        {
            if (password != "")
            {
                if (StringContainsSpecialChar(password) == true)
                {
                    //If the password contains special characters, return 4

                    return 2;
                }
                else
                {
                    //Checks the username to password combination to see if there is a match in the account list, return 0 if yes, return 1, if no
                    if (DoesPasswordHaveMatchToUsername(username, password) == true)
                    {
                        return 0;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            //Default colour for enter password box, if the string is empty
            return 3;
        }

        public static string PasswordResultString(int resultFromCheck)
        {
            if (resultFromCheck == 4)
            {
                return "The entered password contains special Characters.";
            }
            else if (resultFromCheck == 3)
            {
                return "The entered password contains less than 8 Characters.";

            }
            else if (resultFromCheck == 2)
            {
                return "The entered password contains more than 16 Characters.";

            }
            else if (resultFromCheck == 1)
            {
                return "The entered password does not contain at least 1 number and 1 capital letter";
            }
            return "The entered password is ok";
        }

        public static string PasswordResultStringForLogin(int resultFromCheck)
        {
            if (resultFromCheck == 2)
            {
                return "The entered password contains special Characters.";
            }
            else if (resultFromCheck == 1)
            {
                return "The entered password was incorrect";
            }

            return "The entered password is ok";
        }



        public static int DisplayNameCheck(string displayname)
        {
            if (displayname != "")
            {
                if (StringContainsSpecialChar(displayname))
                {
                    return 3;
                }
                else
                {
                    if (DisplayNameLengthCheck(displayname) == 2)
                    {
                        return 2;
                    }
                    else if (DisplayNameLengthCheck(displayname) == 1)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            return 4;
        }

        public static string DisplayNameResultStrng(int resultFromCheck)
        {
            if (resultFromCheck == 3)
            {
                return "The entered display name contains special Characters.";
            }
            else if (resultFromCheck == 2)
            {
                return "The entered display name contains less than 1 Character.";
            }
            else if (resultFromCheck == 1)
            {
                return "The entered display name contains more than 32 Characters.";

            }
            return "The entered display name is ok";
        }

        public static string DisplayNameAddTag(string displayname)
        {
            //Creates and adds a random number tag to a display name, continously checks if it is an available tag for that name until it finds one.
            Random numberGenerator = new Random();
            int randomInt = numberGenerator.Next(0, 10000);

            string identifier = randomInt.ToString();
            for (int i = 4; i > identifier.Length; i = i)
            {
                identifier = "0" + identifier;
            }

            string _tempDisplayname = displayname;
            displayname += "#" + identifier;

            if (DisplayNameNumberinUseCheck(displayname) == true)
            {
                displayname = _tempDisplayname;
                return DisplayNameAddTag(displayname);
            }
            else
            {
                return displayname;
            }
        }

        public static int ConfirmPasswordCheck(string confirmPassword, string password)
        {
            if (confirmPassword != null)
            {
                if (confirmPassword != password)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 2;
            }
        }

        public static string PasswordConfirmResultString(int resultFromCheck)
        {
            if (resultFromCheck == 1)
            {
                return "The entered password does not match";
            }
            return "The entered password matches";
        }

        public static int PasswordCheckVersusOldPassword(string password)
        {
            if (password != null)
            {
                if (password != Account.PassWord)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            return 2;
        }

        public static int EnteredNewPasswordMatchesEnteredOldPassword(string newpassword)
        {
            if (newpassword != null)
            {
                if (StringContainsSpecialChar(newpassword) == true)
                {


                    return 5;
                }
                else
                {

                    if (StringLengthCheck(newpassword) == 2)
                    {


                        return 4;
                    }
                    else if (StringLengthCheck(newpassword) == 1)
                    {


                        return 3;
                    }
                    else
                    {

                        if (StringDoesNotContainOneNumberAndOneCapitalLetter(newpassword) == true)
                        {


                            return 2;
                        }
                        else
                        {
                            if (newpassword == Account.PassWord)
                            {
                                return 1;
                            }
                            else
                            {
                                return 0;
                            }
                        }
                    }
                }
            }

            return 6;
        }

        public static string EnteredNewPasswordMatchesOldPasswordResultFromString(int resultFromCheck)
        {
            if (resultFromCheck == 5)
            {
                return "The entered password contains special Characters.";
            }
            else if (resultFromCheck == 4)
            {
                return "The entered password contains less than 8 Characters.";

            }
            else if (resultFromCheck == 3)
            {
                return "The entered password contains more than 16 Characters.";

            }
            else if (resultFromCheck == 2)
            {
                return "The entered password does not contain at least 1 number and 1 capital letter";
            }
            else if (resultFromCheck == 1)
            {
                return "The entered password cannot be the same as the old one";
            }
            return "The entered password is ok";
        }

        public static BitmapImage ReturnImagePathConfirmPassword(int resultFromCheck)
        {
            if (resultFromCheck == 1)
            {
                return new BitmapImage(new Uri("ms-appx:///Assets/RedX.png"));
            }
            else if (resultFromCheck == 0)
            {
                return new BitmapImage(new Uri("ms-appx:///Assets/GreenCheck.png"));
            }
            return null;
        }

        public static BitmapImage ReturnImagePathUsernamePassword(int resultFromCheck)
        {
            if(resultFromCheck == 4 || resultFromCheck == 3 || resultFromCheck == 2 || resultFromCheck == 1)
            {
                return new BitmapImage(new Uri("ms-appx:///Assets/RedX.png"));
            }
            else if(resultFromCheck == 0)
            {
                return new BitmapImage(new Uri("ms-appx:///Assets/GreenCheck.png"));
            }
            return null;
        }

        public static BitmapImage ReturnImagePathChangePassword(int resultFromCheck)
        {
            if (resultFromCheck == 4 || resultFromCheck == 3 || resultFromCheck == 2 || resultFromCheck == 1 || resultFromCheck == 5)
            {
                return new BitmapImage(new Uri("ms-appx:///Assets/RedX.png"));
            }
            else if (resultFromCheck == 0)
            {
                return new BitmapImage(new Uri("ms-appx:///Assets/GreenCheck.png"));
            }
            return null;
        }

        public static BitmapImage ReturnImagePathUsernameForLogin(int resultFromCheck)
        {
            if (resultFromCheck == 4 || resultFromCheck == 3 || resultFromCheck == 2 || resultFromCheck == 0)
            {
                return new BitmapImage(new Uri("ms-appx:///Assets/RedX.png"));
            }
            else if (resultFromCheck == 1)
            {
                return new BitmapImage(new Uri("ms-appx:///Assets/GreenCheck.png"));
            }
            return null;
        }

        public static BitmapImage ReturnImagePathPasswordForLogin(int resultFromCheck)
        {
            if (resultFromCheck == 2 || resultFromCheck == 1)
            {
                return new BitmapImage(new Uri("ms-appx:///Assets/RedX.png"));
            }
            else if (resultFromCheck == 0)
            {
                return new BitmapImage(new Uri("ms-appx:///Assets/GreenCheck.png"));
            }
            return null;
        }

        public static BitmapImage ReturnImagePathDisplayName(int resultFromCheck)
        {
            if (resultFromCheck == 3 || resultFromCheck == 2 || resultFromCheck == 1)
            {
                return new BitmapImage(new Uri("ms-appx:///Assets/RedX.png"));
            }
            else if (resultFromCheck == 0)
            {
                return new BitmapImage(new Uri("ms-appx:///Assets/GreenCheck.png"));
            }
            return null;
        }


        public static void ChangeDisplayNameInAccountList(string term)
        {
            foreach(Account account in _accountList.AccountList)
            {
                if(_account.UserName == account.UserName)
                {
                    account.DisplayName = term;
                    break;
                }
            }
        }

        public static void ChangeUserPassword(string password)
        {
            foreach (Account acc in _accountList.AccountList)
            {
                if(_account.UserName == acc.UserName)
                {
                    acc.PassWord = password;
                }
            }
        }
        #endregion
    }
}
