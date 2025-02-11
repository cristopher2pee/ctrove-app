using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.Enum
{
    public enum SubjectStatus
    {
        None = 0,
        Completed = 1,
        Ongoing = 2,
        Randomized = 3,
        Withdrawn = 4,
        ScreenFailed = 5,
        Discontinued = 6,
    }

    public enum SiteStatus
    {
        None = 0,
        Pending = 1,
        Activated = 2,
        InActive = 3,
        NotSelected = 4,
    }
}
