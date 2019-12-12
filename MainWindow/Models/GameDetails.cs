using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainWindow.Models
{
    public class GameDetails
    {
        public GameDetails(string title, string gameImageSource, string categories, string gameDescription)
        {
            Title = title;
            GameImageSource = gameImageSource;
            Categories = categories;
            GameDescription = gameDescription;
        }
        public string Title { get; set; }
        public string GameImageSource { get; set; }
        public string Categories { get; set; }
        public string GameDescription { get; set; }
    }
}
