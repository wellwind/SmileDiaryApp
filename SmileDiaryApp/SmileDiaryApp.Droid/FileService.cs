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
        public string GetSavedFilePath(string filename)
        {
            var documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var filePath = System.IO.Path.Combine(documentPath, filename);
            return filePath;
        }

        public void SaveText(string filename, string text)
        {
            var filePath = GetSavedFilePath(filename);
            System.IO.File.WriteAllText(filePath, text);
        }

        public string LoadText(string filename)
        {
            var filePath = GetSavedFilePath(filename);
            return System.IO.File.ReadAllText(filePath);
        }

        public void CopyFile(string from, string to)
        {
            var filePath = GetSavedFilePath(to);
            System.IO.File.Copy(from, filePath, true);
        }
    }
}