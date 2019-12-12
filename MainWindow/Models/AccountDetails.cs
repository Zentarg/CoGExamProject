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
        private ObservableCollection<AccountPurchase> _purchaseHistory = new ObservableCollection<AccountPurchase>();
        private ObservableCollection<string> _gamesOwned = new ObservableCollection<string>();

        public AccountDetails()
        {

        }
      

        #region Properties
        public string UserName { get { return _username; } set { _username = value; } }
        //public string ProfilePicturePatch { get; set; }
        public string DisplayName { get { return _displayname; } set { _displayname = value; } } 
        public string JoinDate { get { return _joinDate; } set { _joinDate = value; } }
        public int GamesOwnedCount { get { return _gamesOwnedCount; } set { _gamesOwnedCount = value; } }
        public ObservableCollection<AccountPurchase> PurchaseHistory { get { return _purchaseHistory; } set { _purchaseHistory = value; } }
        
        //Below needs implementing
        public ShoppingCart AccountShoppingCart { get; set; }
        
        
        //Below is the link to the library (half of it, from the purchase of a game side, stores the game identifier can be changed to name easily if that is easier)
        public ObservableCollection<string> GamesOwned { get { return _gamesOwned; } set { _gamesOwned = value; } }


        #endregion

        #region Methods
        public void AddPurchaseToPurchaseHistory(string gameName, float gamePrice, DateTime purchaseDate, string identifier)
        {
            PurchaseHistory.Insert(0, new AccountPurchase(gameName, gamePrice, purchaseDate, identifier));
            //Code below that is commented was used to sort the list of purchased games initially. However, I realized I could do insert instead of add and then orderbydescending, and achieve the same result.
            //PurchaseHistory = new ObservableCollection<AccountPurchase>(PurchaseHistory.OrderByDescending(i => i.PurchaseDate));
            GamesOwnedCount += 1;
            
            
        }


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
