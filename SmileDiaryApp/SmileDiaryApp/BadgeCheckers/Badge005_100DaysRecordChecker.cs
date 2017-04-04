using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileDiaryApp.BadgeCheckers
{
    public class Badge005_100DaysRecordChecker : IBadgeChecker
    {
        public void Check(double score, BadgeData badgeData)
        {
            if (!badgeData.Badge005_100DaysRecord)
            {
                if (badgeData.RecordDays >= 100)
                {
                    badgeData.Badge005_100DaysRecord = true;
                }
            }
        }
    }
}
