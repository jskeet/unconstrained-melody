using System;
using NUnit.Framework;

namespace UnconstrainedMelody.Test
{
    [TestFixture]
    public class DelegatesTest
    {
        [Test]
        public void NoOp()
        {
            Action x = Delegates.NoOp<Action>(() => Console.WriteLine());
            Action y = Delegates.NoOp(x);
            Assert.AreSame(x, y);
        }
    }
}
