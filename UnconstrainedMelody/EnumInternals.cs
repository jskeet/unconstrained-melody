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
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace UnconstrainedMelody
{
    /// <summary>
    /// Shared constants used by Flags and Enums.
    /// </summary>
    internal static class EnumInternals<T> where T : struct, IEnumConstraint
    {
        internal static readonly bool IsFlags;
        internal static readonly Func<T, T, T> Or;
        internal static readonly Func<T, T, T> And;
        internal static readonly Func<T, T> Not;
        internal static readonly T UsedBits;
        internal static readonly T AllBits;
        internal static readonly T UnusedBits;
        internal static Func<T, T, bool> Equality;
        internal static readonly Func<T, bool> IsEmpty;
        internal static readonly IList<T> Values;
        internal static readonly IList<string> Names;
        internal static readonly Type UnderlyingType;
        internal static readonly Dictionary<T, string> ValueToDescriptionMap;
        internal static readonly Dictionary<string, T> DescriptionToValueMap;

        static EnumInternals()
        {
            Values = new ReadOnlyCollection<T>((T[]) Enum.GetValues(typeof(T)));
            Names = new ReadOnlyCollection<string>(Enum.GetNames(typeof(T)));
            ValueToDescriptionMap = new Dictionary<T, string>();
            DescriptionToValueMap = new Dictionary<string, T>();
            foreach (T value in Values)
            {
                string description = GetDescription(value);
                ValueToDescriptionMap[value] = description;
                if (description != null && !DescriptionToValueMap.ContainsKey(description))
                {
                    DescriptionToValueMap[description] = value;
                }
            }
            UnderlyingType = Enum.GetUnderlyingType(typeof(T));
            IsFlags = typeof(T).IsDefined(typeof(FlagsAttribute), false);
            // Parameters for various expression trees
            ParameterExpression param1 = Expression.Parameter(typeof(T), "x");
            ParameterExpression param2 = Expression.Parameter(typeof(T), "y");
            Expression convertedParam1 = Expression.Convert(param1, UnderlyingType);
            Expression convertedParam2 = Expression.Convert(param2, UnderlyingType);
            Equality = Expression.Lambda<Func<T, T, bool>>(Expression.Equal(convertedParam1, convertedParam2), param1, param2).Compile();
            Or = Expression.Lambda<Func<T, T, T>>(Expression.Convert(Expression.Or(convertedParam1, convertedParam2), typeof(T)), param1, param2).Compile();
            And = Expression.Lambda<Func<T, T, T>>(Expression.Convert(Expression.And(convertedParam1, convertedParam2), typeof(T)), param1, param2).Compile();
            Not = Expression.Lambda<Func<T, T>>(Expression.Convert(Expression.Not(convertedParam1), typeof(T)), param1).Compile();
            IsEmpty = Expression.Lambda<Func<T, bool>>(Expression.Equal(convertedParam1,
                Expression.Constant(Activator.CreateInstance(UnderlyingType))), param1).Compile();

            UsedBits = default(T);
            foreach (T value in Enums.GetValues<T>())
            {
                UsedBits = Or(UsedBits, value);
            }
            AllBits = Not(default(T));
            UnusedBits = And(AllBits, (Not(UsedBits)));
        }

        private static string GetDescription(T value)
        {
            FieldInfo field = typeof(T).GetField(value.ToString());
            return field.GetCustomAttributes(typeof(DescriptionAttribute), false)
                        .Cast<DescriptionAttribute>()
                        .Select(x => x.Description)
                        .FirstOrDefault();
        }
    }
}
