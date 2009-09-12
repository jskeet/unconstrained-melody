using System;
using System.Linq;
using NUnit.Framework;
using System.Reflection;

namespace UnconstrainedMelody.Test
{
    [TestFixture]
    public class DelegatesTest
    {
        public static void StaticActionMethod() { }

        public void InstanceActionMethod() { }

        [Test]
        public void GetInvocationArray()
        {
            Action x = StaticActionMethod;
            Action y = InstanceActionMethod;
            Action z = x + y;
            Assert.IsTrue(z.GetInvocationArray().SequenceEqual(new[] { x, y }));
        }

        [Test]
        public void GetReadOnlyInvocationList()
        {
            Action x = StaticActionMethod;
            Action y = InstanceActionMethod;
            Action z = x + y;
            Assert.IsTrue(z.GetReadOnlyInvocationList().SequenceEqual(new[] { x, y }));
        }

        [Test]
        public void CreateDelegateFromStaticMethod()
        {
            MethodInfo method = typeof(DelegatesTest).GetMethod("StaticActionMethod");
            Action action = Delegates.CreateDelegate<Action>(method);
            Action expected = StaticActionMethod;
            Assert.AreEqual(expected, action);
        }

        [Test]
        public void CreateDelegateFromInstanceMethod()
        {
            MethodInfo method = typeof(DelegatesTest).GetMethod("InstanceActionMethod");
            Action action = Delegates.CreateDelegate<Action>(this, method);
            Action expected = InstanceActionMethod;
            Assert.AreEqual(expected, action);
        }

        [Test]
        public void CreateDelegateFromInstanceMethodByName()
        {
            Action action = Delegates.CreateDelegate<Action>(this, "InstanceActionMethod");
            Action expected = InstanceActionMethod;
            Assert.AreEqual(expected, action);
        }

        [Test]
        public void CreateDelegateFromStaticMethodByName()
        {
            Action action = Delegates.CreateDelegate<Action>(GetType(), "StaticActionMethod");
            Action expected = StaticActionMethod;
            Assert.AreEqual(expected, action);
        }

        [Test]
        public void CreateDelegateWithoutTargetSuppressingBindFailure()
        {
            MethodInfo method = typeof(DelegatesTest).GetMethod("InstanceActionMethod");
            Assert.IsNull(Delegates.CreateDelegate<Action>(method, false));
        }

        [Test]
        public void CreateDelegateWithTargetSuppressingBindFailure()
        {
            MethodInfo method = typeof(DelegatesTest).GetMethod("StaticActionMethod");
            Assert.IsNull(Delegates.CreateDelegate<Action>(this, method, false));
        }

        [Test]
        public void CreateDelegateFromInstanceMethodByNameCaseInsensitively()
        {
            Action action = Delegates.CreateDelegate<Action>(this, "INSTANCEACTIONMETHOD", true);
            Action expected = InstanceActionMethod;
            Assert.AreEqual(expected, action);
        }

        [Test]
        public void CreateDelegateFromStaticMethodByNameCaseInsensitively()
        {
            Action action = Delegates.CreateDelegate<Action>(GetType(), "staticactionmethod", true);
            Action expected = StaticActionMethod;
            Assert.AreEqual(expected, action);
        }

        [Test]
        public void CreateDelegateFromInstanceMethodByNameCaseSensitivelySuppressingBindFailure()
        {
            Assert.IsNull(Delegates.CreateDelegate<Action>(this, "INSTANCEACTIONMETHOD", false, false));
        }

        [Test]
        public void CreateDelegateFromStaticMethodByNameCaseSensitively()
        {
            Assert.IsNull(Delegates.CreateDelegate<Action>(GetType(), "staticactionmethod", false, false));
        }

        #region ArgumentNullException tests
        private static void AssertArgumentNullException(Action action)
        {
            TestHelpers.ExpectException<ArgumentNullException>(action);
        }

        [Test]
        public void GetInvocationArrayWithNullValue()
        {
            AssertArgumentNullException(() => Delegates.GetInvocationArray<Action>(null));
        }

        [Test]
        public void GetReadOnlyInvocationListWithNullValue()
        {
            AssertArgumentNullException(() => Delegates.GetReadOnlyInvocationList<Action>(null));
        }
        #endregion
    }
}
