using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileDiaryApp.BadgeCheckers
{
    public class Badge001_FirstRecordChecker : IBadgeChecker
    {
        public void Check(double score, BadgeData badgeData)
        {
            if (!badgeData.Badge001_FirstRecord)
            {
                if (badgeData.RecordDays >= 1)
                {
                    badgeData.Badge001_FirstRecord = true;
                }
            }
        }
    }
}
