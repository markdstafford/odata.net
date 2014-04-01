//   OData .NET Libraries
//   Copyright (c) Microsoft Corporation
//   All rights reserved. 

//   Licensed under the Apache License, Version 2.0 (the ""License""); you may not use this file except in compliance with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0 

//   THIS CODE IS PROVIDED ON AN  *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR NON-INFRINGEMENT. 

//   See the Apache Version 2.0 License for specific language governing permissions and limitations under the License.

using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

#if ASTORIA_SERVER
namespace Microsoft.OData.Service
#else
#if ASTORIA_CLIENT
namespace Microsoft.OData.Client
#else
#if SPATIAL
namespace Microsoft.Spatial
#else
#if ODATALIB || ODATALIB_QUERY
namespace Microsoft.OData.Core
#else
namespace Microsoft.OData.Edm
#endif
#endif
#endif
#endif
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
#if PORTABLELIB
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
#endif
    using System.Reflection;
    using System.Xml;

    /// <summary>
    /// Helper methods that provide a common API surface on all platforms.
    /// </summary>
    internal static class PlatformHelper
    {
        /// <summary>
        /// Use this instead of Type.EmptyTypes.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
        internal static readonly Type[] EmptyTypes = new Type[0];

#if PORTABLELIB
        /// <summary>
        /// Replacement for Uri.UriSchemeHttp, which does not exist on.
        /// </summary>
        internal static readonly string UriSchemeHttp = "http";

        /// <summary>
        /// Replacement for Uri.UriSchemeHttps, which does not exist on.
        /// </summary>
        internal static readonly string UriSchemeHttps = "https";
#else
        /// <summary>
        /// Use this instead of Uri.UriSchemeHttp.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
        internal static readonly string UriSchemeHttp = Uri.UriSchemeHttp;

        /// <summary>
        /// Use this instead of Uri.UriSchemeHttps.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
        internal static readonly string UriSchemeHttps = Uri.UriSchemeHttps;
#endif

        #region Helper methods for properties

        /// <summary>
        /// Replacement for Type.Assembly.
        /// </summary>
        /// <param name="type">Type on which to call this helper method.</param>
        /// <returns>See documentation for property being accessed in the body of the method.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
        internal static Assembly GetAssembly(this Type type)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
            return type.Assembly;
        }

        /// <summary>
        /// Replacement for Type.IsValueType.
        /// </summary>
        /// <param name="type">Type on which to call this helper method.</param>
        /// <returns>See documentation for property being accessed in the body of the method.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
        internal static bool IsValueType(this Type type)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
            return type.IsValueType;
        }

        /// <summary>
        /// Replacement for Type.IsGenericParameter.
        /// </summary>
        /// <param name="type">Type on which to call this helper method.</param>
        /// <returns>See documentation for property being accessed in the body of the method.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
        internal static bool IsGenericParameter(this Type type)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
            return type.IsGenericParameter;
        }

        /// <summary>
        /// Replacement for Type.IsAbstract.
        /// </summary>
        /// <param name="type">Type on which to call this helper method.</param>
        /// <returns>See documentation for property being accessed in the body of the method.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
        internal static bool IsAbstract(this Type type)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
            return type.IsAbstract;
        }

        /// <summary>
        /// Replacement for Type.IsGenericType.
        /// </summary>
        /// <param name="type">Type on which to call this helper method.</param>
        /// <returns>See documentation for property being accessed in the body of the method.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
        internal static bool IsGenericType(this Type type)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
            return type.IsGenericType;
        }

        /// <summary>
        /// Replacement for Type.IsGenericTypeDefinition.
        /// </summary>
        /// <param name="type">Type on which to call this helper method.</param>
        /// <returns>See documentation for property being accessed in the body of the method.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
        internal static bool IsGenericTypeDefinition(this Type type)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
            return type.IsGenericTypeDefinition;
        }

        /// <summary>
        /// Replacement for Type.IsVisible.
        /// </summary>
        /// <param name="type">Type on which to call this helper method.</param>
        /// <returns>See documentation for property being accessed in the body of the method.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
        internal static bool IsVisible(this Type type)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
            return type.IsVisible;
        }

        /// <summary>
        /// Replacement for Type.IsInterface.
        /// </summary>
        /// <param name="type">Type on which to call this helper method.</param>
        /// <returns>See documentation for property being accessed in the body of the method.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
        internal static bool IsInterface(this Type type)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
            return type.IsInterface;
        }

        /// <summary>
        /// Replacement for Type.IsClass.
        /// </summary>
        /// <param name="type">Type on which to call this helper method.</param>
        /// <returns>See documentation for property being accessed in the body of the method.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
        internal static bool IsClass(this Type type)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
            return type.IsClass;
        }

        /// <summary>
        /// Replacement for Type.IsEnum.
        /// </summary>
        /// <param name="type">Type on which to call this helper method.</param>
        /// <returns>See documentation for property being accessed in the body of the method.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
        internal static bool IsEnum(this Type type)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
            return type.IsEnum;
        }

        /// <summary>
        /// Replacement for Type.BaseType.
        /// </summary>
        /// <param name="type">Type on which to call this helper method.</param>
        /// <returns>See documentation for property being accessed in the body of the method.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
        internal static Type GetBaseType(this Type type)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
            return type.BaseType;
        }

        /// <summary>
        /// Replacement for Type.ContainsGenericParameters.
        /// </summary>
        /// <param name="type">Type on which to call this helper method.</param>
        /// <returns>See documentation for property being accessed in the body of the method.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
        internal static bool ContainsGenericParameters(this Type type)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
            return type.ContainsGenericParameters;
        }

        #endregion

        #region Helper methods for static methods

        /// <summary>
        /// Replacement for Array.AsReadOnly(T[]).
        /// </summary>
        /// <typeparam name="T">Type of items in the array.</typeparam>
        /// <param name="array">Array to use to create the ReadOnlyCollection.</param>
        /// <returns>ReadOnlyCollection containing the specified array items.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
        internal static ReadOnlyCollection<T> AsReadOnly<T>(this T[] array)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
