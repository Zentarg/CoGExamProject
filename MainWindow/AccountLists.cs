using MainWindow.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainWindow
{
    class AccountLists
    {
        private List<Account> _accountLists;

        public AccountLists()
        {
            _accountLists = new List<Account>();
        }

        public void CreateAccount()
        {
            _accountLists.Add(new Account("username", "password"));
            string json = JsonConvert.SerializeObject(_accountLists.ToArray());

            //write string to file
            if (System.IO.File.Exists("@DataStorage\\AccountList.json"))
            {
                System.IO.File.WriteAllText("DataStorage\\AccountList.json", json);
            }
            else
            {
                System.IO.File.Create("DataStorage\\AccountList.json");
                System.IO.File.WriteAllText("DataStorage\\AccountList.json", json);
            }
        }
        
    }
}
