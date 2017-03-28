using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileDiaryApp.BadgeCheckers
{
    public class ContinuousDaysChecker : IBadgeChecker
    {
        public void Check(double score, BadgeData badgeData)
        {
            var lastRecordDate = Convert.ToDateTime(badgeData.LastRecordDate);

            if (IsContinuousDay(DateTime.Now.AddDays(-1), lastRecordDate))
            {
                badgeData.RecordDays++;
            }
            else
            {
                badgeData.RecordDays = 1;
            }
        }

        public bool IsContinuousDay(DateTime yesterday, DateTime lastRecordDate)
        {
            return yesterday.ToString("yyyy/MM/dd").Equals(lastRecordDate.ToString("yyyy/MM/dd"));
        }
    }
}
