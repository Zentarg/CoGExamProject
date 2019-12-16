using MainWindow.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using System.Collections.ObjectModel;


namespace MainWindow
{
    /// <summary>
    /// A singleton class, which insures that it is the same object being dealt with everywhere, that stores all Account objects and handles file writing for them
    /// </summary>
    public class AccountLists
    {
        
        private static ObservableCollection<Account> _accountLists = new ObservableCollection<Account>();
        private static AccountLists accountListInstance = new AccountLists();

        static AccountLists() { }
        private AccountLists() { }

        /// <summary>
        /// Returns the singleton instance of this class when called
        /// </summary>
        public static AccountLists AccountListInstance
        {
            get { return accountListInstance; }
        }


        /// <summary>
        /// Through the instance, you can get the Account List, which returns the list of accounts
        /// </summary>
        public ObservableCollection<Account> AccountList
        {
            get { return _accountLists; }
        }

        /// <summary>
        /// Loads all the accounts from the accountlist.json file
        /// </summary>
        public async void LoadAccounts()
        {
            if (FileHandler.FileExists(Constants.AccountListFileName)) 
            {
                string accounts = await FileHandler.ReadFile(Constants.AccountListFileName);
                if (accounts != "")
                {
                    _accountLists = JsonConvert.DeserializeObject<ObservableCollection<Account>>(accounts);
                }   
            }     
        }

        /// <summary>
        /// Writes the current account list instance to the file accountlist.json over writing what was there
        /// </summary>
        public void AddAccountToFile()
        {
            FileHandler.WriteFile(Constants.AccountListFileName, _accountLists);
        }
    }
}


