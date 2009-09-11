using System;

namespace UnconstrainedMelody
{
    /// <summary>
    /// Provides a set of static methods for use with "flags" enums,
    /// i.e. those decorated with <see cref="FlagsAttribute"/>.
    /// Other than <see cref="IsValidCombination{T}"/>, methods in this
    /// class throw <see cref="NotSupportedException" />.
    /// </summary>
    public static class Flags
    {
        /// <summary>
        /// Helper method used by almost all methods to make sure
        /// the type argument is really a flags enum.
        /// </summary>
        static void ThrowIfNotFlags<T>() where T : struct, IEnumConstraint
        {
            if (!PerTypeFields<T>.IsFlags)
            {
                throw new NotSupportedException("Can't call this method for a non-flags enum");
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
            return PerTypeFields<T>.IsFlags;
        }

        /// <summary>
        /// Determines whether the given value only uses bits covered
        /// by named values.
        /// </summary>
        /// <typeparam name="T">Type of enum</typeparam>
        /// <param name="values">Combination to test</param>
        public static bool IsValidCombination<T>(this T values) where T : struct, IEnumConstraint
        {
            ThrowIfNotFlags<T>();
            return true;
        }

        private static class PerTypeFields<T> where T : struct, IEnumConstraint
        {
            static internal readonly bool IsFlags;

            static PerTypeFields()
            {
                IsFlags = typeof(T).IsDefined(typeof(FlagsAttribute), false);
            }
        }

    }
}
