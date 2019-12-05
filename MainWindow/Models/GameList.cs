using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public Game SelectedGame { get; set; }

        public void AddGame(Game newGame)
        {
            StoreGameCollection?.Add(newGame);
            FileHandler.WriteFile(Constants.GameFileListName, StoreGameCollection);
        }

        public void RemoveGame(Game removeGame)
        {
            StoreGameCollection.Remove(removeGame);
        }

        public void RemoveGame(List<Game> removeGames)
        {
            foreach (Game game in removeGames)
            {
                StoreGameCollection.Remove(game);
            
            }
        }

        public async void LoadGames()
        {
            if (FileHandler.FileExists(Constants.GameFileListName))
            {
                string json = await FileHandler.ReadFile(Constants.GameFileListName);

                StoreGameCollection = JsonConvert.DeserializeObject<ObservableCollection<Game>>(json);
            }
        }

        public ObservableCollection<Game> StoreGameCollection { get; private set; } = new ObservableCollection<Game>();
    }
}
