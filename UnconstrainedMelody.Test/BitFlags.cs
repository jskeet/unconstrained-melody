using System;
using System.ComponentModel;

namespace UnconstrainedMelody.Test
{
    [Flags]
    enum BitFlags
    {
        Flag1 = 1,
        [Description("Duplicate description")]
        Flag2 = 2,
        [Description("Duplicate description")]
        Flag4 = 4,
        Flag24 = Flag2 | Flag4
    }
}