#if PORTABLELIB
            return new ReadOnlyCollection<T>(array);
#else
            return Array.AsReadOnly(array);
#endif
        }

        /// <summary>
        /// Converts a string to a DateTime.
        /// </summary>
        /// <param name="text">String to be converted.</param>
        /// <returns>See documentation for method being accessed in the body of the method.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
        internal static DateTime ConvertStringToDateTime(string text)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif

            // Workaround for XmlConvert.ToDateTime issue which is not able to convert string without seconds to DateTime, So the seconds part is added as zeros.
            text = AddSecondsPaddingIfMissing(text);
#if PORTABLELIB
            // DateTimeOffset always applies an offset (using the local one if one is not present in the input), but the old XmlConvert methods
            // would produce a DateTime value with InternalKind=DateTimeKind.Unspecified and no offset applied if none was specified in the input string.
            // Before we convert to DateTimeOffset, we need to determine what kind of input we have so we can still produce the same kind of DateTime
            // instances that we would have gotten on other platforms with XmlConvert.ToDateTime.
            // 
            // The XML DateTime pattern is described here: http://www.w3.org/TR/xmlschema-2/#dateTime
            // For example:
            //      No timezone specified: "2012-12-21T15:01:23.1234567"
            //      UTC timezone: "2012-12-21T15:01:23.1234567Z"
            //      Timezone offset from UTC: "2012-12-21T15:01:23.1234567-08:00" or "2012-12-21T15:01:23.1234567+08:00"
            // If timezone is specified, the indicator will always be at the same place from the end of the string as in the examples above, so we can look there for the Z or +/-.
            DateTimeKind inputKind;
            const int TimeZoneSignOffset = 6;
            if (text[text.Length - 1] == 'Z')
            {
                inputKind = DateTimeKind.Utc;
            }
            else if (text[text.Length - TimeZoneSignOffset] == '-' || text[text.Length - TimeZoneSignOffset] == '+')
            {
                inputKind = DateTimeKind.Local;
            }
            else
            {
                // To prevent ToDateTimeOffset from applying the local offset in this case, we will append the Z to indicate UTC time
                inputKind = DateTimeKind.Unspecified;
                text = text + "Z";
            }

            var dateTimeOffset = XmlConvert.ToDateTimeOffset(text);
            switch (inputKind)
            {
                case DateTimeKind.Local:
                    return dateTimeOffset.LocalDateTime;
                case DateTimeKind.Utc:
                    return dateTimeOffset.UtcDateTime;
                default:
                    Debug.Assert(inputKind == DateTimeKind.Unspecified, "All dates must be Utc, Local, or Unspecified.");
                    return dateTimeOffset.DateTime;
            }
