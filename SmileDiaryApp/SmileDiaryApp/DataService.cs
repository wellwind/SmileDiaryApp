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

        public void SavePhotoData(MediaFile currentFile, double score)
        {
            var data = LoadPhotoData().ToList();
            var date = DateTime.Now.ToString("yyyy/MM/dd");
            var record = data.FirstOrDefault(rec => rec.Date.Equals(date));
            if (record != null)
            {
                record.Path = currentFile.Path;
                record.score = score;
            }
            else
            {
                data.Add(new SmileRecord()
                {
                    Path = currentFile.AlbumPath,
                    Date = date,
                    score = score
                });
            }
            this.fileService.SaveText(dbPath, JsonHelper.serialize(data));
        }
    }
}
