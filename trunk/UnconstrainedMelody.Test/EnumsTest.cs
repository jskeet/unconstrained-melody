using System;
using System.Linq;
using NUnit.Framework;

namespace UnconstrainedMelody.Test
{
    [TestFixture]
    public class EnumsTest
    {
        [Test]
        public void GetValuesArray()
        {
            Number[] numbers = Enums.GetValuesArray<Number>();
            Assert.IsTrue(numbers.SequenceEqual(new[] { Number.One, Number.Two, Number.Three }));
        }

        [Test]
        public void GetValuesReturnsSingleton()
        {
            var numbers = Enums.GetValues<Number>();
            var numbers2 = Enums.GetValues<Number>();
            Assert.AreSame(numbers, numbers2);
        }

        [Test]
        public void GetValuesReturnsReadOnlyList()
        {
            var numbers = Enums.GetValues<Number>();
            Assert.IsTrue(numbers.IsReadOnly);
        }

        [Test]
        public void GetValuesReturnsCorrectValues()
        {
            var numbers = Enums.GetValues<Number>();
            Assert.IsTrue(numbers.SequenceEqual(new[] { Number.One, Number.Two, Number.Three }));
        }

        [Test]
        public void IsNamedValue()
        {
            Assert.IsTrue(BitFlags.Flag24.IsNamedValue());
            Assert.IsFalse((BitFlags.Flag1 | BitFlags.Flag2).IsNamedValue());
            Assert.IsTrue(Number.One.IsNamedValue());
            Assert.IsFalse(Enums.IsNamedValue<Number>(0));
        }

        [Test]
        public void TryParseNameNonFlags()
        {
            Number number;
            Assert.IsTrue(Enums.TryParseName("One", out number));
            Assert.AreEqual(Number.One, number);
            Assert.IsFalse(Enums.TryParseName("1", out number));
            Assert.AreEqual((Number) 0, number);
            Assert.IsFalse(Enums.TryParseName("rubbish", out number));
            Assert.AreEqual((Number) 0, number);
        }

        [Test]
        public void TryParseNameFlags()
        {
            BitFlags result;
            Assert.IsTrue(Enums.TryParseName("Flag24", out result));
            Assert.AreEqual(BitFlags.Flag24, result);
            Assert.IsFalse(Enums.TryParseName("1", out result));
            Assert.AreEqual((BitFlags) 0, result);
            Assert.IsFalse(Enums.TryParseName("rubbish", out result));
            Assert.AreEqual((BitFlags) 0, result);
            Assert.IsFalse(Enums.TryParseName("Flag2,Flag4", out result));
            Assert.AreEqual((BitFlags) 0, result);
        }

        [Test]
        public void ParseNameInvalidValue()
        {
            TestHelpers.ExpectException<ArgumentException>(() => Enums.ParseName<Number>("rubish"));
        }

        [Test]
        public void ParseName()
        {
            Assert.AreEqual(Number.Two, Enums.ParseName<Number>("Two"));
        }
    }
}
