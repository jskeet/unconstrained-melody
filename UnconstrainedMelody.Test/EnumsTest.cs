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
    }
}
