using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainWindow.ViewModels;
using Newtonsoft.Json;

namespace MainWindow.Models
{
    public class GameList
    {

        private static GameList instance;

        private GameList()
        {

        }

        public static GameList Instance {
            get {
                if (instance == null)
                    instance = new GameList();
                return instance;
            }
        }

        public GameTemplateVM GameTemplateVm { get; set; }

        public Game SelectedGame { get; set; }

        public void AddGame(Game newGame)
        {
            StoreGameCollection?.Add(newGame);
            FileHandler.WriteFile(Constants.GameFileListName, StoreGameCollection);
        }

        public void RemoveGame(Game removeGame)
        {
            if (SelectedGame == removeGame)
                SelectedGame = null;
            StoreGameCollection.Remove(removeGame);
            FileHandler.WriteFile(Constants.GameFileListName, StoreGameCollection);
        }

        public void RemoveGame(List<Game> removeGames)
        {
            foreach (Game game in removeGames)
            {
                if (SelectedGame == game)
                    SelectedGame = null;
                StoreGameCollection.Remove(game);
            }
            FileHandler.WriteFile(Constants.GameFileListName, StoreGameCollection);
        }

        public void EditGame(Game oldGame, Game newGame)
        {
            StoreGameCollection[StoreGameCollection.IndexOf(oldGame)] = newGame;
            FileHandler.WriteFile(Constants.GameFileListName, StoreGameCollection);
        }

        public async Task LoadGames()
        {
            if (FileHandler.FileExists(Constants.GameFileListName))
            {
                string json = await FileHandler.ReadFile(Constants.GameFileListName);
                if (json != "")
                {
                    StoreGameCollection = JsonConvert.DeserializeObject<ObservableCollection<Game>>(json);
                }               
            }
        }

        public ObservableCollection<Game> StoreGameCollection { get; private set; } = new ObservableCollection<Game>();
    }
}
