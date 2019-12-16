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
        #region Constructor
        public ApplicationSettingsVM()
        {
            DoOpenFolder = new RelayCommand(OpenFolder);
        }
        #endregion

        #region Properties
        public RelayCommand DoOpenFolder { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Method for opening the storage folder we use for storing data in (localfolder)
        /// </summary>
        public async void OpenFolder()
        {
            await Launcher.LaunchFolderAsync(ApplicationData.Current.LocalFolder);
        }
        #endregion
    }
}
