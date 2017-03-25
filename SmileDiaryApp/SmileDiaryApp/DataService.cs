using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmileDiaryApp
{
    public class DataService
    {
        private string dbPath = "smile-diary.db.txt";
        private IFileService fileService;

        public DataService(IFileService fileService)
        {
            this.fileService = fileService;
        }
        private IEnumerable<SmileRecord> getData()
        {
            try
            {
                var data = this.fileService.LoadText(dbPath);
                return JsonHelper.Deserialize<List<SmileRecord>>(data);
            }
            catch (Exception ex)
            {
                return new List<SmileRecord>();
            }
        }

        public IEnumerable<SmileRecord> LoadPhotoData()
        {
            var data = getData();
            return data;
        }

        public bool TodayHasPhoto()
        {
            var data = LoadPhotoData();
            var date = DateTime.Now.ToString("yyyy/MM/dd");
            var record = data.FirstOrDefault(rec => rec.Date.Equals(date));
            return record != null;
        }

        public string GetPhotoPath(string dateWithoutSlash)
        {
            var filename = String.Format("smile-diary-image-{0}.jpg", dateWithoutSlash);
            return fileService.GetSavedFilePath(filename);
        }

        public void SavePhotoData(MediaFile currentFile, double score)
        {
            var data = LoadPhotoData().ToList();
            var date = DateTime.Now.ToString("yyyy/MM/dd");
            var filename = String.Format("smile-diary-image-{0}.jpg",
                DateTime.Now.ToString("yyyyMMdd"));

            fileService.CopyFile(currentFile.Path, filename);
            var record = data.FirstOrDefault(rec => rec.Date.Equals(date));
            if (record != null)
            {
                record.Score = score;
            }
            else
            {
                data.Add(new SmileRecord()
                {
                    Date = date,
                    Score = score
                });
            }
            this.fileService.SaveText(dbPath, JsonHelper.serialize(data));
        }
    }
}
