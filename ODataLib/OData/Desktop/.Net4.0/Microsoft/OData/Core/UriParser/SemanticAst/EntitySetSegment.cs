//   OData .NET Libraries
//   Copyright (c) Microsoft Corporation
//   All rights reserved. 

//   Licensed under the Apache License, Version 2.0 (the ""License""); you may not use this file except in compliance with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0 

//   THIS CODE IS PROVIDED ON AN  *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR NON-INFRINGEMENT. 

//   See the Apache Version 2.0 License for specific language governing permissions and limitations under the License.

namespace Microsoft.OData.Core.UriParser.Semantic
{
    #region Namespaces

    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.OData.Core.UriParser.TreeNodeKinds;
    using Microsoft.OData.Core.UriParser.Visitors;
    using Microsoft.OData.Edm;
    using Microsoft.OData.Edm.Library;

    #endregion Namespaces

    /// <summary>
    /// A segment representing an EntitySet in a path.
    /// </summary>
    public sealed class EntitySetSegment : ODataPathSegment
    {
        /// <summary>
        /// The entity set represented by this segment.
        /// </summary>
        private readonly IEdmEntitySet entitySet;

        /// <summary>
        /// Type of the entities in the set represented by this segment.
        /// </summary>
        private readonly IEdmType type;

        /// <summary>
        /// Build a segment representing an entity set
        /// </summary>
        /// <param name="entitySet">The entity set represented by this segment.</param>
        /// <exception cref="System.ArgumentNullException">Throws if the input entitySet is null.</exception>
        [SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "Rule only applies to ODataLib Serialization code.")]
        public EntitySetSegment(IEdmEntitySet entitySet) 
        {
            ExceptionUtils.CheckArgumentNotNull(entitySet, "entitySet");

            this.entitySet = entitySet;

            // creating a new collection type here because the type in the entity set is just the item type, there is no user-provided collection type.
            this.type = new EdmCollectionType(new EdmEntityTypeReference(this.entitySet.ElementType, false));

            this.TargetEdmEntitySet = entitySet;
            this.TargetEdmType = entitySet.ElementType;
            this.TargetKind = RequestTargetKind.Resource;
            this.SingleResult = false;
        }

        /// <summary>
        /// Gets the entity set represented by this segment.
        /// </summary>
        public IEdmEntitySet EntitySet
        {
            get { return this.entitySet; }
        }

        /// <summary>
        /// Gets the <see cref="IEdmType"/> of this <see cref="EntitySetSegment"/>. 
        /// This will always be an <see cref="IEdmCollectionType"/> for the <see cref="IEdmEntityType"/> that this set contains.
        /// </summary>
        [SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "Rule only applies to ODataLib Serialization code.")]
        public override IEdmType EdmType
        {
            get { return this.type; }
        }

        /// <summary>
        /// Translate an <see cref="EntitySetSegment"/> into another type using an instance of <see cref="PathSegmentTranslator{T}"/>.
        /// </summary>
        /// <typeparam name="T">Type that the translator will return after visiting this token.</typeparam>
        /// <param name="translator">An implementation of the translator interface.</param>
        /// <returns>An object whose type is determined by the type parameter of the translator.</returns>
        /// <exception cref="System.ArgumentNullException">Throws if the input translator is null.</exception>
        public override T Translate<T>(PathSegmentTranslator<T> translator)
        {
            ExceptionUtils.CheckArgumentNotNull(translator, "translator");
            return translator.Translate(this);
        }

        /// <summary>
        /// Handle an <see cref="EntitySetSegment"/> using the an instance of the <see cref="PathSegmentHandler"/>.
        /// </summary>
        /// <param name="handler">An implementation of the handler interface.</param>
        /// <exception cref="System.ArgumentNullException">Throws if the input handler is null.</exception>
        public override void Handle(PathSegmentHandler handler)
        {
            ExceptionUtils.CheckArgumentNotNull(handler, "handler");
            handler.Handle(this);
        }

        /// <summary>
        /// Check if this segment is equal to another segment.
        /// </summary>
        /// <param name="other">the other segment to check.</param>
        /// <returns>true if the other segment is equal.</returns>
        /// <exception cref="System.ArgumentNullException">Throws if the input other is null.</exception>
        internal override bool Equals(ODataPathSegment other)
        {
            DebugUtils.CheckNoExternalCallers();
            ExceptionUtils.CheckArgumentNotNull(other, "other");
            EntitySetSegment otherEntitySet = other as EntitySetSegment;
            return otherEntitySet != null && otherEntitySet.EntitySet == this.EntitySet;
        }
    }
}
