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
        public void SaveText(string filename, string text)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            File.WriteAllText(filePath, text);
        }

        public string LoadText(string filename)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var filePath = Path.Combine(documentsPath, filename);
            return File.ReadAllText(filePath);
        }
    }
}
