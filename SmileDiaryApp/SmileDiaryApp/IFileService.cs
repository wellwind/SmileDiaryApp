using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileDiaryApp
{
    public interface IFileService
    {
        string GetSavedFilePath(string filename);
        void SaveText(string filename, string text);
        string LoadText(string filename);
        void CopyFile(string from, string to);
    }
}
