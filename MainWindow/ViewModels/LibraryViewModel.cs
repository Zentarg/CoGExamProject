using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Contacts;
using Windows.ApplicationModel.Store.Preview.InstallControl;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Command;
using MainWindow.Annotations;
using MainWindow.Models;

namespace MainWindow.ViewModels
{
    public class LibraryViewModel : INotifyPropertyChanged
    {
        //Instance Fields
        private Game _game;
        private static ObservableCollection<Game> _games = new ObservableCollection<Game>();
        private NavigationHandler _navigationHandler;
        

        //Constructor
        public LibraryViewModel()
        {
            
            _navigationHandler = NavigationHandler.Instance;

            foreach (Game game in Games)
            {
                FilteredGames.Add(game);
            }
            
        }

       
        //Properties
        public Game SelectedGame
        {
            get
            {
                return _game;
            }

            set
            {
                _game = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ShowDetailsView));
            }
        }

        public bool ShowDetailsView
        {
            get { return SelectedGame != null; }
        }

        //Button that leads to a games page in the store
        private void GoToGameButton_OnClick(object sender, RoutedEventArgs e)
        {
            _navigationHandler.NavigateFrame(Type.GetType($"{Application.Current.GetType().Namespace}.Views.GameTemplate"));
        }

        
        //Stores the owned games into an accounts details
        public ObservableCollection<Game> Games
        {
            get { return AccountHandler.AccountDetail.GamesOwned; }
        }

        //Stores owned games into a searchable collection
        public ObservableCollection<Game> FilteredGames { get; set; } = new ObservableCollection<Game>();

        //Search for games owned in an accounts library 
        public void FilterGames(string searchTerms)
        {
            List<Game> _tempList = new List<Game>();
            foreach (Game game in Games)
            {
                if (game.Name.ToLower().Contains(searchTerms.ToLower()))
                    _tempList.Add(game);
            }

            for (int i = FilteredGames.Count; i > 0; i--)
            {
                if (!(_tempList.Contains(FilteredGames[i - 1])))
                    FilteredGames.Remove(FilteredGames[i - 1]);
            }

            foreach (Game game in _tempList)
            {
                if (!FilteredGames.Contains(game))
                    FilteredGames.Add(game);
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
