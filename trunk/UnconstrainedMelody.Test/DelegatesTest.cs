#region License and Terms
// Unconstrained Melody
// Copyright (c) 2009-2011 Jonathan Skeet. All rights reserved.
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using System;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

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
