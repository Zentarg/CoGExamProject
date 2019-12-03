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
    public static class GameList
    {
        public static void AddGame(Game newGame)
        {
            StoreGameList?.Add(newGame);
            FileHandler.WriteFile(Constants.GameFileListName, StoreGameList);
        }

        public static void RemoveGame(Game removeGame)
        {
            StoreGameList.Remove(removeGame);
        }

        public static void RemoveGame(List<Game> removeGames)
        {
            foreach (Game game in removeGames)
            {
                StoreGameList.Remove(game);
            
            }
        }

        public static async void LoadGames()
        {
            if (FileHandler.FileExists(Constants.GameFileListName))
            {
                string json = await FileHandler.ReadFile(Constants.GameFileListName);

                StoreGameList = JsonConvert.DeserializeObject<ObservableCollection<Game>>(json);
            }
        }

        public static ObservableCollection<Game> StoreGameList { get; private set; } = new ObservableCollection<Game>();
    }
}
