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
