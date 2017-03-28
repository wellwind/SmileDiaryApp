using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileDiaryApp.BadgeCheckers
{
    public interface IBadgeChecker
    {
        void Check(double score, BadgeData badgeData);
    }
}
