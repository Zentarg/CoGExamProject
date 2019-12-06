using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Contacts;
using MainWindow.Annotations;
using MainWindow.Models;

namespace MainWindow.ViewModels
{
    public class LibraryViewModel : INotifyPropertyChanged
    {
        private GameDetails _domainObject;
        private GameCatalog _gameCatalog;
        private GameDetails _selectedGame;
        //private DeleteCommand _deletionCommand;

        public LibraryViewModel()
        {
            _gameCatalog = new GameCatalog();
            DomainObject();
            _selectedGame = null;
            //_deletionCommand = new DeleteCommand(_contactCatalog, this);
        }

        public void DomainObject()
        {
            foreach (var c in _gameCatalog.Games)
            {
                _domainObject = c;
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
