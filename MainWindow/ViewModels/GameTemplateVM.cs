using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainWindow.Models;

namespace MainWindow.ViewModels
{
    class GameTemplateVM
    {
        private GameList _gameList;

        public GameTemplateVM()
        {
            _gameList = GameList.Instance;
        }

        public Game SelectedGame
        {
            get { return _gameList.SelectedGame; }
        }

    }
}
