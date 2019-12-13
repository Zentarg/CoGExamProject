using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json;

namespace MainWindow.Models
{
    public static class FileHandler
    {
        private static readonly StorageFolder _storageFolder = ApplicationData.Current.LocalFolder;

        public static async void WriteFile(string fileName, object objectToSave)
        {
            string _json = JsonConvert.SerializeObject(objectToSave);
            await FileIO.WriteTextAsync(await OpenOrCreateFile(fileName), _json);
        }

        public static async Task<StorageFile> CopyFile(string folderPath, StorageFile fileToCopy)
        {
            return await fileToCopy.CopyAsync(await StorageFolder.GetFolderFromPathAsync(_storageFolder.Path + folderPath), fileToCopy.Name, NameCollisionOption.GenerateUniqueName);
        }

        public static async void RemoveFile(StorageFile fileToRemove)
        {
            await fileToRemove.DeleteAsync();
        }

        public static async void RemoveFile(string filePath)
        {
            await (await StorageFile.GetFileFromPathAsync(_storageFolder + filePath)).DeleteAsync();
        }

        public static bool FileExists(string fileName)
        {
            string path = _storageFolder.Path;

            return _storageFolder.TryGetItemAsync(fileName) != null;
        }

        public static async Task<StorageFile> ReadStorageFile(string filePath, bool fullpath)
        {
            if (fullpath)
                return await StorageFile.GetFileFromPathAsync(filePath);
            else
                return await StorageFile.GetFileFromPathAsync(_storageFolder + filePath);

        }

        public static async Task<String> ReadFile(string fileName)
        {
            string json = await FileIO.ReadTextAsync(await OpenOrCreateFile(fileName));
            return json;
        }

        private static async Task<StorageFile> OpenOrCreateFile(string fileName)
        {
            return await _storageFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
        }



    }
}
