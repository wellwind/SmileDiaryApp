using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileDiaryApp.BadgeCheckers
{
    public class Badge004_30DaysRecordChecker : IBadgeChecker
    {
        public void Check(double score, BadgeData badgeData)
        {
            if (!badgeData.Badge004_30DaysRecord)
            {
                if (badgeData.RecordDays >= 30)
                {
                    badgeData.Badge004_30DaysRecord = true;
                }
            }
        }
    }
}
