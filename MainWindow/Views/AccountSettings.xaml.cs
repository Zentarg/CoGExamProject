using MainWindow.Models;
using MainWindow.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MainWindow.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AccountSettings : Page
    {
        private NavigationHandler _navigationHandler;
        private AccountSettingsVM vm;
        private FileOpenPicker _imagePicker;
        private GameList _gameList;

        public AccountSettings()
        {
            this.InitializeComponent();
            _navigationHandler = NavigationHandler.Instance;
            vm = DataContext as AccountSettingsVM;
            _imagePicker = new FileOpenPicker();
            _imagePicker.ViewMode = PickerViewMode.Thumbnail;
            _imagePicker.SuggestedStartLocation = PickerLocationId.Desktop;
            _imagePicker.FileTypeFilter.Add(".jpg");
            _imagePicker.FileTypeFilter.Add(".jpeg");
            _imagePicker.FileTypeFilter.Add(".png");
            _imagePicker.FileTypeFilter.Add(".gif");
            _gameList = GameList.Instance;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            ChangePassword dialogbox = new ChangePassword();
            await dialogbox.ShowAsync();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            _navigationHandler.NavigateFrame(Type.GetType($"{Application.Current.GetType().Namespace}.Views.PurchaseHistory"));
        }


        private async void ProfilePicture_OnDrop(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                var items = await e.DataView.GetStorageItemsAsync();
                if (items.Count > 0)
                {
                    StorageFile file = await FileHandler.CopyFile(Constants.AccountImageFolderPath, items[0] as StorageFile);

                    BitmapImage temp = new BitmapImage();
                    temp.SetSource(await file.OpenAsync(FileAccessMode.Read));
                    (sender as PersonPicture).ProfilePicture = temp;

                    AccountHandler.AccountDetail.ProfilePicturePath = file.Path;
                    AccountHandler.SetProfileImagePathForUI = file.Path;
                    AccountHandler.MainPageVm?.CallForAccountStatus();
                    AccountHandler.AccountDetail.CreateUserDetailsFile(AccountHandler.AccountDetail, AccountHandler.Account.UserName);
                }

            }
        }

        private async void ProfilePicture_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            StorageFile profileImage = await _imagePicker.PickSingleFileAsync();
            if (profileImage != null)
            {
                profileImage = await FileHandler.CopyFile(Constants.AccountImageFolderPath, profileImage);

                BitmapImage temp = new BitmapImage();
                temp.SetSource(await profileImage.OpenAsync(FileAccessMode.Read));
                (sender as PersonPicture).ProfilePicture = temp;

                AccountHandler.AccountDetail.ProfilePicturePath = profileImage.Path;
                AccountHandler.SetProfileImagePathForUI = profileImage.Path;
                AccountHandler.MainPageVm?.CallForAccountStatus();
                AccountHandler.AccountDetail.CreateUserDetailsFile(AccountHandler.AccountDetail, AccountHandler.Account.UserName);
            }
        }

        private async void ProfilePictureChangeButton_OnClick(object sender, RoutedEventArgs e)
        {
            StorageFile profileImage = await _imagePicker.PickSingleFileAsync();
            if (profileImage != null)
            {
                profileImage = await FileHandler.CopyFile(Constants.AccountImageFolderPath, profileImage);

                BitmapImage temp = new BitmapImage();
                temp.SetSource(await profileImage.OpenAsync(FileAccessMode.Read));
                PfpImage.ProfilePicture = temp;

                AccountHandler.AccountDetail.ProfilePicturePath = profileImage.Path;
                AccountHandler.SetProfileImagePathForUI = profileImage.Path;
                AccountHandler.MainPageVm?.CallForAccountStatus();
                AccountHandler.AccountDetail.CreateUserDetailsFile(AccountHandler.AccountDetail, AccountHandler.Account.UserName);
            }
        }

        private void ProfilePicture_OnDragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
        }

        private void GamePage_OnClick(object sender, RoutedEventArgs e)
        {
            var item = sender as DependencyObject;
            while (!(item is ListViewItem))
            {
                item = VisualTreeHelper.GetParent(item);
            }
            item.SetValue(ListViewItem.IsSelectedProperty, true);

            _gameList.SelectedGame = vm.SelectedGame;
            _navigationHandler.NavigateFrame(Type.GetType($"{Application.Current.GetType().Namespace}.Views.GameTemplate"));
        }
    }
}
