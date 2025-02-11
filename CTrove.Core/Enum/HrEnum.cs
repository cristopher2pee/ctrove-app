using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTrove.Core.Enum
{
    public class HrEnum
    {
    }

    public enum PrefixType
    {
        None = 0,
        Mr = 1,
        Mrs = 2,
        Ms = 3,
        Miss = 4,
        Dr = 5,
        ProfDr = 6,
        Prof = 7,
        Drs = 8,
        Other = 9,
    }

    public enum GradeType
    {
        None = 0,
        MD = 1,
        MDPhD = 2,
        PhD = 3,
        MSc = 4,
        MBA = 5,
        BSc = 6,
        Other = 7,
    }

    public enum ParentType
    {
        None = 0,
        No = 1,
        Linked = 2,
        Yes = 3
    }
}
