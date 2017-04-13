using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileDiaryApp.BadgeCheckers
{
    class Badge802_10DaysLessThan50Checker : IBadgeChecker
    {
        public void Check(double score, BadgeData badgeData)
        {
            if (!badgeData.Badge802_10DaysLessThan50)
            {
                if (badgeData.SmileLessThen50Days >= 10)
                {
                    badgeData.Badge802_10DaysLessThan50 = true;
                }
            }
        }
    }
}
