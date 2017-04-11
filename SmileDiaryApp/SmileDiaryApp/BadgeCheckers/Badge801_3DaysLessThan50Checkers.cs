using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileDiaryApp.BadgeCheckers
{
    public class Badge801_3DaysLessThan50Checkers : IBadgeChecker
    {
        public void Check(double score, BadgeData badgeData)
        {
            if (!badgeData.Badge801_3DaysLessThan50)
            {
                if (badgeData.SmileLessThen50Days >= 3)
                {
                    badgeData.Badge801_3DaysLessThan50 = true;
                }
            }
        }
    }
}
