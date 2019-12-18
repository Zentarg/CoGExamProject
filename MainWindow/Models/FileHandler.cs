using System;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json;

namespace MainWindow.Models
{
    public static class FileHandler
    {
        private static readonly StorageFolder _storageFolder = ApplicationData.Current.LocalFolder;


        /// <summary>
        /// Saves any object as a json file, to the local folder.
        /// </summary>
        public static async void WriteFile(string fileName, object objectToSave)
        {
            string _json = JsonConvert.SerializeObject(objectToSave);
            await FileIO.WriteTextAsync(await OpenOrCreateFile(fileName), _json);
        }

        /// <summary>
        /// Copies a StorageFiler to any folder inside the programs local folder.
        /// </summary>
        public static async Task<StorageFile> CopyFile(string folderPath, StorageFile fileToCopy)
        {
            return await fileToCopy.CopyAsync(await StorageFolder.GetFolderFromPathAsync(_storageFolder.Path + folderPath), fileToCopy.Name, NameCollisionOption.GenerateUniqueName);
        }

        /// <summary>
        /// Deletes whatever file is passed, from the system.
        /// </summary>
        public static async void RemoveFile(StorageFile fileToRemove)
        {
            await fileToRemove.DeleteAsync();
        }

        /// <summary>
        /// Deletes whatever file is stored in the path, from the system.
        /// </summary>
        public static async void RemoveFile(string filePath)
        {
            await (await StorageFile.GetFileFromPathAsync(_storageFolder + filePath)).DeleteAsync();
        }

        /// <summary>
        /// Returns a Task<bool>, depending on whether or not the file passed exists.
        /// </summary>
        public static async Task<bool> FileExists(string fileName)
        {
            return await _storageFolder.TryGetItemAsync(fileName) != null;
        }

        /// <summary>
        /// Returns a Task<StorageFile> from the filepath written. Can be swapped from full path, or from local path based on boolean.
        /// </summary>
        public static async Task<StorageFile> ReadStorageFile(string filePath, bool fullpath)
        {
            if (fullpath)
                return await StorageFile.GetFileFromPathAsync(filePath);
            else
                return await StorageFile.GetFileFromPathAsync(_storageFolder + filePath);

        }

        /// <summary>
        /// Returns a Task<String> with the text from the file passed.
        /// </summary>
        public static async Task<String> ReadFile(string fileName)
        {
            string json = await FileIO.ReadTextAsync(await OpenOrCreateFile(fileName));
            return json;
        }

        /// <summary>
        /// Returns a Task<StorageFile> with the file from the passed filename. If the file doesn't exist already, it will be created.
        /// </summary>
        private static async Task<StorageFile> OpenOrCreateFile(string fileName)
        {
            return await _storageFolder.CreateFileAsync(fileName, CreationCollisionOption.OpenIfExists);
        }

        /// <summary>
        /// Creates the folders we use in local folder, in case they are not created already.
        /// </summary>
        public static async void CreateFolderIfDoesNotExist(string folderName) 
        { 
            await _storageFolder.CreateFolderAsync(folderName, CreationCollisionOption.OpenIfExists);
        }

    }
}
