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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace UnconstrainedMelody
{
    /// <summary>
    /// Provides a set of static utility (and extension) methods for use with delegate
    /// types.
    /// </summary>
    public static class Delegates
    {
        private static void ThrowIfNull(object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
        }

        /// <summary>
        /// Returns the individual delegates comprising the specified value.
        /// Each returned delegate will represent a single method invocation.
        /// This method is effectively a strongly-typed wrapper around
        /// <see cref="Delegate.GetInvocationList"/>.
        /// </summary>
        /// <typeparam name="T">Delegate type</typeparam>
        /// <param name="value">Delegate to split</param>
        /// <returns>A strongly typed array of single delegates.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value" /> is null</exception>
        public static T[] GetInvocationArray<T>(this T value) where T : DelegateConstraint
        {
            ThrowIfNull(value);
            Delegate[] delegates = ((Delegate) (object) value).GetInvocationList();
            T[] ret = new T[delegates.Length];
            delegates.CopyTo(ret, 0);
            return ret;
        }

        /// <summary>
        /// Returns the individual delegates comprising the specified value as an immutable list.
        /// Each returned delegate will represent a single method invocation.
        /// This method is effectively a wrapper around
        /// <see cref="Delegate.GetInvocationList"/>, but returning an immutable list instead
        /// of an array.
        /// </summary>
        /// <typeparam name="T">Delegate type</typeparam>
        /// <param name="value">Delegate to split</param>
        /// <returns>An immutable list of single delegates.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="value" /> is null</exception>
        public static IList<T> GetReadOnlyInvocationList<T>(this T value) where T : DelegateConstraint
        {
            return new ReadOnlyCollection<T>(GetInvocationArray(value));
        }

        #region CreateDelegate (lots of overloads!)
        /// <summary>
        /// See <see cref="Delegate.CreateDelegate(System.Type,System.Reflection.MethodInfo)" />.
        /// </summary>
        /// <typeparam name="T">Delegate type</typeparam>
        /// <param name="method">Method to create delegate for</param>
        /// <returns>A delegate for the given method</returns>
        public static T CreateDelegate<T>(MethodInfo method) where T : DelegateConstraint
        {
            return (T)(object)Delegate.CreateDelegate(typeof(T), method);
        }

        /// <summary>
        /// See <see cref="Delegate.CreateDelegate(System.Type,object,System.Reflection.MethodInfo)" />.
        /// </summary>
        /// <typeparam name="T">Delegate type</typeparam>
        /// <param name="method">Method to create delegate for</param>
        /// <param name="target">The target for the delegate (for instance methods) or the first argument
        /// (for static methods)</param>
        /// <returns>A delegate for the given method</returns>
        public static T CreateDelegate<T>(object target, MethodInfo method) where T : DelegateConstraint
        {
            return (T)(object)Delegate.CreateDelegate(typeof(T), target, method);
        }

        /// <summary>
        /// See <see cref="Delegate.CreateDelegate(System.Type,object,string)" />.
        /// </summary>
        /// <typeparam name="T">Delegate type</typeparam>
        /// <param name="target">The target for the delegate</param>
        /// <param name="methodName">Name of instance method to create delegate for</param>
        /// <returns>A delegate for the given method</returns>
        public static T CreateDelegate<T>(object target, string methodName) where T : DelegateConstraint
        {
            return (T)(object)Delegate.CreateDelegate(typeof(T), target, methodName);
        }

        /// <summary>
        /// See <see cref="Delegate.CreateDelegate(System.Type,System.Type,string)" />.
        /// </summary>
        /// <typeparam name="T">Delegate type</typeparam>
        /// <param name="targetType">The type containing the static method</param>
        /// <param name="methodName">Name of static method to create delegate for</param>
        /// <returns>A delegate for the given method</returns>
        public static T CreateDelegate<T>(Type targetType, string methodName) where T : DelegateConstraint
        {
            return (T)(object)Delegate.CreateDelegate(typeof(T), targetType, methodName);
        }

        /// <summary>
        /// See <see cref="Delegate.CreateDelegate(System.Type,System.Reflection.MethodInfo,bool)" />.
        /// </summary>
        /// <typeparam name="T">Delegate type</typeparam>
        /// <param name="method">Method to create delegate for</param>
        /// <param name="throwOnBindFailure">Whether or not to throw an exception on bind failure</param>
        /// <returns>A delegate for the given method</returns>
        public static T CreateDelegate<T>(MethodInfo method, bool throwOnBindFailure) where T : DelegateConstraint
        {
            return (T)(object)Delegate.CreateDelegate(typeof(T), method, throwOnBindFailure);
        }

        /// <summary>
        /// See <see cref="Delegate.CreateDelegate(System.Type,object,System.Reflection.MethodInfo,bool)" />.
        /// </summary>
        /// <typeparam name="T">Delegate type</typeparam>
        /// <param name="method">Method to create delegate for</param>
        /// <param name="target">The target for the delegate (for instance methods) or the first argument
        /// (for static methods)</param>
        /// <param name="throwOnBindFailure">Whether or not to throw an exception on bind failure</param>
        /// <returns>A delegate for the given method</returns>
        public static T CreateDelegate<T>(object target, MethodInfo method, bool throwOnBindFailure) where T : DelegateConstraint
        {
            return (T)(object)Delegate.CreateDelegate(typeof(T), target, method, throwOnBindFailure);
        }

        /// <summary>
        /// See <see cref="Delegate.CreateDelegate(System.Type,object,string,bool)" />.
        /// </summary>
        /// <typeparam name="T">Delegate type</typeparam>
        /// <param name="target">The target for the delegate</param>
        /// <param name="methodName">Name of instance method to create delegate for</param>
        /// <param name="ignoreCase">Whether the name should be matched in a case-insensitive manner</param>
        /// <returns>A delegate for the given method</returns>
        public static T CreateDelegate<T>(object target, string methodName, bool ignoreCase) where T : DelegateConstraint
        {
            return (T)(object)Delegate.CreateDelegate(typeof(T), target, methodName, ignoreCase);
        }

        /// <summary>
        /// See <see cref="Delegate.CreateDelegate(System.Type,System.Type,string,bool)" />.
        /// </summary>
        /// <typeparam name="T">Delegate type</typeparam>
        /// <param name="targetType">The type containing the static method</param>
        /// <param name="methodName">Name of static method to create delegate for</param>
        /// <param name="ignoreCase">Whether the name should be matched in a case-insensitive manner</param>
        /// <returns>A delegate for the given method</returns>
        public static T CreateDelegate<T>(Type targetType, string methodName, bool ignoreCase) where T : DelegateConstraint
        {
            return (T)(object)Delegate.CreateDelegate(typeof(T), targetType, methodName, ignoreCase);
        }

        /// <summary>
        /// See <see cref="Delegate.CreateDelegate(System.Type,object,string,bool,bool)" />.
        /// </summary>
        /// <typeparam name="T">Delegate type</typeparam>
        /// <param name="target">The target for the delegate</param>
        /// <param name="methodName">Name of instance method to create delegate for</param>
        /// <param name="ignoreCase">Whether the name should be matched in a case-insensitive manner</param>
        /// <param name="throwOnBindFailure">Whether or not to throw an exception on bind failure</param>
        /// <returns>A delegate for the given method</returns>
        public static T CreateDelegate<T>(object target, string methodName, bool ignoreCase, bool throwOnBindFailure) where T : DelegateConstraint
        {
            return (T)(object)Delegate.CreateDelegate(typeof(T), target, methodName, ignoreCase, throwOnBindFailure);
        }

        /// <summary>
        /// See <see cref="Delegate.CreateDelegate(System.Type,System.Type,string,bool,bool)" />.
        /// </summary>
        /// <typeparam name="T">Delegate type</typeparam>
        /// <param name="targetType">The type containing the static method</param>
        /// <param name="methodName">Name of static method to create delegate for</param>
        /// <param name="ignoreCase">Whether the name should be matched in a case-insensitive manner</param>
        /// <param name="throwOnBindFailure">Whether or not to throw an exception on bind failure</param>
        /// <returns>A delegate for the given method</returns>
        public static T CreateDelegate<T>(Type targetType, string methodName, bool ignoreCase, bool throwOnBindFailure) where T : DelegateConstraint
        {
            return (T)(object)Delegate.CreateDelegate(typeof(T), targetType, methodName, ignoreCase, throwOnBindFailure);
        }
        #endregion
    }
}
