using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        /// <summary>
        /// Adds the passed game to the list of games, and saves the newly updated list to the file.
        /// </summary>
        /// <param name="newGame">The game to be added.</param>
        public void AddGame(Game newGame)
        {
            StoreGameCollection?.Add(newGame);
            FileHandler.WriteFile(Constants.GameFileListName, StoreGameCollection);
        }

        /// <summary>
        /// Removes the passed game from the list of games, and saves the newly updated list to the file.
        /// If the game passed is the same game as the SelectedGame, SelectedGame will be set to null.
        /// </summary>
        /// <param name="removeGame">The game to be removed.</param>
        public void RemoveGame(Game removeGame)
        {
            if (SelectedGame == removeGame)
                SelectedGame = null;
            StoreGameCollection.Remove(removeGame);
            FileHandler.WriteFile(Constants.GameFileListName, StoreGameCollection);
        }

        /// <summary>
        /// Removes the passed games from the list of games, and saves the newly updated list to the file.
        /// If one of the games passed is the same game as the SelectedGame, SelectedGame will be set to null.
        /// </summary>
        /// <param name="removeGames">The list of games to be removed.</param>
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

        /// <summary>
        /// Replaces the old game with the newly edited game, and saves the newly updated list to the file.
        /// </summary>
        /// <param name="oldGame">The game to be replaced.</param>
        /// <param name="newGame">The new game, that replaces the old one.</param>
        public void EditGame(Game oldGame, Game newGame)
        {
            StoreGameCollection[StoreGameCollection.IndexOf(oldGame)] = newGame;
            FileHandler.WriteFile(Constants.GameFileListName, StoreGameCollection);
        }

        /// <summary>
        /// Loads all games from the file, and populates StoreGameCollection with said games.
        /// </summary>
        public async Task LoadGames()
        {
            if (await FileHandler.FileExists(Constants.GameFileListName))
            {
                string json = await FileHandler.ReadFile(Constants.GameFileListName);
                if (json != "")
                {
                    StoreGameCollection = JsonConvert.DeserializeObject<ObservableCollection<Game>>(json);
                }
            }
        }

        /// <summary>
        /// The list of all games loaded.
        /// </summary>
        public ObservableCollection<Game> StoreGameCollection { get; private set; } = new ObservableCollection<Game>();
    }
}
