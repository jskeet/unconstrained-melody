using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UnconstrainedMelody
{
    public static class Enums
    {
        /// <summary>
        /// Returns an array of values in the enum, in an identical
        /// way to Enum.GetValues - but strongly typed.
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
            return PerTypeFields<T>.Values;
        }

        /// <summary>
        /// Checks whether the value is a named value for the type.
        /// </summary>
        /// <remarks>
        /// For flags enums, it is possible for a value to be a valid
        /// combination of other values without being a named value
        /// in itself. To test for this possibility, use IsValidCombination.
        /// </remarks>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="item">Value to test</param>
        /// <returns>True if this value has a name, False otherwise.</returns>
        public static bool IsNamedValue<T>(this T value) where T : struct, IEnumConstraint
        {
            // TODO: Speed this up a lot :)
            return GetValues<T>().Contains(value);
        }

        /// <summary>
        /// Returns the description for the given value, 
        /// as specified by DescriptionAttribute, or null
        /// if no description is present.
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="item">Value to fetch description for</param>
        /// <returns>The description of the value, or null if no description
        /// has been specified.</returns>
        /// <exception cref="ArgumentException"><paramref name="item"/>
        /// is not a named member of the enum</exception>
        public static string GetDescription<T>(this T item) where T : struct, IEnumConstraint
        {
            return null;
        }

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
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="name">Name to parse</param>
        /// <param name="value">Enum value corresponding to given name</param>
        /// <returns>Whether the parse attempt was successful or not</returns>
        public static bool TryParseName<T>(string name, out T value) where T : struct, IEnumConstraint
        {
            // TODO: Speed this up
            foreach (T candidate in GetValues<T>())
            {
                if (candidate.ToString() == name)
                {
                    value = candidate;
                    return true;
                }
            }
            value = default(T);
            return false;
        }

        private static class PerTypeFields<T> where T : struct, IEnumConstraint
        {
            static internal readonly IList<T> Values;

            static PerTypeFields()
            {
                Values = new ReadOnlyCollection<T>((T[]) Enum.GetValues(typeof(T)));
            }
        }
    }
}
