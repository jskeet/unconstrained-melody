using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnconstrainedMelody.Test
{
    [Flags]
    public enum UInt64Enum : ulong
    {
        Zero = 0,
        BigValue = 0xFFFFFFFFFFFFFFFF
    }
}
