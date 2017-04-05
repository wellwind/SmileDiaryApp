using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileDiaryApp.BadgeCheckers
{
    public class Badge101_3Days80Checker : IBadgeChecker
    {
        public void Check(double score, BadgeData badgeData)
        {
            if (!badgeData.Badge101_3Days80)
            {
                if(badgeData.Smile80Days >= 3)
                {
                    badgeData.Badge101_3Days80 = true;
                }
            }
        }
    }
}
