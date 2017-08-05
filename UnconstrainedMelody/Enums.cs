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

namespace UnconstrainedMelody
{
    /// <summary>
    /// Provides a set of static methods for use with enum types. Much of
    /// what's available here is already in System.Enum, but this class
    /// provides a strongly typed API.
    /// </summary>
    public static class Enums
    {
        /// <summary>
        /// Returns an array of values in the enum.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns>An array of values in the enum</returns>
        public static T[] GetValuesArray<T>() where T : struct, IEnumConstraint
        { 
            return (T[]) Enum.GetValues(typeof(T)); 
        }

        /// <summary>
        /// Returns the values for the given enum as an immutable list.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        public static IList<T> GetValues<T>() where T : struct, IEnumConstraint
        {
            return EnumInternals<T>.Values;
        }

        /// <summary>
        /// Returns an array of names in the enum.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns>An array of names in the enum</returns>
        public static string[] GetNamesArray<T>() where T : struct, IEnumConstraint
        {
            return Enum.GetNames(typeof(T));
        }

        /// <summary>
        /// Returns the names for the given enum as an immutable list.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns>An array of names in the enum</returns>
        public static IList<string> GetNames<T>() where T : struct, IEnumConstraint
        {
            return EnumInternals<T>.Names;
        }

        /// <summary>
        /// Checks whether the value is a named value for the type.
        /// </summary>
        /// <remarks>
        /// For flags enums, it is possible for a value to be a valid
        /// combination of other values without being a named value
        /// in itself. To test for this possibility, use IsValidCombination.
        /// </remarks>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="value">Value to test</param>
        /// <returns>True if this value has a name, False otherwise.</returns>
        public static bool IsNamedValue<T>(this T value) where T : struct, IEnumConstraint
        {
            // TODO: Speed this up for big enums
            return GetValues<T>().Contains(value);
        }

        /// <summary>
        /// Returns the description for the given value, 
        /// as specified by DescriptionAttribute, or null
        /// if no description is present.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="item">Value to fetch description for</param>
        /// <returns>The description of the value, or null if no description
        /// has been specified (but the value is a named value).</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="item"/>
        /// is not a named member of the enum</exception>
        public static string GetDescription<T>(this T item) where T : struct, IEnumConstraint
        {
            string description;
            if (EnumInternals<T>.ValueToDescriptionMap.TryGetValue(item, out description))
            {
                return description;
            }
            throw new ArgumentOutOfRangeException("item");
        }

        /// <summary>
        /// Attempts to find a value with the given description.
        /// </summary>
        /// <remarks>
        /// More than one value may have the same description. In this unlikely
        /// situation, the first value with the specified description is returned.
        /// </remarks>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="description">Description to find</param>
        /// <param name="value">Enum value corresponding to given description (on return)</param>
        /// <returns>True if a value with the given description was found,
        /// false otherwise.</returns>
        public static bool TryParseDescription<T>(string description, out T value)
            where T : struct, IEnumConstraint
        {
            return EnumInternals<T>.DescriptionToValueMap.TryGetValue(description, out value);
        }


        /// <summary>
        /// Parses the description of an enum value.
        /// </summary>
        /// <remarks>
        /// This method only considers named values: it does not parse comma-separated
        /// combinations of flags enums.
        /// </remarks>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns>The parsed value</returns>
        /// <exception cref="ArgumentException">The description could not be parsed.</exception>
        public static T ParseDescription<T>(string description) where T : struct, IEnumConstraint
        {
            T value;
            if (!TryParseDescription(description, out value))
            {
                throw new ArgumentException("Unknown description", "description");
            }
            return value;
        }

        /// <summary>
        /// Parses the name of an enum value.
        /// </summary>
        /// <remarks>
        /// This method only considers named values: it does not parse comma-separated
        /// combinations of flags enums.
        /// </remarks>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns>The parsed value</returns>
        /// <exception cref="ArgumentException">The name could not be parsed.</exception>
        public static T ParseName<T>(string name) where T : struct, IEnumConstraint
        {
            T value;
            if (!TryParseName(name, out value))
            {
                throw new ArgumentException("Unknown name", "name");
            }
            return value;
        }

        /// <summary>
        /// Attempts to find a value for the specified name.
        /// Only names are considered - not numeric values.
        /// </summary>
        /// <remarks>
        /// If the name is not parsed, <paramref name="value"/> will
        /// be set to the zero value of the enum. This method only
        /// considers named values: it does not parse comma-separated
        /// combinations of flags enums.
        /// </remarks>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="name">Name to parse</param>
        /// <param name="value">Enum value corresponding to given name (on return)</param>
        /// <returns>Whether the parse attempt was successful or not</returns>
        public static bool TryParseName<T>(string name, out T value) where T : struct, IEnumConstraint
        {
            // TODO: Speed this up for big enums
            int index = EnumInternals<T>.Names.IndexOf(name);
            if (index == -1)
            {
                value = default(T);
                return false;
            }
            value = EnumInternals<T>.Values[index];
            return true;
        }

        /// <summary>
        /// Returns the underlying type for the enum
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns>The underlying type (Byte, Int32 etc) for the enum</returns>
        public static Type GetUnderlyingType<T>() where T : struct, IEnumConstraint
        {
            return EnumInternals<T>.UnderlyingType;
        }
    }
}
