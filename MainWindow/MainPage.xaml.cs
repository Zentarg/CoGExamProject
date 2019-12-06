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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MainWindow
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly NavigationHandler _navigationHandler;
        public MainPage()
        {
            this.InitializeComponent();
            _navigationHandler = NavigationHandler.Instance;
            _navigationHandler.SetNavigationFrame(MainFrame);
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
    }
}
