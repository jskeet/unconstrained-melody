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
        public void GetNamesArray()
        {
            Number[] numbers = Enums.GetValuesArray<Number>();
            Assert.IsTrue(numbers.SequenceEqual(new[] { Number.One, Number.Two, Number.Three }));
        }

        [Test]
        public void GetNamesReturnsSingleton()
        {
            var names = Enums.GetNames<Number>();
            var names2 = Enums.GetNames<Number>();
            Assert.AreSame(names, names2);
        }

        [Test]
        public void GetNamesReturnsReadOnlyList()
        {
            var names = Enums.GetNames<Number>();
            Assert.IsTrue(names.IsReadOnly);
        }

        [Test]
        public void GetNamesReturnsCorrectValues()
        {
            var names = Enums.GetNames<Number>();
            Assert.IsTrue(names.SequenceEqual(new[] { "One", "Two", "Three" }));
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
            Assert.AreEqual(BitFlags.Flag24, Enums.ParseName<BitFlags>("Flag24"));
        }

        [Test]
        public void GetUnderlyingType()
        {
            Assert.AreEqual(typeof(byte), Enums.GetUnderlyingType<ByteEnum>());
            Assert.AreEqual(typeof(int), Enums.GetUnderlyingType<Number>());
            Assert.AreEqual(typeof(long), Enums.GetUnderlyingType<Int64Enum>());
            Assert.AreEqual(typeof(ulong), Enums.GetUnderlyingType<UInt64Enum>());
        }

        [Test]
        public void GetDescriptionWhenValueHasDescription()
        {
            Assert.AreEqual("First description", Number.One.GetDescription());
        }

        [Test]
        public void GetDescriptionWhenValueHasNoDescription()
        {
            Assert.IsNull(Number.Two.GetDescription());
        }

        [Test]
        public void GetDescriptionForInvalidValue()
        {
            TestHelpers.ExpectException<ArgumentOutOfRangeException>
                (() => ((Number)4).GetDescription());
        }

        [Test]
        public void ParseUniqueDescription()
        {
            Number number;
            Assert.IsTrue(Enums.TryParseDescription<Number>("Third description", out number));
            Assert.AreEqual(Number.Three, number);
        }

        [Test]
        public void ParseDuplicateDescription()
        {
            BitFlags result;
            Assert.IsTrue(Enums.TryParseDescription<BitFlags>("Duplicate description", out result));
            Assert.AreEqual(BitFlags.Flag2, result);
        }

        [Test]
        public void ParseMissingDescription()
        {
            Number number;
            Assert.IsFalse(Enums.TryParseDescription<Number>("Doesn't exist", out number));
        }
    }
}