#else
            return XmlConvert.ToDateTime(text, XmlDateTimeSerializationMode.RoundtripKind);
#endif
        }

        /// <summary>
        /// Converts a string to a DateTimeOffset.
        /// </summary>
        /// <param name="text">String to be converted.</param>
        /// <returns>See documentation for method being accessed in the body of the method.</returns>
        internal static DateTimeOffset ConvertStringToDateTimeOffset(string text)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
            text = AddSecondsPaddingIfMissing(text);
            return XmlConvert.ToDateTimeOffset(text);
        }

        /// <summary>
        /// Adds the seconds padding as zeros to the date time string if seconds part is missing.
        /// </summary>
        /// <param name="text">String that needs seconds padding</param>
        /// <returns>DateTime string after adding seconds padding</returns>
        internal static string AddSecondsPaddingIfMissing(string text)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
            int indexOfT = text.IndexOf("T", System.StringComparison.Ordinal);
            const int ColonBeforeSecondsOffset = 6;
            int indexOfColonBeforeSeconds = indexOfT + ColonBeforeSecondsOffset;

            // check if the string is in the format of yyyy-mm-ddThh:mm or in the format of yyyy-mm-ddThh:mm[- or +]hh:mm 
            if (indexOfT > 0 && (text.Length <= indexOfColonBeforeSeconds || text[indexOfColonBeforeSeconds] != ':'))
            {
                text = text.Insert(indexOfColonBeforeSeconds, ":00");
            }
            
            return text;
        }

        /// <summary>
        /// Converts the DateTime to a string, internal method.
        /// </summary>
        /// <param name="dateTime">DateTime to convert to String.</param>
        /// <returns>Converted String.</returns>
        internal static string ConvertDateTimeToStringInternal(DateTime dateTime)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
            if (dateTime.Kind == DateTimeKind.Unspecified)
            {
                // If we just cast to DateTimeOffset here, the local offset will be applied, which can alter the meaning of the value.
                // Instead we need to create a new DateTimeOffset with the timezone explicitly set to UTC, which will prevent
                // any offset from being used. The resulting string does have the Z on it in that case, but we want to leave the timezone
                // unspecified here, so we will just remove that.
                DateTimeOffset dateTimeOffset = new DateTimeOffset(dateTime, TimeSpan.Zero);
                string outputWithZ = XmlConvert.ToString(dateTimeOffset);
                Debug.Assert(outputWithZ[outputWithZ.Length - 1] == 'Z', "Expected DateTimeOffset to be a UTC value.");
                return outputWithZ.TrimEnd('Z');
            }
            else
            {
                // For Utc and Local kinds, ToString produces the same string that the old XmlConvert methods would produce.
                return XmlConvert.ToString((DateTimeOffset)dateTime);
            }    
        }

        /// <summary>
        /// Converts a DateTime to a string.
        /// </summary>
        /// <param name="dateTime">DateTime to be converted.</param>
        /// <returns>See documentation for property being accessed in the body of the method.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
        internal static string ConvertDateTimeToString(DateTime dateTime)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
#if PORTABLELIB
            return ConvertDateTimeToStringInternal(dateTime);
#else
            return XmlConvert.ToString(dateTime, XmlDateTimeSerializationMode.RoundtripKind);
