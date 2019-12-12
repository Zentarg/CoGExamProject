using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using GalaSoft.MvvmLight.Command;
using MainWindow.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MainWindow.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public partial class GameTemplate : Page
    {
        private ShoppingCart _shoppingCart;
        private GameList _gameList;
        private NavigationHandler _navigationHandler;

        public GameTemplate()
        {
            this.InitializeComponent();
            _shoppingCart = ShoppingCart.Instance;
            _gameList = GameList.Instance;
            _navigationHandler = NavigationHandler.Instance;
            ScrollViewGrid.MaxHeight = ApplicationView.GetForCurrentView().VisibleBounds.Height *
                                       DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
            DeleteGameCommand = new RelayCommand(DeleteGame);
        }

        private void BuyGameButton_OnClick(object sender, RoutedEventArgs e)
        {
            _shoppingCart.AddGame(_gameList.SelectedGame);
        }


        private RelayCommand DeleteGameCommand { get; }

        private void RemoveGameButton_OnClick(object sender, RoutedEventArgs e)
        {
            ContentDialog confirmDeletion = new ContentDialog()
            {
                Title = "Delete Game",
                Content = "Are you sure you want to remove the game from the store?",
                PrimaryButtonText = "Cancel",
                SecondaryButtonText = "Remove the game"
            };
            confirmDeletion.SecondaryButtonCommand = DeleteGameCommand;
            confirmDeletion.ShowAsync();
        }

        private void DeleteGame()
        {
            _gameList.RemoveGame(_gameList.SelectedGame);
            _navigationHandler.NavigateFrame(Type.GetType($"{Application.Current.GetType().Namespace}.Views.Store"));
        }

    }
}
