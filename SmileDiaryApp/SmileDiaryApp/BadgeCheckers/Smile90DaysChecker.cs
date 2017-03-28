using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileDiaryApp.BadgeCheckers
{
    public class Smile90DaysChecker : IBadgeChecker
    {
        public void Check(double score, BadgeData badgeData)
        {
            if(score >= 90)
            {
                ++badgeData.Smile90Days;
            }
        }
    }
}
