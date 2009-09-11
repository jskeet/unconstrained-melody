using System;
using NUnit.Framework;

namespace UnconstrainedMelody.Test
{
    [TestFixture]
    public class FlagsTest
    {
        public void IsFlags()
        {
            Assert.IsTrue(Flags.IsFlags<BitFlags>());
            Assert.IsFalse(Flags.IsFlags<Number>());
        }

        [Test]
        [Ignore("Not implemented yet")]
        public void IsValidCombination()
        {
            Assert.IsTrue(BitFlags.Flag24.IsValidCombination());
            Assert.IsTrue((BitFlags.Flag1 | BitFlags.Flag2).IsValidCombination());
            Assert.IsTrue(Flags.IsValidCombination<BitFlags>(0));
            Assert.IsFalse(((BitFlags)100).IsValidCombination());
        }

        #region NotSupportedException tests (all very boring)
        private void AssertNotSupported(Action action)
        {
            TestHelpers.ExpectException<NotSupportedException>(action);
        }

        [Test]
        public void IsValidCombinationForNonFlags()
        {
            AssertNotSupported(() => Number.Two.IsValidCombination());
        }
        #endregion
    }
}
