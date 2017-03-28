using SmileDiaryApp.BadgeCheckers;
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

        private IEnumerable<IBadgeChecker> badgeCheckers;

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

        private void initBadgeCheckers()
        {
            badgeCheckers = new List<IBadgeChecker>()
            {
                new ContinuousDaysChecker()
            };
        }

        public void AddRecord(double score)
        {
            if (String.IsNullOrEmpty(badgeData.LastRecordDate))
            {
                badgeData.LastRecordDate = DateTime.Now.ToString("yyyy/MM/dd");
            }

            foreach(var checker in badgeCheckers)
            {
                checker.Check(score, badgeData);
            }
           
            badgeData.LastRecordDate = DateTime.Now.ToString("yyyy/MM/dd");
            saveRecord();
        }

       

        private void saveRecord()
        {
            fileService.SaveText(dbPath, JsonHelper.serialize(badgeData));
        }
    }
}
