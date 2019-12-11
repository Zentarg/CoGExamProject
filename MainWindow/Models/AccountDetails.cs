using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainWindow.Models
{
    public class AccountDetails
    {

        //private Library _library;

        // private string _profilePicturePath;
        private string _displayname;
        private string _username;
        private string _joinDate;
        private int _gamesOwnedCount;

        public AccountDetails()
        {

        }
      

        #region Properties
        public string UserName { get { return _username; } set { _username = value; } }
        //public string ProfilePicturePatch { get; set; }
        public string DisplayName { get { return _displayname; } set { _displayname = value; } } 
        public string JoinDate { get { return _joinDate; } set { _joinDate = value; } }
        public int GamesOwnedCount { get { return _gamesOwnedCount; } set { _gamesOwnedCount = value; } }

        //Below need implementing
        public ShoppingCart AccountShoppingCart { get; set; }
        public ObservableCollection<Game> Library { get; set; }

        #endregion

        #region Methods
        public void CreateUserDetailsFile(AccountDetails accountDetails, string username)
        {
            FileHandler.WriteFile(Constants.AccountDetailsFolderPath + username + ".json", accountDetails);
        }

        public async Task<AccountDetails> LoadUserDetailsFile(string username)
        {
            if (FileHandler.FileExists(Constants.AccountDetailsFolderPath + username + ".json"))
            {
                string json = await FileHandler.ReadFile(Constants.AccountDetailsFolderPath + username + ".json");

                return JsonConvert.DeserializeObject<AccountDetails>(json);
            }
            else
            {
                return null;
            }       
        }
        #endregion
    }
}
