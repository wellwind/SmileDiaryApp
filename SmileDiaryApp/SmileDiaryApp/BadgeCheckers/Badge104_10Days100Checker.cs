using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileDiaryApp.BadgeCheckers
{
    public class Badge104_10Days100Checker : IBadgeChecker
    {
        public void Check(double score, BadgeData badgeData)
        {
            if (!badgeData.Badge104_10Days100)
            {
                if (badgeData.Smile100Days >= 10)
                {
                    badgeData.Badge104_10Days100 = true;
                }
            }
        }
    }
}
