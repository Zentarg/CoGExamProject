using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Newtonsoft.Json;

namespace MainWindow.Models
{
    static class FileHandler
    {
        private static readonly StorageFolder _storageFolder = ApplicationData.Current.LocalFolder;

        public static async void WriteFile(string fileName, object objectToSave)
        {
            string _json = JsonConvert.SerializeObject(objectToSave);
            await FileIO.WriteTextAsync(await OpenOrCreateFile(fileName), _json);
        }

        public static bool FileExists(string fileName)
        {
            string path = _storageFolder.Path;

            return _storageFolder.TryGetItemAsync(fileName) != null;
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
