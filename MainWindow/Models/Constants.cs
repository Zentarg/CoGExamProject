using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainWindow.Models
{
    public static class Constants
    {
        public enum CarrouselItemType { Video, Image, YoutubeVideo }

        public enum AddGameErrors { NoError, GameExists, NameInvalid, DescriptionInvalid, PriceInvalid, CategoriesInvalid }

        public const string GameFileListName = "GameList.json";

        public const string AccountListFileName = "AccountList.json";

        public const string AccountDetailsFolderPath = "\\AccountDetails\\";

        public const string ThumbnailImageFolderPath = "\\Thumbnails\\";

        public const string CarrouselItemFolderPath = "\\CarrouselItems\\";


    }
}
