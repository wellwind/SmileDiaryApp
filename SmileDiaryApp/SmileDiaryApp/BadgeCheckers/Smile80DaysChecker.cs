using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileDiaryApp.BadgeCheckers
{
    public class Smile80DaysChecker : IBadgeChecker
    {
        public void Check(double score, BadgeData badgeData)
        {
            if(score >= 80)
            {
                ++badgeData.Smile80Days;
            }
        }
    }
}
