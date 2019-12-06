using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainWindow.Models
{
    public class GameCatalog
    {
        private GameDetails _selectedGame;

        private ObservableCollection<GameDetails> _games = new ObservableCollection<GameDetails>()
        {
            new GameDetails("Pepe's Kitchen", "..\\Assets\\1Pepe'sKitchen.jpg" ,"Strategy, action, fighting, cooking","You are a new proud team member of Pepe’s Kitchen, when you are not serving the all new highly addictive fried chicken taco sandwich you will have the exciting task of surviving another shift. Join a team of underpaid and underappreciated high school dropouts in serving your local deranged community, while supplies last."),
            new GameDetails("HARDCORE MEGA DEATH PT.7", "..\\Assets\\2HARDCOREMEGADEATHPART7.jpg", "Impossible, arcade, fighting, rpg, mature", "Now with even more hardcore meganess than all previous titles combined. HARDCORE MEGA DEATH PT.7 Will be the most deleted game by mothers this holiday season. Don’t miss out on all the completely unnecessary gore and violence! Now with an even less relevant story and plots that don’t make sense, buy now!" ),
            new GameDetails("Bike Wares","..\\Assets\\3BIKEWARES.jpg","Simulator, style, shopping, biking","Create a customer and then browse your local bike shops inventory. See the latest fashion in the biking industry or even better set new fashion trends! With thousands of different articles of clothing, there are millions of different clothing combinations. Guaranteed to be the most exciting bike store fashion simulator ever!"),
            new GameDetails("BIKE WARS!","..\\Assets\\4BIKEWARS.jpg","Racing, fighting, action, adventure","Its that time of year again, time to eat metal, breathe welding fumes, and drink oil while building your monster bike to take on last years champion! Build custom bikes, defeat gutter punks, grow your ranks, and take over the bike punk world!"),
            new GameDetails("Poney Spawk Snow Alligators","..\\Assets\\5PloneySpawkSnowAlligators.jpg","Hunting, survival, adventure, roguelike","Do you have what it takes to hunt alligators bare chested in the middle of winter? Get on board or I should say off board because there’s also no boat! Join other alligator hunters in the highly anticipated Ploney Spawk adventure."),
            new GameDetails("Pineapple Brothers","..\\Assets\\6PineappleBrothers.jpg","2-player, action-adventure, 2D, shooter","Nades, nades, and more nades. That’s all the Pineapple brothers have but is it enough to take out the Hawaii’s new dictator and his tribes of brainwashed dude bros. Time will tell, hopefully it will tell more than this game description!"),
            new GameDetails("Generic Hero Game","..\\Assets\\7GenereicHeroGame.jpg","Adventure, RPG, 32Bit","Knight, shining armor, damsel in distress… You know the rest."),
            new GameDetails("Jen's Hair Saloon","..\\Assets\\8Jen'sHairSalloon.jpg","party game, simulator, fighting","Get drunk, get a haircut, and make sure to do it in the proper order. What could go wrong in Jen’s Saloon? Or is it salon? No one really knows, not even Jens. If you don’t like the prices or the way someone looks, take it out back, in the dumpster diver battle arena. Confused yet? Just wait, it gets much worse!"),
            new GameDetails("Vegas Steak","..\\Assets\\9VegasSteak.jpg","Indie, MMORPG, roguelike","You failed as a cow, now you are a steak, and an overpriced one at that. You are a sentient steak, and just got a new lease on life, you just need to survive one of the slimiest places on this planet. Avoid hungry hookers, fat tourists, homeless people, street performers, buffet goers and much more in this true test of survival."),
            new GameDetails("Really Scary Hill","..\\Assets\\10ReallyScaryHill.jpg","Exploration, adventure, horror","WARNING!: if you are scared of heights and hills do not play this game without proper lighting and emotional support. Not your average garden variety hill game. This one is scary. Like really scary. Like you’re going to be calling your mom after this scary. Prepare for weeks of sleep deprivation after playing really scary hill.."),
            new GameDetails("Loud Hill the Sequel","..\\Assets\\11LoudHillTheSequel.jpg","Exploration, adventure, series","Disclaimer: Loud Hill the Sequel has nothing to do with Scary Hill. Loud Hill the Sequel is probably exactly what you think it is, or not, but hey it’s not easy thinking of 24 indie game ideas so here we are. Really surprised if anyone is actually reading any of this. Lalalala.."),
            new GameDetails("Way Too Small Rental Bike","..\\Assets\\12WayTooSmallRentalBike.jpg","Adventure, racing, rpg","Rent a bike they said, it would be fun they said… they lied! You are Frank a 2 meter high 150 kg man and you need to go sight seeing with the rest of your family... only problem is you’re a giant and the only bike available is way too small… get ready to piss off the locals and cause people to be late to work in this not so exciting racing adventure game!"),
            new GameDetails("Anime Sloth Ninja Fighting Game","..\\Assets\\13AnimeSlothNinjaFightingGame.jpg","Fighting, strategy","Ever wish you could play a game where you can see what your enemy is doing way before it has any consequences... well you are in luck my friend because these sloths are slow and angry. These arboreal mammals sure are dangerous… looooooooooook oooooouuuuuuuuut…."),
            new GameDetails("Dragon City Sushi","..\\Assets\\14DragonCitySushi.jpg","Simulation, food, adventure, F2P","Are you hungry? Do you like sushi? Are you broke? Well if you answered yes to all three of these than you are in luck! This free to play simulator will probably just make you hungrier, but hey its free! You get what you pay for in this all you cannot eat game!"),
            new GameDetails("Minimum Wage","..\\Assets\\15MinimumWage.jpg","Simulator, indie","Do you miss being young and underpaid, well this game has plenty of being under paid. You can even select an older character with little to no education and simulate actual poverty in this game. Depressed yet? Well it gets worse because we may not have even made the right title for this game’s thumbnail. Buy now if you want a sad mess for the holidays!"),
            new GameDetails("Non-Post-Apocalyptic Game","..\\Assets\\16Non-PostApocalypticGame.jpg","Utopia, survival, adventure","Tired of your friends talking about there zombie plans or building bomb shelters or raising their own chickens? Well in this game everyone has enough and there’s no war and all that good stuff… so yeah… no real conflict in this game except for the fact that there is no conflict… try to cope with the boredom in Non-Post-Apocalyptic Game!"),
            new GameDetails("Jargy McPhearson","..\\Assets\\17JargyMcphearson.jpg","Action, adventure, shooter","He’s drunk, probably high, pissed off and ready to take out some alien slime! Imagine all the bad “macho” characters from the 80’s rolled into one big hot mess. To those out there who are easily offended, don’t buy this game. Jargy McPhearson don’t give a damn."),
            new GameDetails("Allegiance of Allied Cooperation of Leagues","..\\Assets\\18AllegianceofAlliedCooperationofLeagues.jpg","Adventure, space, strategy","What is Allegiance of allied cooperation of leagues? Ill tell you what Allegiance of Allied Cooperation of Leagues is not, the Allegiance of Allied Cooperation of Leagues is not the Alliance Against the Allied Cooperation of Leagues, but they are protectors of the Allergic Alabaster Abstinent Astronauts from Albania."),
            new GameDetails("Plump Plumb Plumbers","..\\Assets\\19PlumpPlumbPlumbers.jpg","Adventure, 32Bit, puzzle","Help these plump plumb plumbers, plumb people’s pipes periodically. You must solve the puzzle before you flood poor Mrs. Johnsons apartment. Help the cities inhabitants enjoy the luxury of indoor plumbing in Plump Plumb Plumbers."),
            new GameDetails("Bleep Bloop Loop Robo Mobo","..\\Assets\\20BleepBloopLoopRoboMobo.jpg","Puzzle, strategy","Help rebuild robots’ motherboards before they are melted down and turned into lawn furniture. With your help you can build an army of robots, what you do with them is entirely up to you in the fast-paced puzzle game!"),
            new GameDetails("Useless Crypto Mining Game","..\\Assets\\21UselessCryptoMiningGame.jpg","Arcade, puzzle","Like any good old mining game there is a claw involved… or well a “claw gun” that you can shoot at random attendees of a local crypto convention. If you manage to hit someone you will instantly start uploading Chinese crypto mining malware on their phone and maybe even their laptop if they have a backpack! Soon you will be sending your newly hashed monero coins to your private wallet. Enjoy!"),
            new GameDetails("Luciano’s Ques","..\\Assets\\22LucianosQuest.jpg","Adventure, dungeon, puzzle","Imagine a knockoff of a certain little green clad boy who must save a princess… but now he’s Italian and full of stereotypes! Help our protagonist Luciano rescue the princess Zeldy from Gorgonzolladorf before it is too late!"),
            new GameDetails("Portland Trail","..\\Assets\\23PortlandTrail.jpg","Strategy, adventure, inventory","Relive a classic but instead of shooting bison you’ll be shooting rats in the back alley of a hardcore show. Remember only to like bands that no ones ever heard of and trade in the trainers your parents bought you for an old pair of worn out boots that are two sizes too large, better that than being called a poser. Just remember just because manifest destiny is dead, that doesn’t mean gutter punks can’t still get dysentery!"),
            new GameDetails("Are you Smarter than a Space Turtle?","..\\Assets\\24AreYouSmarterThanaSpaceTurtle.jpg","Quiz, adventure","No, you are not smarter than a space turtle but what better device to confirm that with than an indie game that was slapped together in 3 hours. Even if you get every answer right in this game… you still are not on a space turtles’ level.")


        };
        public ObservableCollection<GameDetails> Games
        {
            get { return _games; }
        }

        

   

    }
}

