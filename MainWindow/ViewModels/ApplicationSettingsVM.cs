using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.System;

namespace MainWindow.ViewModels
{
    class ApplicationSettingsVM
    {



        public ApplicationSettingsVM()
        {
            DoOpenFolder = new RelayCommand(OpenFolder);
        }

        public RelayCommand DoOpenFolder { get; set; }

        public async void OpenFolder()
        {
            await Launcher.LaunchFolderAsync(ApplicationData.Current.LocalFolder);
        }
    }
}
