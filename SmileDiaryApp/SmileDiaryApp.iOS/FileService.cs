using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media.Abstractions;
using SmileDiaryApp;

[assembly: Xamarin.Forms.Dependency(typeof(SmileDiaryApp.iOS.FileService))]
namespace SmileDiaryApp.iOS
{
    public class FileService : IFileService
    {
        public string GetSavedFilePath(string filename)
        {
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentPath, filename);
            return filePath;
        }

        public void SaveText(string filename, string text)
        {
            var filePath = GetSavedFilePath(filename);
            File.WriteAllText(filePath, text);
        }

        public string LoadText(string filename)
        {
            var filePath = GetSavedFilePath(filename);
            return File.ReadAllText(filePath);
        }

        public void CopyFile(string from, string to)
        {
            var filePath = GetSavedFilePath(to);
            File.Copy(from, filePath, true);
        }

        public bool FileExist(string filename)
        {
            var filePath = GetSavedFilePath(filename);
            return File.Exists(filePath);
        }
    }
}
