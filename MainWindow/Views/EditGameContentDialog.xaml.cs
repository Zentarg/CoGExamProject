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
using Windows.UI.Xaml.Navigation;
using MainWindow.Models;
using MainWindow.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MainWindow.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditGameContentDialog : ContentDialog
    {
        private EditGameContentDialogVM _editGameContentDialogVm;
        private FileOpenPicker _imagePicker;
        private FileOpenPicker _videoPicker;
        private bool _closeDialog;
        public EditGameContentDialog()
        {
            this.InitializeComponent();
            _editGameContentDialogVm = DataContext as EditGameContentDialogVM;
            _imagePicker = new FileOpenPicker();
            _imagePicker.ViewMode = PickerViewMode.Thumbnail;
            _imagePicker.SuggestedStartLocation = PickerLocationId.Desktop;
            _imagePicker.FileTypeFilter.Add(".jpg");
            _imagePicker.FileTypeFilter.Add(".jpeg");
            _imagePicker.FileTypeFilter.Add(".png");
            _imagePicker.FileTypeFilter.Add(".gif");
            _videoPicker = new FileOpenPicker();
            _videoPicker.ViewMode = PickerViewMode.Thumbnail;
            _videoPicker.SuggestedStartLocation = PickerLocationId.Desktop;
            _videoPicker.FileTypeFilter.Add(".mp4");
            _videoPicker.FileTypeFilter.Add(".wmv");

            _closeDialog = true;
        }


        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            _closeDialog = true;
        }

        private async void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            _closeDialog = true;
            Constants.AddGameErrors status = _editGameContentDialogVm.EditGame();

            if (status != Constants.AddGameErrors.NoError)
            {
                _closeDialog = false;

                switch (status)
                {
                    case Constants.AddGameErrors.NameInvalid:
                        Flyout.ShowAttachedFlyout(GameNameTextBox);
                        break;
                    case Constants.AddGameErrors.DescriptionInvalid:
                        Flyout.ShowAttachedFlyout(GameDescriptionTextBox);
                        break;
                    case Constants.AddGameErrors.PriceInvalid:
                        Flyout.ShowAttachedFlyout(GamePriceTextBox);
                        break;
                    case Constants.AddGameErrors.CategoriesInvalid:
                        Flyout.ShowAttachedFlyout(CategoriesTextBox);
                        break;
                }

            }


        }

        private async void ThumbnailImage_OnDrop(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                var items = await e.DataView.GetStorageItemsAsync();
                if (items.Count > 0)
                {
                    if ((items[0] as StorageFile).FileType == ".jpg" || (items[0] as StorageFile).FileType == ".jpeg" || (items[0] as StorageFile).FileType == ".png" || (items[0] as StorageFile).FileType == ".gif")
                    {
                        StorageFile file =
                            await FileHandler.CopyFile(Constants.ThumbnailImageFolderPath, items[0] as StorageFile);
                        _editGameContentDialogVm.ThumbnailImagePath = file.Path;

                    }
                }
            }
        }

        private async void ThumbnailImage_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            StorageFile thumbnailImage = await _imagePicker.PickSingleFileAsync();
            if (thumbnailImage != null)
            {
                thumbnailImage = await FileHandler.CopyFile(Constants.ThumbnailImageFolderPath, thumbnailImage);
                _editGameContentDialogVm.ThumbnailImagePath = thumbnailImage.Path;
            }
        }

        private async void ThumbnailImageChangeButton_OnClick(object sender, RoutedEventArgs e)
        {
            StorageFile thumbnailImage = await _imagePicker.PickSingleFileAsync();
            if (thumbnailImage != null)
            {
                thumbnailImage = await FileHandler.CopyFile(Constants.ThumbnailImageFolderPath, thumbnailImage);
                _editGameContentDialogVm.ThumbnailImagePath = thumbnailImage.Path;
            }
        }

        private void ThumbnailImage_OnDragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
        }

        private async void CarrouselVideosAddButton_OnClick(object sender, RoutedEventArgs e)
        {
            IReadOnlyList<StorageFile> carrouselVideos = await _videoPicker.PickMultipleFilesAsync();
            if (carrouselVideos.Count != 0)
            {
                foreach (StorageFile video in carrouselVideos)
                {
                    StorageFile newVideo = await FileHandler.CopyFile(Constants.CarrouselItemFolderPath, video);
                    _editGameContentDialogVm.CarrouselVideos.Add(newVideo);
                }
            }

        }

        private async void CarrouselVideosGridView_OnDrop(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                var items = await e.DataView.GetStorageItemsAsync();
                foreach (StorageFile item in items)
                {
                    if (item.FileType.ToLower() == ".mp4" || item.FileType.ToLower() == ".wmv")
                    {
                        StorageFile newItem = await FileHandler.CopyFile(Constants.CarrouselItemFolderPath, item);
                        _editGameContentDialogVm.CarrouselVideos.Add(newItem);
                    }
                }
            }
        }

        private void CarrouselVideosGridView_OnDragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
        }

        private void CarrouselVideosDelButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_editGameContentDialogVm.SelectedCarrouselVideo != null)
            {
                FileHandler.RemoveFile(_editGameContentDialogVm.SelectedCarrouselVideo);
                _editGameContentDialogVm.CarrouselVideos.Remove(_editGameContentDialogVm.SelectedCarrouselVideo);
            }
        }

        private async void CarrouselImagesAddButton_OnClick(object sender, RoutedEventArgs e)
        {
            IReadOnlyList<StorageFile> carrouselImages = await _imagePicker.PickMultipleFilesAsync();
            if (carrouselImages.Count != 0)
            {
                foreach (StorageFile image in carrouselImages)
                {
                    StorageFile newImage = await FileHandler.CopyFile(Constants.CarrouselItemFolderPath, image);
                    _editGameContentDialogVm.CarrouselImages.Add(newImage);
                }
            }
        }

        private async void CarrouselImagesGridView_OnDrop(object sender, DragEventArgs e)
        {
            if (e.DataView.Contains(StandardDataFormats.StorageItems))
            {
                var items = await e.DataView.GetStorageItemsAsync();
                foreach (StorageFile item in items)
                {
                    if (item.FileType.ToLower() == ".jpg" || item.FileType.ToLower() == ".jpeg" || item.FileType.ToLower() == ".png" || item.FileType.ToLower() == ".gif")
                    {
                        StorageFile newItem = await FileHandler.CopyFile(Constants.CarrouselItemFolderPath, item);
                        _editGameContentDialogVm.CarrouselImages.Add(newItem);
                    }
                }
            }
        }

        private void CarrouselImagesGridView_OnDragOver(object sender, DragEventArgs e)
        {
            e.AcceptedOperation = DataPackageOperation.Copy;
        }

        private void CarrouselImagesDelButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_editGameContentDialogVm.SelectedCarrouselImage != null)
            {
                FileHandler.RemoveFile(_editGameContentDialogVm.SelectedCarrouselImage);
                _editGameContentDialogVm.CarrouselImages.Remove(_editGameContentDialogVm.SelectedCarrouselImage);
            }
        }

        private void CarrouselYoutubeAddButton_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            ListviewString _newItem = new ListviewString("https://www.youtube.com/embed/");

            _editGameContentDialogVm.CarrouselYoutubeVids.Add(_newItem);
            ScrollViewer.UpdateLayout();
            ScrollViewer.ChangeView(ScrollViewer.HorizontalOffset, ScrollViewer.ScrollableHeight, ScrollViewer.ZoomFactor, false);
        }

        private void CarrouselYoutubeDelButton_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            var item = sender as DependencyObject;
            while (!(item is ListViewItem))
            {
                item = VisualTreeHelper.GetParent(item);
            }
            item.SetValue(ListViewItem.IsSelectedProperty, true);
            _editGameContentDialogVm.CarrouselYoutubeVids.Remove(_editGameContentDialogVm.SelectedCarrouselYoutubeVid);
        }

        private void EditGameContentDialog_OnClosing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            if (!_closeDialog)
            {
                args.Cancel = true;
            }
        }

    }
}
