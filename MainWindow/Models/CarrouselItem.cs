using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainWindow.Models
{
    class CarrouselItem
    {

        public CarrouselItem(Constants.CarrouselItemType carrouselItemType, string itemLink)
        {
            CarrouselItemType = carrouselItemType;
            ItemLink = itemLink;
        }

        public Constants.CarrouselItemType CarrouselItemType { get; private set; }
        public string ItemLink { get; private set; }

    }
}
