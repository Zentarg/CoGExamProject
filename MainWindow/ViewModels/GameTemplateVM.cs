using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.System;
using MainWindow.Annotations;
using MainWindow.Models;

namespace MainWindow.ViewModels
{
    public class GameTemplateVM : INotifyPropertyChanged
    {
        private GameList _gameList;

        public GameTemplateVM()
        {
            _gameList = GameList.Instance;
            AccountHandler.GameTemplateVm = this;
            _gameList.GameTemplateVm = this;
        }

        public Game SelectedGame
        {
            get { return _gameList.SelectedGame; }
        }

        public bool UserIsOwner
        {
            get { return AccountHandler.Account?.UserName == SelectedGame.Author?.UserName; }
        }

        public bool UserIsntOwner
        {
            get { return AccountHandler.Account?.UserName != SelectedGame.Author?.UserName; }
        }

        public string ReleaseDateString
        {
            get { return SelectedGame.ReleaseDate.Date.ToShortDateString(); }
        }

        /// <summary>
        /// Updates UserIsOwner, and UserIsntOwner.
        /// </summary>
        public void OnAccountChanged()
        {
            OnPropertyChanged(nameof(UserIsOwner));
            OnPropertyChanged(nameof(UserIsntOwner));
        }

        /// <summary>
        /// Updates SelectedGame, and ReleaseDateString
        /// </summary>
        public void OnGameEdited()
        {
            OnPropertyChanged(nameof(SelectedGame));
            OnPropertyChanged(nameof(ReleaseDateString));
        }

        /// <summary>
        /// Method for telling the view a property has been updated
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
