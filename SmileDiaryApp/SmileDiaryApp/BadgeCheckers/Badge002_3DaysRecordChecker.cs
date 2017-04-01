using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileDiaryApp.BadgeCheckers
{
    public class Badge002_3DaysRecordChecker : IBadgeChecker
    {
        public void Check(double score, BadgeData badgeData)
        {
            if (!badgeData.Badge002_3DaysRecord)
            {
                if (badgeData.RecordDays >= 3)
                {
                    badgeData.Badge002_3DaysRecord = true;
                }
            }
        }
    }
}
