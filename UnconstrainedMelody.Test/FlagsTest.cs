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
        public void IsValidCombination()
        {
            Assert.IsTrue(BitFlags.Flag24.IsValidCombination());
            Assert.IsTrue((BitFlags.Flag1 | BitFlags.Flag2).IsValidCombination());
            Assert.IsTrue(Flags.IsValidCombination<BitFlags>(0));
            Assert.IsFalse(((BitFlags)100).IsValidCombination());
        }

        [Test]
        public void Or()
        {
            Assert.AreEqual(BitFlags.Flag1 | BitFlags.Flag2, BitFlags.Flag1.Or(BitFlags.Flag2));
            Assert.AreEqual(BitFlags.Flag1, BitFlags.Flag1.Or(BitFlags.Flag1));
        }

        [Test]
        public void And()
        {
            Assert.AreEqual(BitFlags.Flag2 & BitFlags.Flag24, BitFlags.Flag2.And(BitFlags.Flag24));
            Assert.AreEqual(BitFlags.Flag24 & BitFlags.Flag2, BitFlags.Flag24.And(BitFlags.Flag2));
            Assert.AreEqual(BitFlags.Flag1, BitFlags.Flag1.And(BitFlags.Flag1));
        }

        [Test]
        public void GetUsedBits()
        {
            Assert.AreEqual(BitFlags.Flag1 | BitFlags.Flag2 | BitFlags.Flag4, Flags.GetUsedBits<BitFlags>());
        }

        [Test]
        public void InverseUsedBits()
        {
            Assert.AreEqual(BitFlags.Flag1, BitFlags.Flag24.UsedBitsInverse());
            Assert.AreEqual(BitFlags.Flag24, BitFlags.Flag1.UsedBitsInverse());
        }

        [Test]
        public void InverseAllBits()
        {
            Assert.AreEqual(~BitFlags.Flag1, BitFlags.Flag1.AllBitsInverse());
            Assert.AreEqual(~BitFlags.Flag24, BitFlags.Flag24.AllBitsInverse());
        }

        [Test]
        public void HasAny()
        {
            Assert.IsTrue(BitFlags.Flag2.HasAny(BitFlags.Flag24));
            Assert.IsTrue(BitFlags.Flag24.HasAny(BitFlags.Flag2));
            Assert.IsFalse(BitFlags.Flag2.HasAny(BitFlags.Flag1));
        }

        [Test]
        public void HasAll()
        {
            Assert.IsFalse(BitFlags.Flag2.HasAll(BitFlags.Flag24));
            Assert.IsTrue(BitFlags.Flag24.HasAll(BitFlags.Flag2));
            Assert.IsTrue(BitFlags.Flag24.HasAll(BitFlags.Flag24));
            Assert.IsFalse(BitFlags.Flag2.HasAll(BitFlags.Flag1));
        }

        #region TypeArgumentException tests (all very boring)
        private static void AssertTypeArgumentException(Action action)
        {
            TestHelpers.ExpectException<TypeArgumentException>(action);
        }

        [Test]
        public void IsValidCombinationForNonFlags()
        {
            AssertTypeArgumentException(() => Number.Two.IsValidCombination());
        }

        [Test]
        public void AndForNonFlags()
        {
            AssertTypeArgumentException(() => Number.Two.And(Number.One));
        }

        [Test]
        public void OrForNonFlags()
        {
            AssertTypeArgumentException(() => Number.Two.Or(Number.One));
        }

        [Test]
        public void UsedBitsInverseForNonFlags()
        {
            AssertTypeArgumentException(() => Number.Two.UsedBitsInverse());
        }

        [Test]
        public void AllBitsInverseForNonFlags()
        {
            AssertTypeArgumentException(() => Number.Two.AllBitsInverse());
        }

        [Test]
        public void IsEmptyForNonFlags()
        {
            AssertTypeArgumentException(() => Number.Two.IsEmpty());
        }

        [Test]
        public void IsNotEmptyForNonFlags()
        {
            AssertTypeArgumentException(() => Number.Two.IsNotEmpty());
        }

        [Test]
        public void GetUsedBitsForNonFlags()
        {
            AssertTypeArgumentException(() => Flags.GetUsedBits<Number>());
        }

        [Test]
        public void HasAnyForNonFlags()
        {
            AssertTypeArgumentException(() => Number.One.HasAny(Number.Two));
        }

        [Test]
        public void HasAllForNonFlags()
        {
            AssertTypeArgumentException(() => Number.One.HasAll(Number.Two));
        }
        #endregion
    }
}
