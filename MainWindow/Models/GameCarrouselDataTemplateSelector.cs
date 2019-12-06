using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MainWindow.Models
{
    public class GameCarrouselDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate VideoTemplate { get; set; }
        public DataTemplate YoutubeVideoTemplate { get; set; }
        public DataTemplate ImageTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            if (((CarrouselItem) item).CarrouselItemType == Constants.CarrouselItemType.Video)
                return VideoTemplate;
            if (((CarrouselItem) item).CarrouselItemType == Constants.CarrouselItemType.YoutubeVideo)
                return YoutubeVideoTemplate;
            if (((CarrouselItem) item).CarrouselItemType == Constants.CarrouselItemType.Image)
                return ImageTemplate;
            return base.SelectTemplateCore(item);
        }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            return SelectTemplateCore(item);
        }
    }
}
