using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileDiaryApp.BadgeCheckers
{
    public interface IBageChecker
    {
        bool Check(IEnumerable<SmileRecord> smileRecord, BadgeData badgeData);
    }
}
