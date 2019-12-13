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
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using MainWindow.ViewModels;
using MainWindow.Models;
using Windows.Storage.Pickers;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MainWindow.Views
{
    public sealed partial class CreateAccountDialogBox : ContentDialog
    {
        private CreateAccountVM vm;
        private FileOpenPicker _imagePicker;

        public CreateAccountDialogBox()
        {
            this.InitializeComponent();
            vm = DataContext as CreateAccountVM;
            _imagePicker = new FileOpenPicker();
            _imagePicker.ViewMode = PickerViewMode.Thumbnail;
            _imagePicker.SuggestedStartLocation = PickerLocationId.Desktop;
            _imagePicker.FileTypeFilter.Add(".jpg");
            _imagePicker.FileTypeFilter.Add(".jpeg");
            _imagePicker.FileTypeFilter.Add(".png");
            _imagePicker.FileTypeFilter.Add(".gif");
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            vm.Confirm();
        }




        private async void ProfilePicture_OnDrop(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                var items = await e.DataView.GetStorageItemsAsync();
                if (items.Count > 0)
                {
                    StorageFile file = await FileHandler.CopyFile(Constants.AccountImageFolderPath, items[0] as StorageFile);
                    vm.TempProfilePicturePath = file.Path;

                    BitmapImage temp = new BitmapImage();
                    temp.SetSource(await file.OpenAsync(FileAccessMode.Read));
                    (sender as PersonPicture).ProfilePicture = temp;
                }

            }
        }

        private async void ProfilePicture_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            StorageFile profileImage = await _imagePicker.PickSingleFileAsync();
            if (profileImage != null)
            {
                profileImage = await FileHandler.CopyFile(Constants.AccountImageFolderPath, profileImage);
                vm.TempProfilePicturePath = profileImage.Path;

                BitmapImage temp = new BitmapImage();
                temp.SetSource(await profileImage.OpenAsync(FileAccessMode.Read));
                (sender as PersonPicture).ProfilePicture = temp;
            }
        }

        private async void ProfilePictureChangeButton_OnClick(object sender, RoutedEventArgs e)
        {
            StorageFile profileImage = await _imagePicker.PickSingleFileAsync();
            if (profileImage != null)
            {
                profileImage = await FileHandler.CopyFile(Constants.AccountImageFolderPath, profileImage);
                vm.TempProfilePicturePath = profileImage.Path;

                BitmapImage temp = new BitmapImage();
                temp.SetSource(await profileImage.OpenAsync(FileAccessMode.Read));
                PfpImage.ProfilePicture = temp;
            }
        }

        private void ProfilePicture_OnDragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
        }
    }
}
