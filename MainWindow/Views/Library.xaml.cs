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
    public sealed partial class Library : Page
    {
        private NavigationHandler _navigationHandler;
        private LibraryViewModel _libraryViewModel;

        public Library()
        {
            this.InitializeComponent();
            _navigationHandler = NavigationHandler.Instance;
            _libraryViewModel = DataContext as LibraryViewModel;
        }

        private void BaseExample_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBlock_SelectionChanged_1()
        {

        }

        private void BaseExample_SelectionChanged_1()
        {

        }

        private void TextBlock_SelectionChanged()
        {

        }

        private void ScrollViewer_ViewChanged()
        {

        }

        private void AutoSuggestBox_OnTextChanged_GameCatalog(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            throw new NotImplementedException();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void GoToGameButton_OnClick(object sender, RoutedEventArgs e)
        { 
            _navigationHandler.NavigateFrame(Type.GetType($"{Application.Current.GetType().Namespace}.Views.GameTemplate"));
        }

        private void Library_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LibrarySearchBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _libraryViewModel.FilterGames(LibrarySearchBox.Text);
        }
    }
}
