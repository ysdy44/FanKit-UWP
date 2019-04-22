using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace FanKit.Samples
{
    public  class File
    {
        public static async Task<string> GetFile(string path)
        {
            Uri uri = new Uri(path);
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(uri);
            return await FileIO.ReadTextAsync(file);
        }
    }
}
