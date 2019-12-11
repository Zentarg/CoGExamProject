using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.System.RemoteSystems;
using Windows.UI.Xaml.Controls;
using LinqToDB.Common;
using MainWindow.Annotations;
using MainWindow.Models;

namespace MainWindow.ViewModels
{

    class AddGameContentDialogVM : INotifyPropertyChanged
    {
        private ReleaseDate _releaseDate;
        private string _thumbnailImagePath;
        private ObservableCollection<StorageFile> _carrouselImages;
        private ObservableCollection<StorageFile> _carrouselVideos;
        private ObservableCollection<ListviewString> _carrouselYoutubeVids;
        private ListviewString _selectedListviewStrings;

        private GameList _gameList;

        public AddGameContentDialogVM()
        {
            ThumbnailImagePath = "../Assets/10ReallyScaryHill.jpg";
            CarrouselImages = new ObservableCollection<StorageFile>();
            CarrouselVideos = new ObservableCollection<StorageFile>();
            CarrouselYoutubeVids = new ObservableCollection<ListviewString>();
            _gameList = GameList.Instance;
        }

        public Constants.AddGameErrors AddGame()
        {




            if (Name.IsNullOrEmpty())
                return Constants.AddGameErrors.NameInvalid;
            else if (Categories.IsNullOrEmpty())
                return Constants.AddGameErrors.CategoriesInvalid;
            else if (Price.IsNullOrEmpty())
                return Constants.AddGameErrors.PriceInvalid;
            else if (Description.IsNullOrEmpty())
                return Constants.AddGameErrors.DescriptionInvalid;

            float _price = float.Parse(Price, System.Globalization.CultureInfo.InvariantCulture);
            List<String> _categories = new List<string>();
            _categories = Categories.Split(",").ToList();
            List<CarrouselItem> _carrouselItems = new List<CarrouselItem>();

            foreach (StorageFile file in CarrouselImages)
            {
                _carrouselItems.Add(new CarrouselItem(Constants.CarrouselItemType.Image, file.Path));
            }
            foreach (StorageFile file in CarrouselVideos)
            {
                _carrouselItems.Add(new CarrouselItem(Constants.CarrouselItemType.Video, file.Path));
            }
            foreach (ListviewString item in CarrouselYoutubeVids)
            {
                _carrouselItems.Add(new CarrouselItem(Constants.CarrouselItemType.YoutubeVideo, item.Value));
            }

            if (_price.ToString().Length == 0 || _price < 1 || _price > 1000)
                return Constants.AddGameErrors.PriceInvalid;

            Game newGame = new Game(AccountHandler.Account, ThumbnailImagePath, Name, _price, 0, Description, "", _categories, _carrouselItems, _releaseDate);

            foreach (Game game in _gameList.StoreGameCollection)
            {
                if (game.Identifier == newGame.Identifier)
                {
                    return Constants.AddGameErrors.GameExists;
                }
            }
            _gameList.AddGame(newGame);

            return Constants.AddGameErrors.NoError;

        }


        public string ThumbnailImagePath
        {
            get { return _thumbnailImagePath; }
            set
            {
                _thumbnailImagePath = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<StorageFile> CarrouselImages
        {
            get { return _carrouselImages; }
            set
            {
                _carrouselImages = value;
                OnPropertyChanged();
            }
        }

        public StorageFile SelectedCarrouselImage { get; set; }

        public ObservableCollection<StorageFile> CarrouselVideos
        {
            get { return _carrouselVideos; }
            set
            {
                _carrouselVideos = value;
                OnPropertyChanged();
            }
        }

        public StorageFile SelectedCarrouselVideo { get; set; }

        public ObservableCollection<ListviewString> CarrouselYoutubeVids
        {
            get { return _carrouselYoutubeVids; }
            set
            {
                _carrouselYoutubeVids = value;
                OnPropertyChanged();
            }
        }

        public ListviewString SelectedCarrouselYoutubeVid
        {
            get { return _selectedListviewStrings; }
            set { _selectedListviewStrings = value; OnPropertyChanged(); }
        }

        public ReleaseDate ReleaseTime
        {
            get { return _releaseDate; }
            set { _releaseDate = value; OnPropertyChanged(); }
        }

        public string Name { get; set; }
        public string Categories { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
