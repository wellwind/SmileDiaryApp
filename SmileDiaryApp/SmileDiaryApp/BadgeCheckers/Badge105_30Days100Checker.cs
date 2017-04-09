﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileDiaryApp.BadgeCheckers
{
    public class Badge105_30Days100Checker : IBadgeChecker
    {
        public void Check(double score, BadgeData badgeData)
        {
            if (!badgeData.Badge105_30Days100)
            {
                if (badgeData.Smile100Days >= 30)
                {
                    badgeData.Badge105_30Days100 = true;
                }
            }
        }
    }
}
