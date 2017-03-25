using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

[assembly: Xamarin.Forms.Dependency(typeof(SmileDiaryApp.Droid.FileService))]
namespace SmileDiaryApp.Droid
{
    public class FileService : IFileService
    {
        public void SaveText(string filename, string text)
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = System.IO.Path.Combine(documentsPath, filename);
            System.IO.File.WriteAllText(filePath, text);
        }

        public string LoadText(string filename)
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = System.IO.Path.Combine(documentsPath, filename);
            return System.IO.File.ReadAllText(filePath);
        }
    }
}