using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileDiaryApp.BadgeCheckers
{
    public class Badge103_3Days100Checker : IBadgeChecker
    {
        public void Check(double score, BadgeData badgeData)
        {
            if (!badgeData.Badge103_3Days100)
            {
                if (badgeData.Smile100Days >= 3)
                {
                    badgeData.Badge103_3Days100 = true;
                }
            }
        }
    }
}
