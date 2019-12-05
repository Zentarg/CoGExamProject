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
            if (FileHandler.FileExists(Constants.FileName)) 
            {
                string accounts = await FileHandler.ReadFile(Constants.FileName);
                if (accounts != "")
                {
                    _accountLists = JsonConvert.DeserializeObject<ObservableCollection<Account>>(accounts);
                }   
            }     
        }

        public async Task CreateAccount(Account A)
        {
            _accountLists.Add(A);
            FileHandler.WriteFile(Constants.FileName, _accountLists);

            //Line below returns path of the file for checking
        }
    }
}


