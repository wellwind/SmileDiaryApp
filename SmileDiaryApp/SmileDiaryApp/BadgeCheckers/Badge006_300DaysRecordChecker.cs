using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileDiaryApp.BadgeCheckers
{
    public class Badge006_300DaysRecordChecker : IBadgeChecker
    {
        public void Check(double score, BadgeData badgeData)
        {
            if (!badgeData.Badge006_300DaysRecord)
            {
                if (badgeData.RecordDays >= 300)
                {
                    badgeData.Badge006_300DaysRecord = true;
                }
            }
        }
    }
}
