using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace FanKit.Samples
{
    public static class File
    {
        /// <summary>
        /// Gets text from File.txt.
        /// </summary>
        /// <param name="path">file's path </param>
        /// <returns></returns>
        public static async Task<string> GetFile(string path)
        {
            Uri uri = new Uri(path);
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(uri);
            return await FileIO.ReadTextAsync(file);
        }         
    }
}