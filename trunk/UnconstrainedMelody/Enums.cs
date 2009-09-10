using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace UnconstrainedMelody
{
    public static class Enums
    {
        public static T[] GetValuesArray<T>() where T : struct, IEnumConstraint
        { 
            return (T[]) Enum.GetValues(typeof(T)); 
        }

        public static IList<T> GetValues<T>() where T : struct, IEnumConstraint
        {
            return PerTypeFields<T>.Values;
        }

        private static class PerTypeFields<T> where T : struct, IEnumConstraint
        {
            static internal readonly IList<T> Values = new ReadOnlyCollection<T>((T[]) Enum.GetValues(typeof(T)));
        }
    }
}
