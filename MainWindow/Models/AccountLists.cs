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
    public class AccountLists
    {
        private static ObservableCollection<Account> _accountLists = new ObservableCollection<Account>();
        private static AccountLists accountListInstance = new AccountLists();

        static AccountLists() { }
        private AccountLists() { }

        public static AccountLists AccountListInstance
        {
            get { return accountListInstance; }
        }

        public ObservableCollection<Account> AccountList
        {
            get { return _accountLists; }
        }


        public async Task LoadAccounts()
        {
            if (await FileHandler.FileExists(Constants.AccountListFileName)) 
            {
                string accounts = await FileHandler.ReadFile(Constants.AccountListFileName);
                if (accounts != "")
                {
                    _accountLists = JsonConvert.DeserializeObject<ObservableCollection<Account>>(accounts);
                }   
            }     
        }

        public async Task AddAccountToFile(Account A)
        {
            FileHandler.WriteFile(Constants.AccountListFileName, _accountLists);
        }
    }
}


