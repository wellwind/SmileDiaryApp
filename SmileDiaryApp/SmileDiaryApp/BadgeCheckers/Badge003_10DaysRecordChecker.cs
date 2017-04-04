using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileDiaryApp.BadgeCheckers
{
    public class Badge003_10DaysRecordChecker : IBadgeChecker
    {
        public void Check(double score, BadgeData badgeData)
        {
            if (!badgeData.Badge003_10DaysRecord)
            {
                if (badgeData.RecordDays >= 30)
                {
                    badgeData.Badge003_10DaysRecord = true;
                }
            }
        }
    }
}