#endif
        }

        /// <summary>
        /// Gets the specified type.
        /// </summary>
        /// <param name="typeName">Name of the type to get.</param>
        /// <exception cref="TypeLoadException">Throws if the type could not be found.</exception>
        /// <returns>Type instance that represents the specified type name.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
        internal static Type GetTypeOrThrow(string typeName)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
            return Type.GetType(typeName, true);
        }

        /// <summary>
        /// Gets the TypeCode for the specified type.
        /// </summary>
        /// <param name="type">Type on which to call this helper method.</param>
        /// <returns>TypeCode representing the specified type.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
        internal static TypeCode GetTypeCode(Type type)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
            return Type.GetTypeCode(type);
        }

        #endregion

        #region Methods to replace other changed functionality where the replacement doesn't map exactly to an existing method on other platforms

        /// <summary>
        /// Gets the Unicode Category of the specified character.
        /// </summary>
        /// <param name="c">Character to get category of.</param>
        /// <returns>Category of the character.</returns>
        internal static UnicodeCategory GetUnicodeCategory(Char c)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
            // Portable Library platform doesn't have Char.GetUnicodeCategory, its on CharUnicodeInfo instead.
#if PORTABLELIB
            return CharUnicodeInfo.GetUnicodeCategory(c);
#else
            return Char.GetUnicodeCategory(c);
#endif
        }

        /// <summary>
        /// Replacement for usage of MemberInfo.MemberType property.
        /// </summary>
        /// <param name="member">MemberInfo on which to access this method.</param>
        /// <returns>True if the specified member is a property, otherwise false.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
        internal static bool IsProperty(MemberInfo member)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
#if PORTABLELIB
            return member is PropertyInfo;
#else
            return member.MemberType == MemberTypes.Property;
#endif
        }

        /// <summary>
        /// Replacement for usage of Type.IsPrimitive property.
        /// </summary>
        /// <param name="type">Type on which to access this method.</param>
        /// <returns>True if the specified type is primitive, otherwise false.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
        internal static bool IsPrimitive(this Type type)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
            return type.IsPrimitive;
        }

        /// <summary>
        /// Replacement for usage of Type.IsSealed property.
        /// </summary>
        /// <param name="type">Type on which to access this method.</param>
        /// <returns>True if the specified type is sealed, otherwise false.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
        internal static bool IsSealed(this Type type)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
            return type.IsSealed;
        }

        /// <summary>
        /// Replacement for usage of MemberInfo.MemberType property.
        /// </summary>
        /// <param name="member">MemberInfo on which to access this method.</param>
        /// <returns>True if the specified member is a method, otherwise false.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
        internal static bool IsMethod(MemberInfo member)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
#if PORTABLELIB
            return member is MethodInfo;
#else
            return member.MemberType == MemberTypes.Method;
#endif
        }

        /// <summary>
        /// Compares two methodInfos and returns true if they represent the same method.
        /// Need this for Windows Phone as the method Infos of the same method are not always instance equivalent.
        /// </summary>
        /// <param name="member1">MemberInfo to compare.</param>
        /// <param name="member2">MemberInfo to compare.</param>
        /// <returns>True if the specified member is a method, otherwise false.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
        internal static bool AreMembersEqual(MemberInfo member1, MemberInfo member2)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
#if PORTABLELIB
            return member1 == member2;
#else 
            return member1.MetadataToken == member2.MetadataToken; 
