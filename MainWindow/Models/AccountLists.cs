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
        private const string FileName = "AccountList.json";
        readonly StorageFolder _storageFolder = ApplicationData.Current.LocalFolder;
        private static AccountLists accountListInstance = new AccountLists();

        static AccountLists() { }
        private AccountLists() { }

        public static AccountLists AccountListInstance
        {
            get { return accountListInstance; }
        }


        public async Task LoadAccounts()
        {
            string accounts = await FileIO.ReadTextAsync(await OpenOrCreateFile());
            if (accounts != "")
            {
                _accountLists = JsonConvert.DeserializeObject<ObservableCollection<Account>>(accounts);
            }    
        }

        public async Task CreateAccount(Account A)
        {
            _accountLists.Add(A);
            string _json = JsonConvert.SerializeObject(_accountLists);
            await FileIO.WriteTextAsync(await OpenOrCreateFile(), _json);

            //Line below returns path of the file for checking
            var path = _storageFolder.Path;
        }

        public async Task<StorageFile> OpenOrCreateFile()
        {
             
            return await _storageFolder.CreateFileAsync(FileName, CreationCollisionOption.OpenIfExists);
        }
    
    }
}


