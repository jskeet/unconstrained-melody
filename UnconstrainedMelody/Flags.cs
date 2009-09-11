using System;

namespace UnconstrainedMelody
{
    /// <summary>
    /// Provides a set of static methods for use with "flags" enums,
    /// i.e. those decorated with <see cref="FlagsAttribute"/>.
    /// Other than <see cref="IsValidCombination{T}"/>, methods in this
    /// class throw <see cref="TypeArgumentException" />.
    /// </summary>
    public static class Flags
    {
        /// <summary>
        /// Helper method used by almost all methods to make sure
        /// the type argument is really a flags enum.
        /// </summary>
        static void ThrowIfNotFlags<T>() where T : struct, IEnumConstraint
        {
            if (!EnumInternals<T>.IsFlags)
            {
                throw new TypeArgumentException("Can't call this method for a non-flags enum");
            }
        }

        /// <summary>
        /// Returns whether or not the specified enum is a "flags" enum,
        /// i.e. whether it has FlagsAttribute applied to it.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <returns>True if the enum type is decorated with
        /// FlagsAttribute; False otherwise.</returns>
        public static bool IsFlags<T>() where T : struct, IEnumConstraint
        {
            return EnumInternals<T>.IsFlags;
        }

        /// <summary>
        /// Determines whether the given value only uses bits covered
        /// by named values.
        /// </summary>
        /// internal static
        /// <param name="values">Combination to test</param>
        /// <exception cref="TypeArgumentException"><typeparamref name="T"/> is not a flags enum.</exception>
        public static bool IsValidCombination<T>(this T values) where T : struct, IEnumConstraint
        {
            ThrowIfNotFlags<T>();
            return values.And(EnumInternals<T>.UnusedBits).IsEmpty();
        }

        /// <summary>
        /// Determines whether the two specified values have any flags in common.
        /// </summary>
        /// <param name="value">Value to test</param>
        /// <param name="desiredFlags">Flags we wish to find</param>
        /// <returns>Whether the two specified values have any flags in common.</returns>
        /// <exception cref="TypeArgumentException"><typeparamref name="T"/> is not a flags enum.</exception>
        public static bool HasAny<T>(this T value, T desiredFlags) where T : struct, IEnumConstraint
        {
            ThrowIfNotFlags<T>();
            return value.And(desiredFlags).IsNotEmpty();
        }

        /// <summary>
        /// Determines whether all of the flags in <paramref name="desiredFlags"/>
        /// </summary>
        /// <param name="value">Value to test</param>
        /// <param name="desiredFlags">Flags we wish to find</param>
        /// <returns>Whether all the flags in <paramref name="desiredFlags"/> are in <paramref name="value"/>.</returns>
        /// <exception cref="TypeArgumentException"><typeparamref name="T"/> is not a flags enum.</exception>
        public static bool HasAll<T>(this T value, T desiredFlags) where T : struct, IEnumConstraint
        {
            ThrowIfNotFlags<T>();
            return EnumInternals<T>.Equality(value.And(desiredFlags), desiredFlags);
        }

        /// <summary>
        /// Returns the bitwise "and" of two values.
        /// </summary>
        /// internal static
        /// <param name="first">First value</param>
        /// <param name="second">Second value</param>
        /// <returns>The bitwise "and" of the two values</returns>
        /// <exception cref="TypeArgumentException"><typeparamref name="T"/> is not a flags enum.</exception>
        public static T And<T>(this T first, T second) where T : struct, IEnumConstraint
        {
            ThrowIfNotFlags<T>();
            return EnumInternals<T>.And(first, second);
        }

        /// <summary>
        /// Returns the bitwise "or" of two values.
        /// </summary>
        /// internal static
        /// <param name="first">First value</param>
        /// <param name="second">Second value</param>
        /// <returns>The bitwise "or" of the two values</returns>
        /// <exception cref="TypeArgumentException"><typeparamref name="T"/> is not a flags enum.</exception>
        public static T Or<T>(this T first, T second) where T : struct, IEnumConstraint
        {
            ThrowIfNotFlags<T>();
            return EnumInternals<T>.Or(first, second);
        }

        /// <summary>
        /// Returns all the bits used in any flag values
        /// </summary>
        /// internal static
        /// <returns>A flag value with all the bits set that are ever set in any defined value</returns>
        /// <exception cref="TypeArgumentException"><typeparamref name="T"/> is not a flags enum.</exception>
        public static T GetUsedBits<T>() where T : struct, IEnumConstraint
        {
            ThrowIfNotFlags<T>();
            return EnumInternals<T>.UsedBits;
        }

        /// <summary>
        /// Returns the inverse of a value, with no consideration for which bits are used
        /// by values within the enum (i.e. a simple bitwise negation).
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="value">Value to invert</param>
        /// <returns>The bitwise negation of the value</returns>
        /// <exception cref="TypeArgumentException"><typeparamref name="T"/> is not a flags enum.</exception>
        public static T AllBitsInverse<T>(this T value) where T : struct, IEnumConstraint
        {
            ThrowIfNotFlags<T>();
            return EnumInternals<T>.Not(value);
        }

        /// <summary>
        /// Returns the inverse of a value, but limited to those bits which are used by
        /// values within the enum.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="value">Value to invert</param>
        /// <returns>The restricted inverse of the value</returns>
        /// <exception cref="TypeArgumentException"><typeparamref name="T"/> is not a flags enum.</exception>
        public static T UsedBitsInverse<T>(this T value) where T : struct, IEnumConstraint
        {
            ThrowIfNotFlags<T>();
            return value.AllBitsInverse().And(EnumInternals<T>.UsedBits);
        }

        /// <summary>
        /// Returns whether this value is an empty set of fields, i.e. the zero value.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="value">Value to test</param>
        /// <returns>True if the value is empty (zero); False otherwise.</returns>
        /// <exception cref="TypeArgumentException"><typeparamref name="T"/> is not a flags enum.</exception>
        public static bool IsEmpty<T>(this T value) where T : struct, IEnumConstraint
        {
            ThrowIfNotFlags<T>();
            return EnumInternals<T>.IsEmpty(value);
        }

        /// <summary>
        /// Returns whether this value has any fields set, i.e. is not zero.
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="value">Value to test</param>
        /// <returns>True if the value is non-empty (not zero); False otherwise.</returns>
        /// <exception cref="TypeArgumentException"><typeparamref name="T"/> is not a flags enum.</exception>
        public static bool IsNotEmpty<T>(this T value) where T : struct, IEnumConstraint
        {
            ThrowIfNotFlags<T>();
            return !value.IsEmpty();
        }
    }
}
