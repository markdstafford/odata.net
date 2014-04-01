//   OData .NET Libraries
//   Copyright (c) Microsoft Corporation
//   All rights reserved. 

//   Licensed under the Apache License, Version 2.0 (the ""License""); you may not use this file except in compliance with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0 

//   THIS CODE IS PROVIDED ON AN  *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR NON-INFRINGEMENT. 

//   See the Apache Version 2.0 License for specific language governing permissions and limitations under the License.

namespace Microsoft.OData.Edm
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    using Microsoft.OData.Edm.Library.Values;

    /// <summary>
    /// Enum helper
    /// </summary>
    public static class EnumHelper
    {
        private static readonly IDictionary<IEdmEnumType, HashEntry> fieldInfoHash = new Dictionary<IEdmEnumType, HashEntry>();
        private const int MaxHashElements = 100;

        /// <summary>
        /// Parse enum to integer
        /// </summary>
        /// <param name="enumType">edm enum type</param>
        /// <param name="value">input string value</param>
        /// <param name="ignoreCase">true if case insensitive, false if case sensitive</param>
        /// <param name="parseResult">parse result</param>
        /// <returns>true if parse succeeds, false if parse fails</returns>
        public static bool TryParseEnum(this IEdmEnumType enumType, string value, bool ignoreCase, ref long parseResult)
        {
            char[] enumSeperatorCharArray = new[] { ',' };

            string[] enumNames;
            ulong[] enumValues;
            IEdmEnumType type = enumType;

            if (value == null)
            {
                return false;
            }

            value = value.Trim();
            if (value.Length == 0)
            {
                return false;
            }

            ulong num = 0L;
            if ((char.IsDigit(value[0]) || (value[0] == '-')) || (value[0] == '+'))
            {
                Type underlyingType = typeof(long);
                try
                {
                    object obj = Convert.ChangeType(value, underlyingType, CultureInfo.InvariantCulture);
                    parseResult = (long)obj;
                    return true;
                }
                catch (FormatException)
                {
                    // will then parse as string
                }
                catch (Exception)
                {
                    return false;
                }
            }

            string[] values = value.Split(enumSeperatorCharArray);
            type.GetCachedValuesAndNames(out enumValues, out enumNames, true, true);
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = values[i].Trim();
                bool flag = false;
                for (int j = 0; j < enumNames.Length; j++)
                {
                    if (ignoreCase)
                    {
                        if (string.Compare(enumNames[j], values[i], StringComparison.OrdinalIgnoreCase) != 0)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (!enumNames[j].Equals(values[i]))
                        {
                            continue;
                        }
                    }

                    ulong item = enumValues[j];
                    num |= item;
                    flag = true;
                    break;
                }

                if (!flag)
                {
                    return false;
                }
            }

            try
            {
                parseResult = (long)num;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Convert enum int value to string
        /// </summary>
        /// <param name="type">edm enum type reference</param>
        /// <param name="value">input int value</param>
        /// <returns>string literal of the enum value</returns>
        public static string ToStringLiteral(this IEdmEnumTypeReference type, Int64 value)
        {
            if (type != null)
            {
                // parse the value to string literal
                IEdmEnumType enumType = type.Definition as IEdmEnumType;
                if (enumType != null)
                {
                    return enumType.IsFlags ? enumType.ToStringWithFlags(value) : enumType.ToStringNoFlags(value);
                }
            }

            return value.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// For enum with flags, use a sequential search for bit masks, and then check if any residual
        /// </summary>
        /// <param name="enumType">edm enum type</param>
        /// <param name="value">input integer value</param>
        /// <returns>string seperated by comma</returns>
        private static string ToStringWithFlags(this IEdmEnumType enumType, Int64 value)
        {
            string[] strArray;
            ulong[] numArray;
            ulong num = (ulong)value;
            enumType.GetCachedValuesAndNames(out numArray, out strArray, true, true);
            int index = numArray.Length - 1;
            StringBuilder builder = new StringBuilder();
            bool flag = true;
            ulong num3 = num;
            const int Zero = 0;
            const ulong UlongZero = 0L;

            while (index >= Zero)
            {
                if ((index == Zero) && (numArray[index] == UlongZero))
                {
                    break;
                }

                if ((num & numArray[index]) == numArray[index])
                {
                    num -= numArray[index];
                    if (!flag)
                    {
                        builder.Insert(Zero, ", ");
                    }

                    builder.Insert(Zero, strArray[index]);
                    flag = false;
                }

                index--;
            }

            if (num != UlongZero)
            {
                return value.ToString(CultureInfo.InvariantCulture);
            }

            if (num3 != UlongZero)
            {
                return builder.ToString();
            }

            if ((numArray.Length > Zero) && (numArray[Zero] == UlongZero))
            {
                return strArray[Zero];
            }

            return Zero.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// For enum without flags, use a binary search
        /// </summary>
        /// <param name="enumType">edm enum type</param>
        /// <param name="value">input integer value</param>
        /// <returns>string</returns>
        private static string ToStringNoFlags(this IEdmEnumType enumType, Int64 value)
        {
            ulong[] values;
            string[] names;
            enumType.GetCachedValuesAndNames(out values, out names, true, true);
            ulong num = (ulong)value;
            int index = Array.BinarySearch(values, num);
            return index >= 0 ? names[index] : value.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Get cached values and names from hash table
        /// </summary>
        /// <param name="enumType">edm enum type</param>
        /// <param name="values">output values</param>
        /// <param name="names">output names</param>
        /// <param name="getValues">true if get values, false otherwise</param>
        /// <param name="getNames">true if get names, false otherwise</param>
        private static void GetCachedValuesAndNames(this IEdmEnumType enumType, out ulong[] values, out string[] names, bool getValues, bool getNames)
        {
            HashEntry hashEntry = GetHashEntry(enumType);
            values = hashEntry.Values;
            if (values != null)
            {
                getValues = false;
            }

            names = hashEntry.Names;
            if (names != null)
            {
                getNames = false;
            }

            if (!getValues && !getNames)
            {
                return;
            }

            GetEnumValuesAndNames(enumType, ref values, ref names, getValues, getNames);
            if (getValues)
            {
                hashEntry.Values = values;
            }

            if (getNames)
            {
                hashEntry.Names = names;
            }
        }

        private static void GetEnumValuesAndNames(IEdmEnumType enumType, ref ulong[] values, ref string[] names, bool getValues, bool getNames)
        {
            Dictionary<string, ulong> dict = new Dictionary<string, ulong>();
            foreach (var member in enumType.Members)
            {
                EdmIntegerConstant intValue = member.Value as EdmIntegerConstant;
                if (intValue != null)
                {
                    dict.Add(member.Name, (ulong)intValue.Value);
                }
            }

            Dictionary<string, ulong> sortedDict = dict.OrderBy(d => d.Value).ToDictionary(d => d.Key, d => d.Value);
            values = sortedDict.Select(d => d.Value).ToArray();
            names = sortedDict.Select(d => d.Key).ToArray();
        }

        private static HashEntry GetHashEntry(IEdmEnumType enumType)
        {
            HashEntry entry;
            fieldInfoHash.TryGetValue(enumType, out entry);
            if (entry == null)
            {
                if (fieldInfoHash.Count > MaxHashElements)
                {
                    fieldInfoHash.Clear();
                }

                entry = new HashEntry(null, null);
                fieldInfoHash.Add(enumType, entry);
            }

            return entry;
        }

        private class HashEntry
        {
            public string[] Names;
            public ulong[] Values;

            public HashEntry(string[] names, ulong[] values)
            {
                this.Names = names;
                this.Values = values;
            }
        }
    }
}
