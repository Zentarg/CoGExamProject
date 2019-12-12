using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Contacts;
using Windows.ApplicationModel.Store.Preview.InstallControl;
using Windows.UI.Xaml.Controls;
using GalaSoft.MvvmLight.Command;
using MainWindow.Annotations;
using MainWindow.Models;

namespace MainWindow.ViewModels
{
    public class LibraryViewModel : INotifyPropertyChanged
    {
        
        private GameCatalog _gameCatalog;
        private GameDetails _selectedGame;
        private string _searchQuery;

        //private DeleteCommand _deletionCommand;

        public LibraryViewModel()
        {
            _gameCatalog = new GameCatalog();
            
            _selectedGame = null;
            //_deletionCommand = new DeleteCommand(_contactCatalog, this);
        }

        


        public string GameSearch
        {
            get
            {
                var res = GamesCollection.FirstOrDefault(g => g.Title == _searchQuery);
                if (res != null)
                {
                  return res.Title;  
                }

                return "Search";
            }
            set
            {
                _searchQuery = value;
                OnPropertyChanged();
            }
        }


        public GameDetails SelectedGame
        {
            get
            {
                return _selectedGame;
            }

            set
            {
                _selectedGame = value;
                OnPropertyChanged();
                //_deletionCommand.RaiseCanExecuteChanged();
            }
        }
        /*
        public sealed class AutoSuggestBox : ItemsControl { 

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            // Only get results when it was a user typing, 
            // otherwise assume the value got filled in by TextMemberPath 
            // or the handler for SuggestionChosen.
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                //Set the ItemsSource to be your filtered dataset
                sender.ItemsSource = GamesCollection;
            }
        }


        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            // Set sender.Text. You can use args.SelectedItem to build your text string.
            sender.ItemsSource = args.SelectedItem;

        }


        private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                
                // User selected an item from the suggestion list, take an action on it here.
            }
            else
            {
                
                Console.WriteLine("Game can't be found.");
                // Use args.QueryText to determine what to do.
            }
        }
            }
            
        */





        /*
        public DeleteCommand DeletionCommand
        {
            get { return _deletionCommand; }
        }
        */
        public ObservableCollection<GameDetails> GamesCollection
        {
            get { return _gameCatalog.Games; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
