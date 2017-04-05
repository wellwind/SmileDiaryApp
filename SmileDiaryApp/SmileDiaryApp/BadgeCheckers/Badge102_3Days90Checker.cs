using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileDiaryApp.BadgeCheckers
{
    public class Badge102_3Days90Checker : IBadgeChecker
    {
        public void Check(double score, BadgeData badgeData)
        {
            if (!badgeData.Badge102_3Days90)
            {
                if (badgeData.Smile90Days >= 3)
                {
                    badgeData.Badge102_3Days90 = true;
                }
            }
        }
    }
}
