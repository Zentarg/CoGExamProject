using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MainWindow.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MainWindow.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GameTemplate : Page
    {
        private ShoppingCart _shoppingCart;
        private GameList _gameList;

        public GameTemplate()
        {
            this.InitializeComponent();
            _shoppingCart = ShoppingCart.Instance;
            _gameList = GameList.Instance;
        }

        private void BuyGameButton_OnClick(object sender, RoutedEventArgs e)
        {
            _shoppingCart.AddGame(_gameList.SelectedGame);
        }
    }
}
