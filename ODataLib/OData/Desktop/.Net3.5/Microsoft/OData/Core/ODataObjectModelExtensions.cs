//   OData .NET Libraries
//   Copyright (c) Microsoft Corporation
//   All rights reserved. 

//   Licensed under the Apache License, Version 2.0 (the ""License""); you may not use this file except in compliance with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0 

//   THIS CODE IS PROVIDED ON AN  *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR NON-INFRINGEMENT. 

//   See the Apache Version 2.0 License for specific language governing permissions and limitations under the License.

namespace Microsoft.OData.Core
{
    /// <summary>
    /// Extension methods on the OData object model.
    /// </summary>
    public static class ODataObjectModelExtensions
    {
        /// <summary>
        /// Provide additional serialization information to the <see cref="ODataWriter"/> for <paramref name="feed"/>.
        /// </summary>
        /// <param name="feed">The instance to set the serialization info.</param>
        /// <param name="serializationInfo">The serialization info to set.</param>
        public static void SetSerializationInfo(this ODataFeed feed, ODataFeedAndEntrySerializationInfo serializationInfo)
        {
            ExceptionUtils.CheckArgumentNotNull(feed, "feed");
            feed.SerializationInfo = serializationInfo;
        }

        /// <summary>
        /// Provide additional serialization information to the <see cref="ODataWriter"/> for <paramref name="entry"/>.
        /// </summary>
        /// <param name="entry">The instance to set the serialization info.</param>
        /// <param name="serializationInfo">The serialization info to set.</param>
        public static void SetSerializationInfo(this ODataEntry entry, ODataFeedAndEntrySerializationInfo serializationInfo)
        {
            ExceptionUtils.CheckArgumentNotNull(entry, "entry");
            entry.SerializationInfo = serializationInfo;
        }

        /// <summary>
        /// Provide additional serialization information to the <see cref="ODataWriter"/> for <paramref name="property"/>.
        /// </summary>
        /// <param name="property">The instance to set the serialization info.</param>
        /// <param name="serializationInfo">The serialization info to set.</param>
        public static void SetSerializationInfo(this ODataProperty property, ODataPropertySerializationInfo serializationInfo)
        {
            ExceptionUtils.CheckArgumentNotNull(property, "property");
            property.SerializationInfo = serializationInfo;
        }

        /// <summary>
        /// Provide additional serialization information to the <see cref="ODataCollectionWriter"/> for <paramref name="collectionStart"/>.
        /// </summary>
        /// <param name="collectionStart">The instance to set the serialization info.</param>
        /// <param name="serializationInfo">The serialization info to set.</param>
        public static void SetSerializationInfo(this ODataCollectionStart collectionStart, ODataCollectionStartSerializationInfo serializationInfo)
        {
            ExceptionUtils.CheckArgumentNotNull(collectionStart, "collectionStart");
            collectionStart.SerializationInfo = serializationInfo;
        }

        /// <summary>
        /// Provide additional serialization information to the <see cref="ODataMessageWriter"/> for <paramref name="entityReferenceLink"/>.
        /// </summary>
        /// <param name="entityReferenceLink">The instance to set the serialization info.</param>
        /// <param name="serializationInfo">The serialization info to set.</param>
        public static void SetSerializationInfo(this ODataEntityReferenceLink entityReferenceLink, ODataEntityReferenceLinkSerializationInfo serializationInfo)
        {
            ExceptionUtils.CheckArgumentNotNull(entityReferenceLink, "entityReferenceLink");
            entityReferenceLink.SerializationInfo = serializationInfo;
        }

        /// <summary>
        /// Provide additional serialization information to the <see cref="ODataMessageWriter"/> for <paramref name="entityReferenceLinks"/>.
        /// </summary>
        /// <param name="entityReferenceLinks">The instance to set the serialization info.</param>
        /// <param name="serializationInfo">The serialization info to set.</param>
        public static void SetSerializationInfo(this ODataEntityReferenceLinks entityReferenceLinks, ODataEntityReferenceLinksSerializationInfo serializationInfo)
        {
            ExceptionUtils.CheckArgumentNotNull(entityReferenceLinks, "entityReferenceLinks");
            entityReferenceLinks.SerializationInfo = serializationInfo;
        }
    }
}
