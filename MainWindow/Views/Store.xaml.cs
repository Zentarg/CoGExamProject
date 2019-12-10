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
using MainWindow.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MainWindow.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Store : Page
    {
        private StoreVM _storeVm;
        private NavigationHandler _navigationHandler;
        public Store()
        {
            this.InitializeComponent();
            _storeVm = DataContext as StoreVM;
            _navigationHandler = NavigationHandler.Instance;
        }

        private void StoreItemClicked(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(VisualTreeHelper.GetParent(sender as FrameworkElement) as FrameworkElement);
        }

        private void BuyButton_OnClick(object sender, RoutedEventArgs e)
        {
            var _Item = sender as DependencyObject;
            while (!(_Item is GridViewItem))
                _Item = VisualTreeHelper.GetParent(_Item);
            _Item.SetValue(GridViewItem.IsSelectedProperty, true);
            _storeVm.PurchaseSelectedGame();
        }

        private void GoToGameButton_OnClick(object sender, RoutedEventArgs e)
        {
            _navigationHandler.NavigateFrame(Type.GetType($"{Application.Current.GetType().Namespace}.Views.GameTemplate"));
        }

        private void StoreSearchTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _storeVm.FilterGames(StoreSearchTextBox.Text);
        }

        private void AddGameButton_OnClick(object sender, RoutedEventArgs e)
        {
            AddGameContentDialog dialog = new AddGameContentDialog();
            dialog.ShowAsync();
        }
    }
}