#endif
        }

        /// <summary>
        /// Gets public properties for the specified type.
        /// </summary>
        /// <param name="type">Type on which to call this helper method.</param>
        /// <param name="instanceOnly">True if method should return only instance properties, false if it should return both instance and static properties.</param>
        /// <returns>Enumerable of public properties for the type.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
        internal static IEnumerable<PropertyInfo> GetPublicProperties(this Type type, bool instanceOnly)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
            return GetPublicProperties(type, instanceOnly, false /*declaredOnly*/);
        }

        /// <summary>
        /// Gets public properties for the specified type.
        /// </summary>
        /// <param name="type">Type on which to call this helper method.</param>
        /// <param name="instanceOnly">True if method should return only instance properties, false if it should return both instance and static properties.</param>
        /// <param name="declaredOnly">True if method should return only properties that are declared on the type, false if it should return properties declared on the type as well as those inherited from any base types.</param>
        /// <returns>Enumerable of public properties for the type.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
        internal static IEnumerable<PropertyInfo> GetPublicProperties(this Type type, bool instanceOnly, bool declaredOnly)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;
            if (!instanceOnly)
            {
                bindingFlags |= BindingFlags.Static;
            }

            if (declaredOnly)
            {
                bindingFlags |= BindingFlags.DeclaredOnly;
            }

            return type.GetProperties(bindingFlags);
        }

        /// <summary>
        /// Gets instance constructors for the specified type.
        /// </summary>
        /// <param name="type">Type on which to call this helper method.</param>
        /// <param name="isPublic">True if method should return only public constructors, false if it should return only non-public constructors.</param>
        /// <returns>Enumerable of instance constructors for the specified type.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
        internal static IEnumerable<ConstructorInfo> GetInstanceConstructors(this Type type, bool isPublic)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
            BindingFlags bindingFlags = BindingFlags.Instance;
            bindingFlags |= isPublic ? BindingFlags.Public : BindingFlags.NonPublic;
            return type.GetConstructors(bindingFlags);
        }

        /// <summary>
        /// Gets a instance constructor for the type that takes the specified argument types.
        /// </summary>
        /// <param name="type">Type on which to call this helper method.</param>
        /// <param name="isPublic">True if method should search only public constructors, false if it should search only non-public constructors.</param>
        /// <param name="argTypes">Array of argument types for the constructor.</param>
        /// <returns>ConstructorInfo for the constructor with the specified characteristics if found, otherwise null.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
        internal static ConstructorInfo GetInstanceConstructor(this Type type, bool isPublic, Type[] argTypes)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
#if PORTABLELIB
            return GetInstanceConstructors(type, isPublic).SingleOrDefault(c => CheckTypeArgs(c, argTypes));
#endif
#if !PORTABLELIB
            BindingFlags bindingFlags = BindingFlags.Instance;
            bindingFlags |= isPublic ? BindingFlags.Public : BindingFlags.NonPublic;
            return type.GetConstructor(bindingFlags, null, argTypes, null);
#endif
        }

        /// <summary>
        /// Tries to the get method from the type, returns null if not found.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="name">The name.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <returns>Returns True if found.</returns>
        internal static bool TryGetMethod(this Type type, string name, Type[] parameterTypes, out MethodInfo foundMethod)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
            foundMethod = null;
            try
            {
                foundMethod = type.GetMethod(name, parameterTypes);
                return foundMethod != null;
            }
            catch (ArgumentNullException)
            {
                return false;
            }
        }

        /// <summary>
        /// Gets a method on the specified type.
        /// </summary>
        /// <param name="type">Type on which to call this helper method.</param>
        /// <param name="name">Name of the method on the type.</param>
        /// <param name="isPublic">True if method should search only public methods, false if it should search only non-public methods.</param>
        /// <param name="isStatic">True if method should search only static methods, false if it should search only instance methods.</param>
        /// <returns>MethodInfo for the method with the specified characteristics if found, otherwise null.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
        internal static MethodInfo GetMethod(this Type type, string name, bool isPublic, bool isStatic)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
            // PortableLib: The BindingFlags enum and all related reflection method overloads have been removed from . Instead of trying to provide
            // a general purpose flags enum and methods that can take any combination of the flags, we provide more restrictive methods that
            // still allow for the same functionality as needed by the calling code.
           
#if PORTABLELIB
            BindingFlags bindingFlags = isPublic ? BindingFlags.Public : BindingFlags.NonPublic;
            bindingFlags |= isStatic ? BindingFlags.Static : BindingFlags.Instance;
            return type.GetMethod(name, bindingFlags);
#endif
#if !PORTABLELIB
            BindingFlags bindingFlags = BindingFlags.Default;
            bindingFlags |= isPublic ? BindingFlags.Public : BindingFlags.NonPublic;
            bindingFlags |= isStatic ? BindingFlags.Static : BindingFlags.Instance;
            return type.GetMethod(name, bindingFlags);
