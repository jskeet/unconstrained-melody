using System.ComponentModel;

namespace UnconstrainedMelody.Test
{
    enum Number
    {
        [Description("First description")]
        One = 1,
        Two = 2,
        [Description("Third description")]
        Three = 3
    }
}
