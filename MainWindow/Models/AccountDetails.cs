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
        #region Instance Fields
        private string _displayname;
        private string _username;
        private string _joinDate;
        private int _gamesOwnedCount;
        private ObservableCollection<AccountPurchase> _purchaseHistory = new ObservableCollection<AccountPurchase>();
        private ObservableCollection<Game> _gamesOwned = new ObservableCollection<Game>();
        #endregion

        #region Constructor
        public AccountDetails()
        {

        }
        #endregion

        #region Properties
        public string UserName { get { return _username; } set { _username = value; } }
        public string DisplayName { get { return _displayname; } set { _displayname = value; } }
        public string ProfilePicturePath { get; set; }
        public string JoinDate { get { return _joinDate; } set { _joinDate = value; } }
        public int GamesOwnedCount { get { return _gamesOwnedCount; } set { _gamesOwnedCount = value; } }
        
        //Stores the current users shopping cart.
        public ShoppingCart AccountShoppingCart { get; set; }
             
        //Stores all purchased games for the library.
        public ObservableCollection<Game> GamesOwned { get { return _gamesOwned; } set { _gamesOwned = value; } }

        //Stores purchase history for games bought, minimal information.
        public ObservableCollection<AccountPurchase> PurchaseHistory { get { return _purchaseHistory; } set { _purchaseHistory = value; } }
        #endregion

        #region Methods
        public void AddPurchaseToPurchaseHistory(string gameName, float gamePrice, DateTime purchaseDate, string identifier)
        {
            //Rather than sorting the purchases of new games, so that the most recent is at the top. It is easier to just insert a new purchase into the already made list.
            PurchaseHistory.Insert(0, new AccountPurchase(gameName, gamePrice, purchaseDate, identifier));
            //Increases the games owned count, every time a game is added to the purchase history.
            GamesOwnedCount += 1;           
        }


        public void CreateUserDetailsFile(AccountDetails accountDetails, string username)
        {
            //Creates or writes the details stored in the properties of an object of this class to a file with the username provided, where username is a passed argument.
            FileHandler.WriteFile(Constants.AccountDetailsFolderPath + username + ".json", accountDetails);
        }

        public async Task<AccountDetails> LoadUserDetailsFile(string username)
        {
            //This method loads a user's details file if it exists, else returns null. However, it should never happen that this returns null.
            if (await FileHandler.FileExists(Constants.AccountDetailsFolderPath + username + ".json"))
            {
                //Reads the data of an object of this class' properties and then returns that data.
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
