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
        #region Instance Fields
        private static Account _account;
        private static AccountLists _accountList = AccountLists.AccountListInstance;
        private static string _displaynameForUI;
        private static AccountDetails _accountDetails;
        private static DateTime _joinDateBeforeFormat;
        #endregion

        #region Constructor
        static AccountHandler()
        {  
            _account = null;
            _accountDetails = null;  
        }
        #endregion

        #region Properties
        public static MainPageVM MainPageVm { get; set; }
        public static StoreVM StoreVm { get; set; }
        public static GameTemplateVM GameTemplateVm { get; set; }
        public static AccountLoginVM ALVM { get; set; }

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

        public static string SetDisplayNameForUI
        {
            get { return _displaynameForUI; }
            set { _displaynameForUI = value; }
        }

        public static string SetProfileImagePathForUI { get; set; }
        #endregion


        #region Methods pertaining to login, account creation and logoff
        /// <summary>
        /// Method that logs a user into the system
        /// </summary>
        /// <param name="username">A username string that is passed to this method</param>
        /// <param name="password">A password string that is passed to this method</param>
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
                    SetProfileImagePathForUI = _accountDetails.ProfilePicturePath;
                    MainPageVm?.CallForAccountStatus();
                    StoreVm?.OnAccountChanged();
                    GameTemplateVm?.OnAccountChanged();
                    if (AccountDetail.AccountShoppingCart != null)
                    {
                        foreach (Game game in AccountDetail.AccountShoppingCart.Games)
                        {
                            ShoppingCart.Instance.AddGame(game);
                        }
                    }
                    break;
                }
            }
        }

        /// <summary>
        /// Logs a user off the system and sets objects to null
        /// </summary>
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
            SetProfileImagePathForUI = null;
            MainPageVm?.CallForAccountStatus();
            StoreVm?.OnAccountChanged();
            GameTemplateVm?.OnAccountChanged();
        }

        /// <summary>
        /// Creates an account in the system and writes the account to the files
        /// </summary>
        /// <param name="account">An account object that is passed</param>
        /// <param name="pfpPath">A string that is passed that links an image to the account</param>
        public static void CreateAccount(Account account, string pfpPath)
        {

            _accountList.AccountList.Add(account);
            SetDisplayNameForUI = _account.DisplayName;
            AccountDetail = new AccountDetails();
            _accountDetails.DisplayName = account.DisplayName;
            _accountDetails.UserName = account.UserName;
            _accountDetails.ProfilePicturePath = pfpPath;
            SetProfileImagePathForUI = _accountDetails.ProfilePicturePath;
            _joinDateBeforeFormat = DateTime.Now;
            _accountDetails.JoinDate = _joinDateBeforeFormat.Date.ToShortDateString();
            _accountDetails.CreateUserDetailsFile(_accountDetails, account.UserName);
            MainPageVm?.CallForAccountStatus();
            StoreVm?.OnAccountChanged();
            GameTemplateVm?.OnAccountChanged();
        }
        #endregion

        #region Check Username, password and display name methods
        /// <summary>
        /// Method for checking if a username is in use
        /// </summary>
        /// <param name="_string">The passed argument that it uses to check if the username is in use</param>
        /// <returns>true if the passed username exists in _accountList.AccountList, otherwise returns false</returns>
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

        /// <summary>
        /// Method for checking if a passed username exists in the accountList and if the passsword passed is equal to that username in accountList
        /// </summary>
        /// <param name="username">Username used to check through the accounts in accountList</param>
        /// <param name="password">Password used to check against the account found with a matching username</param>
        /// <returns>True if a username/password match was found, false if none</returns>
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

        /// <summary>
        /// Checks to see if a string passed to it contains any special characters, returns bool
        /// </summary>
        /// <param name="_string">The passed string that gets checked</param>
        /// <returns>returns true if a special character was found, otherwise false</returns>
        private static bool StringContainsSpecialChar(string _string)
        {
            //Checks for if there is any special characters in the username and returns true or false depending
            return _string.Any(ch => !char.IsLetterOrDigit(ch));
        }

        /// <summary>
        /// Method for checking if the entered string does not contain a number and a capital letter, returns bool
        /// </summary>
        /// <param name="_string">String that is passed for the check</param>
        /// <returns>returns true if either it doesnt contain a capital letter or a number, returns false if it contains both</returns>
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


        /// <summary>
        /// Checks the passed string to see if it is within a certain length, returns int (mainly used for checking password and username length)
        /// </summary>
        /// <param name="_string">The passed string to be checked</param>
        /// <returns>Returns 2 if the string is less than 8, returns 1 if the string is greater than 16, returns 0 if it is in between 8 and 16</returns>
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

        /// <summary>
        /// Checks the length of a passed string to see if it is within a certain length, returns int
        /// </summary>
        /// <param name="_string">String passed to the method to be checked (displayname)</param>
        /// <returns>returns 2 if string is less than 1, returns 1 if string is greater than 32, returns 0 if the string is in between 1 and 32</returns>
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

        /// <summary>
        /// Method for checking if a displayname with a tag provided already exists or not
        /// </summary>
        /// <param name="_string">The passed string to be checked against each Account.Displayname in accountList</param>
        /// <returns>returns true if there is a match, otherwise returns false</returns>
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

        /// <summary>
        /// Check the username amongst various conditions 
        /// </summary>
        /// <param name="username">a username string is passed for checking</param>
        /// <returns>returns 4 if it contains special characters, 3 if the length is less than 8, 2 if the lenght is greater than 16, 1 if the username is in use, 0 if the username is not in use, 5 for default (empty string/username passed)</returns>
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

        /// <summary>
        /// A method for determining what string to return when trying to create an account (for username)
        /// </summary>
        /// <param name="resultFromCheck">Takes an Int, supposed to be obtained from UserNameCheck()</param>
        /// <returns>returns different string for values 1,2,3,4</returns>
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

        /// <summary>
        /// A method for determing what string to return when trying to login to an account (for username)
        /// </summary>
        /// <param name="resultFromCheck">Takes an Int, supposed to be obtained from UserNameCheck()</param>
        /// <returns>returns a different string for values 0,2,3,4</returns>
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
            return "Ok";
        }

        /// <summary>
        /// Same as UserNameCheck() except for Passwords (checks if the passed password passes certain conditions) returns int
        /// </summary>
        /// <param name="password">Takes a password string and tests it</param>
        /// <returns>returns 4 if the password contains special characters. Returns 3 if the password length is less than 8. Returns 2 if the length is greater than 16. Returns 1 if the password does not contain at least 1 capital letter and 1 number. Returns 0 if the password is fine. Returns 5 if the passed password was empty (default)</returns>
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

        /// <summary>
        /// A method for determining if a password has a match, used for login
        /// </summary>
        /// <param name="password">A passed password string</param>
        /// <param name="username">A passed username string</param>
        /// <returns>returns 2 if the password contains special characters, 1 if the passed password doesnt match the username, return 0 if it does match, return 3 if the password is empty</returns>
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

        /// <summary>
        /// A method for returning a string depending on the integer passed to it, used for account creation and changing password
        /// </summary>
        /// <param name="resultFromCheck">The int passed to the method that determines what string to return</param>
        /// <returns>Returns a string for 1,2,3,4, if none of those were entered, return default string</returns>
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
            //default string
            return "The entered password is ok";
        }

        /// <summary>
        /// A method for returning a string depending on the int passed to it, used for account login
        /// </summary>
        /// <param name="resultFromCheck"> the int passed to the method that determines the return</param>
        /// <returns>returns a string for 1, 2, and a default string if 1 or 2 wasnt passed</returns>
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
            //default string
            return "The entered password is ok";
        }

        /// <summary>
        /// A method for checking the display name against certain criterion, returns int
        /// </summary>
        /// <param name="displayname">the display name string passed to the method for checking</param>
        /// <returns>returns 3 if the display name contains special characters, returns 2 if the length is below 1, returns 1 if the length is greater than 32, returns 0 if it is inbetween 1 and 32, returns 4 as a default if the display name is an empty string</returns>
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
            //Default
            return 4;
        }

        /// <summary>
        /// A method for returning a string relevant to displayname checks that takes an int, gotten from DisplayNameCheck() 
        /// </summary>
        /// <param name="resultFromCheck">The int passed to the method</param>
        /// <returns>Returns a string for 3,2,1, and a default string otherwise</returns>
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
            //default
            return "The entered display name is ok";
        }

        /// <summary>
        /// Method for adding a tag to a given displayname, then returns that displayname string
        /// </summary>
        /// <param name="displayname">The displayname that it takes and returns</param>
        /// <returns>Returns a displayname with a #xxxx tag appended to the end, the tag is unique for that displayname. Thus it checks against all display names until it finds itself being unique (either becuase of recursion, or it was unique from the start)</returns>
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

        /// <summary>
        /// A method that checks two passed password strings to see if they are the same
        /// </summary>
        /// <param name="confirmPassword">The password used for confirming they are the same</param>
        /// <param name="password">The first password</param>
        /// <returns>returns 0 if the two passwords match, 1 if they dont, 2 as a default value</returns>
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
                //default
                return 2;
            }
        }

        /// <summary>
        /// A method that returns a string depending on the number passed, used for checking if two passwords are the same (ConfirmPasswordCheck())
        /// </summary>
        /// <param name="resultFromCheck">Passed int that is used for getting a string</param>
        /// <returns>returns a string for 1 and a default string for any value</returns>
        public static string PasswordConfirmResultString(int resultFromCheck)
        {
            if (resultFromCheck == 1)
            {
                return "The entered password does not match";
            }
            //default
            return "The entered password matches";
        }

        /// <summary>
        /// A method for checking if the entered password is the same as the old one (used for changing passwords)
        /// </summary>
        /// <param name="password">the new password that is to be checked against the old one</param>
        /// <returns>returns 1 if the password does not match the currently logged in account's account.password, returns 1 if it does, 2 as a default</returns>
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
            //default
            return 2;
        }

        /// <summary>
        /// A method for checking a passed password string against several criterion
        /// </summary>
        /// <param name="newpassword">The passed password string</param>
        /// <returns>returns 5 if it contains special characters, returns 4 if the length is less than 8, returns 3 if the length is greater than 16, returns 2 if the password does not contain at least 1 upper case letter and 1 number, returns 1 if the entered password string matches the old one, returns 0 if not, otherwise it returns 6 as a default</returns>
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
            //default
            return 6;
        }

        /// <summary>
        /// A method that takes an int and returns a string depending on the passed int (used for changing password)
        /// </summary>
        /// <param name="resultFromCheck">The int passed</param>
        /// <returns>returns a string for 1,2,3,4,5 and a default string for any other value</returns>
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

        /// <summary>
        /// A method that returns an image depending on the int passed to it (for confirm password)
        /// </summary>
        /// <param name="resultFromCheck">Int passed for getting the image</param>
        /// <returns>Returns the red x for 1 and green check for 0</returns>
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

        /// <summary>
        /// A method that returns an image depending on the int passed to it (for username and password checks in account creation)
        /// </summary>
        /// <param name="resultFromCheck">Int passed for getting the image</param>
        /// <returns>returns red x for 4, 3, 2, 1, and a green check for 0</returns>
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

        /// <summary>
        /// A method that returns an image depending on the int passed to it (for change password)
        /// </summary>
        /// <param name="resultFromCheck">Int passed for getting the image</param>
        /// <returns>Returns red x for 4, 3, 2, 1, 5, returns green check for 0</returns>
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

        /// <summary>
        /// A method that returns an image depending on the int passed to it (for username login)
        /// </summary>
        /// <param name="resultFromCheck">Int passed for getting the image</param>
        /// <returns>returns a red x for 4, 3, 2, 0, returns a green check for 1</returns>
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

        /// <summary>
        /// A method that returns an image depending on the int passed to it (for password login)
        /// </summary>
        /// <param name="resultFromCheck">Int passed for getting the image</param>
        /// <returns>returns a red x for 2, 1, returns green check for 0</returns>
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

        /// <summary>
        ///  A method that returns an image depending on the int passed to it (for display name account creation and change)
        /// </summary>
        /// <param name="resultFromCheck">Int passed for getting the image</param>
        /// <returns>returns red x for 3, 2, 1, returns green check for 0</returns>
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

        /// <summary>
        /// A method for changing the display name in the account list, for the logged in account.
        /// </summary>
        /// <param name="term">term is the new display name string passed to the method</param>
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

        /// <summary>
        /// A method that changes the password in the account list, for the logged in account
        /// </summary>
        /// <param name="password">the new password that the old one will be over written with</param>
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
