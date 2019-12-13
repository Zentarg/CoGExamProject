using MainWindow.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MainWindow.Models;
using MainWindow.ViewModels;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MainWindow
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly NavigationHandler _navigationHandler;
        private MainPageVM _mainPageVM; 


        public MainPage()
        {
            this.InitializeComponent();
            _navigationHandler = NavigationHandler.Instance;
            _navigationHandler.SetNavigationFrame(MainFrame);
            _mainPageVM = DataContext as MainPageVM;
            
        }


        private void NavigateMainFrameBack(object sender, RoutedEventArgs e)
        {
            _navigationHandler.NavigateFrameBack();
        }

        private void NavigateMainFrameForwards(object sender, RoutedEventArgs e)
        {
            _navigationHandler.NavigateFrameForwards();
        }

        private void ChangePage(object sender, RoutedEventArgs e)
        {
            _navigationHandler.NavigateFrame(Type.GetType($"{Application.Current.GetType().Namespace}.Views.{(sender as Button).Tag}"));
            PageNameTextblock.Text =
                Type.GetType($"{Application.Current.GetType().Namespace}.Views.{(sender as Button).Tag}").Name;
        }

        private void ChangePageMenuFlyoutItem(object sender, RoutedEventArgs e)
        {
            _navigationHandler.NavigateFrame(Type.GetType($"{Application.Current.GetType().Namespace}.Views.{(sender as MenuFlyoutItem).Tag}"));
        }

        private void HamburgerToggle_OnClick(object sender, RoutedEventArgs e)
        {
            MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
        }

        private async void ShowDialog_Click(object sender, RoutedEventArgs e)
        {
            // Show the custom dialog
            CreateAccountDialogBox dialog = new CreateAccountDialogBox();
            await dialog.ShowAsync();

            // Use the returned custom result
            /*if (dialog.Result == MyResult.Yes)
            {
                DialogResult.Text = "Dialog result Yes.";
            }
            else if (dialog.Result == MyResult.Cancle)
            {
                DialogResult.Text = "Dialog result Canceled.";
            }
            else if (dialog.Result == MyResult.No)
            {
                DialogResult.Text = "Dialog result NO.";
            }*/
        }
        
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            CheckoutFlyout.Hide();
        }

        private async void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            AccountLoginDialogBox dialog = new AccountLoginDialogBox();
            await dialog.ShowAsync();
        }

        private void MenuFlyoutItem_Click_1(object sender, RoutedEventArgs e)
        {
            AccountHandler.LogOff();
            _navigationHandler.NavigateFrame(Type.GetType($"{Application.Current.GetType().Namespace}.Views.{"Store"}"));
            PageNameTextblock.Text =
                Type.GetType($"{Application.Current.GetType().Namespace}.Views.{"Store"}").Name;
            //Frame.BackStack.Clear(); <- Needs implementation 
        }

        private void RemoveGame_OnClick(object sender, RoutedEventArgs e)
        {
            var item = sender as DependencyObject;
            while (!(item is ListViewItem))
            {
                item = VisualTreeHelper.GetParent(item);
            }
            item.SetValue(ListViewItem.IsSelectedProperty, true);
            _mainPageVM.RemoveGame();
        }

        private void Border_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Flyout.ShowAttachedFlyout(sender as FrameworkElement);
        }
    }
}

