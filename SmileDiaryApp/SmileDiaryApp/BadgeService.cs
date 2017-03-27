using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileDiaryApp
{
    public class BadgeService
    {
        private string dbPath = "smile-diary-badges.db.txt";
        private IFileService fileService;
        private BadgeData badgeData;

        public BadgeService(IFileService fileService)
        {
            this.fileService = fileService;

            if (!fileService.FileExist(dbPath))
            {
                badgeData = new BadgeData();
                saveRecord();
            }

            badgeData = JsonHelper.Deserialize<BadgeData>(fileService.LoadText(dbPath));
        }

        public void AddRecord(double record)
        {
            badgeData.LastRecordDate = DateTime.Now.ToString("yyyy/MM/dd");

            // TODO: 檢查是否取得成就, 以及成就進度紀錄

            saveRecord();
        }

        private void saveRecord()
        {
            fileService.SaveText(dbPath, JsonHelper.serialize(badgeData));
        }
    }
}
