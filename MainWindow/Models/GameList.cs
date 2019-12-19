using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using LinqToDB.Common;
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
        /// Adds the passed games to the list of games, and saves the newly updated list to the file.
        /// </summary>
        /// <param name="newGame">The games to be added.</param>
        public void AddGame(List<Game> newGame)
        {
            foreach (Game game in newGame)
            {
                StoreGameCollection.Add(game);
            }
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
            else
            {
                List<Game> newGameList = new List<Game>();
                Account _accountToUse = new Account("Baseline", "Baseline", "Baseline");
                newGameList.Add(new Game(_accountToUse,"..\\Assets\\1Pepe'sKitchen.jpg", "Pepe's Kitchen", 19.95f, 0,
                    "You are a new proud team member of Pepe’s Kitchen, when you are not serving the all new highly addictive fried chicken taco sandwich you will have the exciting task of surviving another shift. Join a team of underpaid and underappreciated high school dropouts in serving your local deranged community, while supplies last.",
                    "", new List<string>() {"Strategy", "action", "fighting", "cooking"}, new List<CarrouselItem>(),
                    DateTimeOffset.MinValue));
                newGameList.Add(new Game(_accountToUse, "..\\Assets\\2HARDCOREMEGADEATHPART7.jpg", "HARDCORE MEGA DEATH PT.7", 9.95f, 0,
                    "Now with even more hardcore meganess than all previous titles combined. HARDCORE MEGA DEATH PT.7 Will be the most deleted game by mothers this holiday season. Don’t miss out on all the completely unnecessary gore and violence! Now with an even less relevant story and plots that don’t make sense, buy now!",
                    "", new List<string>() { "Impossible", "arcade", "fighting", "rpg", "mature" }, new List<CarrouselItem>(),
                    DateTimeOffset.MinValue));
                newGameList.Add(new Game(_accountToUse, "..\\Assets\\3BIKEWARES.jpg", "Bike Wares", 15.95f, 0,
                    "Create a customer and then browse your local bike shops inventory. See the latest fashion in the biking industry or even better set new fashion trends! With thousands of different articles of clothing, there are millions of different clothing combinations. Guaranteed to be the most exciting bike store fashion simulator ever!",
                    "", new List<string>() { "Simulator", "style", "shopping", "biking" }, new List<CarrouselItem>(),
                    DateTimeOffset.MinValue));
                newGameList.Add(new Game(_accountToUse, "..\\Assets\\4BIKEWARS.jpg", "BIKE WARS!", 39.95f, 0,
                    "Its that time of year again, time to eat metal, breathe welding fumes, and drink oil while building your monster bike to take on last years champion! Build custom bikes, defeat gutter punks, grow your ranks, and take over the bike punk world!",
                    "", new List<string>() { "Racing", "fighting", "action", "adventure" }, new List<CarrouselItem>(),
                    DateTimeOffset.MinValue));
                newGameList.Add(new Game(_accountToUse, "..\\Assets\\5PloneySpawkSnowAlligators.jpg", "Poney Spawk Snow Alligators", 49.95f, 0,
                    "Do you have what it takes to hunt alligators bare chested in the middle of winter? Get on board or I should say off board because there’s also no boat! Join other alligator hunters in the highly anticipated Ploney Spawk adventure.",
                    "", new List<string>() { "Hunting", "survival", "adventure", "roguelike" }, new List<CarrouselItem>(),
                    DateTimeOffset.MinValue));
                newGameList.Add(new Game(_accountToUse, "..\\Assets\\6PineappleBrothers.jpg", "Pineapple Brothers", 59.95f, 0,
                    "Nades, nades, and more nades. That’s all the Pineapple brothers have but is it enough to take out the Hawaii’s new dictator and his tribes of brainwashed dude bros. Time will tell, hopefully it will tell more than this game description!",
                    "", new List<string>() { "2-player", "action-adventure", "2D", "shooter" }, new List<CarrouselItem>(),
                    DateTimeOffset.MinValue));
                newGameList.Add(new Game(_accountToUse, "..\\Assets\\7GenereicHeroGame.jpg", "Generic Hero Game", 15.95f, 0,
                    "Knight, shining armor, damsel in distress… You know the rest.",
                    "", new List<string>() { "Adventure", "RPG", "32Bit" }, new List<CarrouselItem>(),
                    DateTimeOffset.MinValue));
                newGameList.Add(new Game(_accountToUse, "..\\Assets\\8Jen'sHairSalloon.jpg", "Jen's Hair Saloon", 35.95f, 0,
                    "Get drunk, get a haircut, and make sure to do it in the proper order. What could go wrong in Jen’s Saloon? Or is it salon? No one really knows, not even Jens. If you don’t like the prices or the way someone looks, take it out back, in the dumpster diver battle arena. Confused yet? Just wait, it gets much worse!",
                    "", new List<string>() { "party game", "simulator", "fighting" }, new List<CarrouselItem>(),
                    DateTimeOffset.MinValue));
                newGameList.Add(new Game(_accountToUse, "..\\Assets\\9VegasSteak.jpg", "Vegas Steak", 45.95f, 0,
                    "You failed as a cow, now you are a steak, and an overpriced one at that. You are a sentient steak, and just got a new lease on life, you just need to survive one of the slimiest places on this planet. Avoid hungry hookers, fat tourists, homeless people, street performers, buffet goers and much more in this true test of survival.",
                    "", new List<string>() { "Indie", "MMORPG", "roguelike" }, new List<CarrouselItem>(),
                    DateTimeOffset.MinValue));
                newGameList.Add(new Game(_accountToUse, "..\\Assets\\10ReallyScaryHill.jpg", "Really Scary Hill", 25.95f, 0,
                    "WARNING!: if you are scared of heights and hills do not play this game without proper lighting and emotional support. Not your average garden variety hill game. This one is scary. Like really scary. Like you’re going to be calling your mom after this scary. Prepare for weeks of sleep deprivation after playing really scary hill..",
                    "", new List<string>() { "Exploration", "adventure", "horror" }, new List<CarrouselItem>(),
                    DateTimeOffset.MinValue));
                newGameList.Add(new Game(_accountToUse, "..\\Assets\\11LoudHillTheSequel.jpg", "Loud Hill the Sequel", 29.95f, 0,
                    "Disclaimer: Loud Hill the Sequel has nothing to do with Scary Hill. Loud Hill the Sequel is probably exactly what you think it is, or not, but hey it’s not easy thinking of 24 indie game ideas so here we are. Really surprised if anyone is actually reading any of this. Lalalala..",
                    "", new List<string>() { "Exploration", "adventure", "series" }, new List<CarrouselItem>(),
                    DateTimeOffset.MinValue));
                newGameList.Add(new Game(_accountToUse, "..\\Assets\\12WayTooSmallRentalBike.jpg", "Way Too Small Rental Bike", 29.95f, 0,
                    "Rent a bike they said, it would be fun they said… they lied! You are Frank a 2 meter high 150 kg man and you need to go sight seeing with the rest of your family... only problem is you’re a giant and the only bike available is way too small… get ready to piss off the locals and cause people to be late to work in this not so exciting racing adventure game!",
                    "", new List<string>() { "Adventure", "racing", "rpg" }, new List<CarrouselItem>(),
                    DateTimeOffset.MinValue));
                newGameList.Add(new Game(_accountToUse, "..\\Assets\\13AnimeSlothNinjaFightingGame.jpg", "Anime Sloth Ninja Fighting Game", 29.95f, 0,
                    "Ever wish you could play a game where you can see what your enemy is doing way before it has any consequences... well you are in luck my friend because these sloths are slow and angry. These arboreal mammals sure are dangerous… looooooooooook oooooouuuuuuuuut….",
                    "", new List<string>() { "Fighting", "strategy" }, new List<CarrouselItem>(),
                    DateTimeOffset.MinValue));
                newGameList.Add(new Game(_accountToUse, "..\\Assets\\14DragonCitySushi.jpg", "Dragon City Sushi", 29.95f, 0,
                    "Are you hungry? Do you like sushi? Are you broke? Well if you answered yes to all three of these than you are in luck! This free to play simulator will probably just make you hungrier, but hey its free! You get what you pay for in this all you cannot eat game!",
                    "", new List<string>() { "Simulation", "food", "adventure", "F2P" }, new List<CarrouselItem>(),
                    DateTimeOffset.MinValue));
                newGameList.Add(new Game(_accountToUse, "..\\Assets\\15MinimumWage.jpg", "Minimum Wage", 29.95f, 0,
                    "Do you miss being young and underpaid, well this game has plenty of being under paid. You can even select an older character with little to no education and simulate actual poverty in this game. Depressed yet? Well it gets worse because we may not have even made the right title for this game’s thumbnail. Buy now if you want a sad mess for the holidays!",
                    "", new List<string>() { "Simulator", "indie" }, new List<CarrouselItem>(),
                    DateTimeOffset.MinValue));
                newGameList.Add(new Game(_accountToUse, "..\\Assets\\16Non-PostApocalypticGame.jpg", "Non-Post-Apocalyptic Game", 29.95f, 0,
                    "Tired of your friends talking about there zombie plans or building bomb shelters or raising their own chickens? Well in this game everyone has enough and there’s no war and all that good stuff… so yeah… no real conflict in this game except for the fact that there is no conflict… try to cope with the boredom in Non-Post-Apocalyptic Game!",
                    "", new List<string>() { "Utopia", "survival", "adventure" }, new List<CarrouselItem>(),
                    DateTimeOffset.MinValue));
                newGameList.Add(new Game(_accountToUse, "..\\Assets\\17JargyMcphearson.jpg", "Jargy McPhearson", 29.95f, 0,
                    "He’s drunk, probably high, pissed off and ready to take out some alien slime! Imagine all the bad “macho” characters from the 80’s rolled into one big hot mess. To those out there who are easily offended, don’t buy this game. Jargy McPhearson don’t give a damn.",
                    "", new List<string>() { "Action", "adventure", "shooter" }, new List<CarrouselItem>(),
                    DateTimeOffset.MinValue));
                newGameList.Add(new Game(_accountToUse, "..\\Assets\\18AllegianceofAlliedCooperationofLeagues.jpg", "Allegiance of Allied Cooperation of Alliances", 29.95f, 0,
                    "What is Allegiance of allied cooperation of leagues? Ill tell you what Allegiance of Allied Cooperation of Leagues is not, the Allegiance of Allied Cooperation of Leagues is not the Alliance Against the Allied Cooperation of Leagues, but they are protectors of the Allergic Alabaster Abstinent Astronauts from Albania.",
                    "", new List<string>() { "Adventure", "space", "strategy" }, new List<CarrouselItem>(),
                    DateTimeOffset.MinValue));
                newGameList.Add(new Game(_accountToUse, "..\\Assets\\19PlumpPlumbPlumbers.jpg", "Plump Plumb Plumbers", 29.95f, 0,
                    "Help these plump plumb plumbers, plumb people’s pipes periodically. You must solve the puzzle before you flood poor Mrs. Johnsons apartment. Help the cities inhabitants enjoy the luxury of indoor plumbing in Plump Plumb Plumbers.",
                    "", new List<string>() { "Adventure", "32Bit", "puzzle" }, new List<CarrouselItem>(),
                    DateTimeOffset.MinValue));
                newGameList.Add(new Game(_accountToUse, "..\\Assets\\20BleepBloopLoopRoboMobo.jpg", "Bleep Bloop Loop Robo Mobo", 29.95f, 0,
                    "Help rebuild robots’ motherboards before they are melted down and turned into lawn furniture. With your help you can build an army of robots, what you do with them is entirely up to you in the fast-paced puzzle game!",
                    "", new List<string>() { "Puzzle", "strategy"}, new List<CarrouselItem>(),
                    DateTimeOffset.MinValue));
                newGameList.Add(new Game(_accountToUse, "..\\Assets\\22LucianosQuest.jpg", "Luciano’s Quest", 29.95f, 0,
                    "Imagine a knockoff of a certain little green clad boy who must save a princess… but now he’s Italian and full of stereotypes! Help our protagonist Luciano rescue the princess Zeldy from Gorgonzolladorf before it is too late!",
                    "", new List<string>() { "Adventure", "dungeon", "puzzle" }, new List<CarrouselItem>(),
                    DateTimeOffset.MinValue));
                newGameList.Add(new Game(_accountToUse, "..\\Assets\\23PortlandTrail.jpg", "Portland Trail", 29.95f, 0,
                    "Relive a classic but instead of shooting bison you’ll be shooting rats in the back alley of a hardcore show. Remember only to like bands that no ones ever heard of and trade in the trainers your parents bought you for an old pair of worn out boots that are two sizes too large, better that than being called a poser. Just remember just because manifest destiny is dead, that doesn’t mean gutter punks can’t still get dysentery!",
                    "", new List<string>() { "Strategy", "adventure", "inventory" }, new List<CarrouselItem>(),
                    DateTimeOffset.MinValue));
                newGameList.Add(new Game(_accountToUse, "..\\Assets\\24AreYouSmarterThanaSpaceTurtle.jpg", "Are you Smarter than a Space Turtle?", 29.95f, 0,
                    "No, you are not smarter than a space turtle but what better device to confirm that with than an indie game that was slapped together in 3 hours. Even if you get every answer right in this game… you still are not on a space turtles’ level.",
                    "", new List<string>() { "Quiz", "adventure" }, new List<CarrouselItem>(),
                    DateTimeOffset.MinValue));
                AddGame(newGameList);
            }
        }

        /// <summary>
        /// The list of all games loaded.
        /// </summary>
        public ObservableCollection<Game> StoreGameCollection { get; private set; } = new ObservableCollection<Game>();
    }
}
