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
        private IEnumerable<IBadgeChecker> continuousChecker;

        public BadgeData BadgeData => badgeData;

        public BadgeService(IFileService fileService)
        {
            this.fileService = fileService;
            initBadgeCheckers();

            if (!fileService.FileExist(dbPath))
            {
                badgeData = new BadgeData();
                saveRecord();
            }

            badgeData = JsonHelper.Deserialize<BadgeData>(fileService.LoadText(dbPath));
        }

        private void initBadgeCheckers()
        {
            continuousChecker = new List<IBadgeChecker>()
            {
                new Smile100DaysChecker(),
                new Smile90DaysChecker(),
                new Smile80DaysChecker(),
                new SmileLessThen50DaysChecker()
            };

            badgeCheckers = new List<IBadgeChecker>()
            {
                new Badge001_FirstRecordChecker()
            };
        }

        public void AddRecord(double score)
        {
            if (String.IsNullOrEmpty(badgeData.LastRecordDate))
            {
                badgeData.LastRecordDate = DateTime.Now.ToString("yyyy/MM/dd");
            }

            // 檢查是否有連續紀錄
            (new ContinuousDaysChecker()).Check(score, badgeData);

            // 有連續, 才繼續檢查
            if (badgeData.RecordDays >= 1)
            {
                foreach (var checker in continuousChecker)
                {
                    checker.Check(score, badgeData);
                }

                foreach (var checker in badgeCheckers)
                {
                    checker.Check(score, badgeData);
                }
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
