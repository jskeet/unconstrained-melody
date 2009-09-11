using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace UnconstrainedMelody.Test
{
    internal static class TestHelpers
    {
        // For some reason ExpectException doesn't work in the combination of
        // R# and NUnit I'm using...
        internal static void ExpectException<T>(Action action) where T : Exception
        {
            try
            {
                action();
                Assert.Fail("Expected exception of type " + typeof(T).Name);
            }
            catch (T)
            {
                // Expected
            }
        }
    }
}
