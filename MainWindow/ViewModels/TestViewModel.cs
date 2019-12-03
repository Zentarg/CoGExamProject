using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using MainWindow.Models;

namespace MainWindow.ViewModels
{
   public class TestViewModel
    {

        public ObservableCollection<Game> StoreGameCollection { get; }
        public TestGame SelectedGame { get; set; }
        public string Name { get; set; }
        public string TimePlayed { get; set; }
        public  string ImageLink { get; set; }
    }

   public class TestGame
   {
       public TestGame(string name, string timePlayed, string imageLink)
       {
           Name = name;
           TimePlayed = timePlayed;
           ImageLink = imageLink;   
       }
       public string Name { get; set; }
       public string TimePlayed { get; set; }
       public string ImageLink { get; set; }
    }
}
