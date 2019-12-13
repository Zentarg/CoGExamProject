using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using MainWindow.Annotations;
using MainWindow.Models;

namespace MainWindow.ViewModels
{
    public class MainPageVM : INotifyPropertyChanged
    {
        private ReleaseDate _releaseDate;
        private ShoppingCart _shoppingCart;
        private Game _selectedGame;
        private bool _isAccountCreationLoginEnabled = true;
        private bool _isAccountLogOffEnabled;
        private string _displayName;
        private bool _isAccountSettingsEnabled = false;
        private bool _isProfileEnabled = false;
        private string _profileImagePath;


        public MainPageVM()
        {
            //AccountLists allAccounts = new AccountLists();
            //allAccounts.LoadAccounts();
            _shoppingCart = ShoppingCart.Instance;
            DoAddGame = new RelayCommand(AddGame);
            DoRemoveGame = new RelayCommand(RemoveGame);
            DoPurchaseGame = new RelayCommand(PurchaseGame);
            
            AccountHandler.MainPageVm = this;
            List<string> categories = new List<string>();
            categories.Add("Category 1");
            categories.Add("Category 2");
            categories.Add("Category 1");
            categories.Add("Category 2");
            categories.Add("Category 1");
            categories.Add("Category 2");
            categories.Add("Category 1");
            categories.Add("Category 2");
            categories.Add("Category 1");
            categories.Add("Category 2");
            categories.Add("Category 1");
            categories.Add("Category 2");
            categories.Add("Category 1");
            categories.Add("Category 2");
            categories.Add("Category 1");
            categories.Add("Category 2");
            string lorem =
                "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.";
            _selectedGame = new Game(new Account("username", "password", "displayName"), "https://helpx.adobe.com/content/dam/help/en/stock/how-to/visual-reverse-image-search/jcr_content/main-pars/image/visual-reverse-image-search-v2_intro.jpg", "gameName", 34.5f, 0, lorem, "gamepath", categories, new List<CarrouselItem>(), new ReleaseDate(12,12,2069));
        }

        
        public float TotalPrice
        {
            get { return _shoppingCart.TotalPrice;}
        }
        public Game SelectedGame
        {
            get { return _selectedGame; }
            set
            {
                _selectedGame = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Game> Games
        {
            get { return _shoppingCart.Games; }
        }

        public RelayCommand DoRemoveGame { get; set; }
        public RelayCommand DoAddGame { get; set; }
        public RelayCommand DoPurchaseGame { get; set; }
        public RelayCommand DoCheckAccountStatus { get; set; }
        public bool IsAccountCreationLoginEnabled { get { return _isAccountCreationLoginEnabled; } set { _isAccountCreationLoginEnabled = value; OnPropertyChanged(); } }
        public bool IsAccountLogOffEnabled { get { return _isAccountLogOffEnabled; } set { _isAccountLogOffEnabled = value; OnPropertyChanged(); } }
        public string DisplayName { get { return _displayName; } set { _displayName = value; OnPropertyChanged(); } }
        public bool IsAccountSettingsEnabled { get { return _isAccountSettingsEnabled; } set { _isAccountSettingsEnabled = value; OnPropertyChanged(); } }
        public bool IsProfileEnabled { get { return _isProfileEnabled; } set { _isProfileEnabled = value; OnPropertyChanged(); } }
        public string ProfileImagePath { get { return _profileImagePath; } set { _profileImagePath = value; OnPropertyChanged(); } }
       
        
        public void AddGame()
        {
            _shoppingCart.AddGame(SelectedGame);
            OnPropertyChanged(nameof(Games));
            OnPropertyChanged(nameof(TotalPrice));
            
        }

        public void RemoveGame()
        {
            _shoppingCart.RemoveGame(SelectedGame);
            OnPropertyChanged(nameof(Games));
            OnPropertyChanged(nameof(TotalPrice));
        }

        public void PurchaseGame()
        {
            _shoppingCart.PurchaseGame();
            OnPropertyChanged(nameof(Games));
            AccountHandler.AccountDetail.AccountShoppingCart = null;
        }

        private void IsAccountCreationLoginPossible()
        {
            if (AccountHandler.Account != null)
            {
                IsAccountCreationLoginEnabled = false;
            }
            else
            {
                IsAccountCreationLoginEnabled = true;
            }  
        }

        



        public void CallForAccountStatus()
        {
            if (AccountHandler.Account != null)
            {
                DisplayName = AccountHandler.SetDisplayNameForUI;
                IsAccountSettingsEnabled = true;
                IsProfileEnabled = true;
                IsAccountCreationLoginEnabled = false;
                IsAccountLogOffEnabled = true;
                ProfileImagePath = AccountHandler.SetProfileImagePathForUI;
            }
            else
            {
                DisplayName = null;
                IsAccountSettingsEnabled = false;
                IsProfileEnabled = false;
                IsAccountLogOffEnabled = false;
                IsAccountCreationLoginEnabled = true;
                ProfileImagePath = null;
            }
        }

       
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
