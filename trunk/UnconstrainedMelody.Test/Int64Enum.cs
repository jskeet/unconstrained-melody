using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnconstrainedMelody.Test
{
    [Flags]
    public enum Int64Enum : long
    {
        MinusOne = -1,
        Zero = 0,
        Max = 0x7FFFFFFFFFFFFFFF
    }
}
