using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileDiaryApp.BadgeCheckers
{
    public class Smile100DaysChecker : IBadgeChecker
    {
        public void Check(double score, BadgeData badgeData)
        {
            if(score.ToString("0.00").Equals("100.00"))
            {
                ++badgeData.Smile100Days;
            }
        }
    }
}
