using System;

namespace UnconstrainedMelody.Test
{
    [Flags]
    enum BitFlags
    {
        Flag1 = 1,
        Flag2 = 2,
        Flag4 = 4,
        Flag24 = Flag2 | Flag4
    }
}
