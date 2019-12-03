using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using MainWindow.Models;

namespace MainWindow.ViewModels
{
    class StoreVM
    {
        private Game newgame;
                

        public StoreVM()
        {
            newgame = new Game(new Account("username", "password", "displayname"), "gamename", 34.5f, 0, "description", "gamepath", new List<CarrouselItem>());

            DoAddGame = new RelayCommand(AddGame);
            DoLoadGames = new RelayCommand(LoadGames);
        }

        public RelayCommand DoAddGame { get; set; }
        public RelayCommand DoLoadGames { get; set; }

        private void AddGame()
        {
            GameList.AddGame(newgame);
        }

        private void LoadGames()
        {
            GameList.LoadGames();
        }

        public ObservableCollection<Game> StoreGameCollection { get; set; }



    }
}