#endif
        }

        /// <summary>
        /// Gets a method on the specified type.
        /// </summary>
        /// <param name="type">Type on which to call this helper method.</param>
        /// <param name="name">Name of the method on the type.</param>
        /// <param name="types">Argument types for the method.</param>
        /// <param name="isPublic">True if method should search only public methods, false if it should search only non-public methods.</param>
        /// <param name="isStatic">True if method should search only static methods, false if it should search only instance methods.</param>
        /// <returns>MethodInfo for the method with the specified characteristics if found, otherwise null.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
        internal static MethodInfo GetMethod(this Type type, string name, Type[] types, bool isPublic, bool isStatic)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
#if PORTABLELIB
            MethodInfo methodInfo = type.GetMethod(name, types);
            if (isPublic == methodInfo.IsPublic && isStatic == methodInfo.IsStatic)
            {
                return methodInfo;
            }

            return null;
#endif
#if !PORTABLELIB
            BindingFlags bindingFlags = BindingFlags.Default;
            bindingFlags |= isPublic ? BindingFlags.Public : BindingFlags.NonPublic;
            bindingFlags |= isStatic ? BindingFlags.Static : BindingFlags.Instance;
            return type.GetMethod(name, bindingFlags, null, types, null);
#endif
        }

        /// <summary>
        /// Gets all public static methods for a type.
        /// </summary>
        /// <param name="type">Type on which to call this helper method.</param>
        /// <returns>Enumerable of all public static methods for the specified type.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
        internal static IEnumerable<MethodInfo> GetPublicStaticMethods(this Type type)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
            return type.GetMethods(BindingFlags.Static | BindingFlags.Public);
        }

        /// <summary>
        /// Replacement for Type.GetNestedTypes(BindingFlags.NonPublic)
        /// </summary>
        /// <param name="type">Type on which to call this helper method.</param>
        /// <returns>All types nested in the current type</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Code is shared among multiple assemblies and this method should be available as a helper in case it is needed in new code.")]
        internal static IEnumerable<Type> GetNonPublicNestedTypes(this Type type)
        {
#if ODATALIB
            DebugUtils.CheckNoExternalCallers();
#endif
            return type.GetNestedTypes(BindingFlags.NonPublic);
        }
        #endregion

#if PORTABLELIB
        /// <summary>
        /// Checks if the specified constructor takes arguments of the specified types.
        /// </summary>
        /// <param name="constructorInfo">ConstructorInfo on which to call this helper method.</param>
        /// <param name="types">Array of type arguments to check against the constructor parameters.</param>
        /// <returns>True if the constructor takes arguments of the specified types, otherwise false.</returns>
        private static bool CheckTypeArgs(ConstructorInfo constructorInfo, Type[] types)
        {
            Debug.Assert(types != null, "Types should not be null, use a different overload of the calling method if you don't care about the parameter types.");

            ParameterInfo[] parameters = constructorInfo.GetParameters();
            if (parameters.Length != types.Length)
            {
                return false;
            }

            for (int i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].ParameterType != types[i])
                {
                    return false;
                }
            }

            return true;
        }
#endif

        /// <summary>
        /// Creates a Compiled Regex expression
        /// </summary>
        /// <param name="pattern">Pattern to match.</param>
        /// <param name="options">Options to use.</param>
        /// <returns>Regex expression to match supplied patter</returns>
        /// <remarks>Is marked as compiled option only in platforms otherwise RegexOption.None is used</remarks>
        public static Regex CreateCompiled(string pattern, RegexOptions options)
        {
#if SILVERLIGHT || ORCAS || PORTABLELIB
            options = options | RegexOptions.None;
#else
            options = options | RegexOptions.Compiled;
#endif
            return new Regex(pattern, options);
        }

        public static string[] GetSegments(this Uri uri)
        {
#if SILVERLIGHT || PORTABLELIB
            return uri.AbsolutePath.Split('/');
#else
            return uri.Segments;
#endif
        }
    }
}
