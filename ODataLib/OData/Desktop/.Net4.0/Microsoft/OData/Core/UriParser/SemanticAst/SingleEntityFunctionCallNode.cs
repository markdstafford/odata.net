//   OData .NET Libraries
//   Copyright (c) Microsoft Corporation
//   All rights reserved. 

//   Licensed under the Apache License, Version 2.0 (the ""License""); you may not use this file except in compliance with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0 

//   THIS CODE IS PROVIDED ON AN  *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR NON-INFRINGEMENT. 

//   See the Apache Version 2.0 License for specific language governing permissions and limitations under the License.

namespace Microsoft.OData.Core.UriParser
{
    #region Namespaces

    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Microsoft.OData.Core.UriParser.TreeNodeKinds;
    using Microsoft.OData.Core.UriParser.Visitors;
    using Microsoft.OData.Edm;
    using Microsoft.OData.Edm.Library;
    using Microsoft.OData.Core.UriParser.Semantic;
    using ODataErrorStrings = Microsoft.OData.Core.Strings;
    #endregion Namespaces

    /// <summary>
    /// Node representing a function call which returns a single entity.
    /// </summary>
    public sealed class SingleEntityFunctionCallNode : SingleEntityNode
    {
        /// <summary>
        /// the name of this function 
        /// </summary>
        private readonly string name;
        
        /// <summary>
        /// the list of operation imports represented by this node.
        /// </summary>
        private readonly ReadOnlyCollection<IEdmOperationImport> operationImports; 
        
        /// <summary>
        /// List of arguments provided to the function.
        /// </summary>
        private readonly IEnumerable<QueryNode> parameters;

        /// <summary>
        /// The return type of this function.
        /// </summary>
        private readonly IEdmEntityTypeReference returnedEntityTypeReference;

        /// <summary>
        /// The EntitySet containing the single entity that this function returns.
        /// </summary>
        private readonly IEdmEntitySet entitySet;

        /// <summary>
        /// The semantically bound parent of this function.
        /// </summary>
        private readonly QueryNode source;

        /// <summary>
        /// Create a SingleEntityFunctionCallNode
        /// </summary>
        /// <param name="name">The name of the function to call</param>
        /// <param name="parameters">List of arguments provided to the operation import. Can be null.</param>
        /// <param name="returnedEntityTypeReference">The return type of this function.</param>
        /// <param name="entitySet">The EntitySet containing the single entity that this operation import returns.</param>
        /// <exception cref="System.ArgumentNullException">Throws if the input name, returnedEntityTypeReference, or entitySet is null.</exception>
        public SingleEntityFunctionCallNode(string name, IEnumerable<QueryNode> parameters, IEdmEntityTypeReference returnedEntityTypeReference, IEdmEntitySet entitySet)
        : this(name, null, parameters, returnedEntityTypeReference, entitySet, null)
        {
        }

        /// <summary>
        /// Create a SingleEntityFunctionCallNode
        /// </summary>
        /// <param name="name">The name of the operation import to call</param>
        /// <param name="operationImports">the list of operation imports this node represents.</param>
        /// <param name="parameters">List of arguments provided to the function. Can be null.</param>
        /// <param name="returnedEntityTypeReference">The return type of this operation import.</param>
        /// <param name="entitySet">The EntitySet containing the single entity that this operation import returns.</param>
        /// <param name="source">The semantically bound parent of this operation import.</param>
        /// <exception cref="System.ArgumentNullException">Throws if the input name, returnedEntityTypeReference, or entitySet is null.</exception>
        public SingleEntityFunctionCallNode(string name, IEnumerable<IEdmOperationImport> operationImports, IEnumerable<QueryNode> parameters, IEdmEntityTypeReference returnedEntityTypeReference, IEdmEntitySet entitySet, QueryNode source)
        {
            ExceptionUtils.CheckArgumentNotNull(name, "name");
            ExceptionUtils.CheckArgumentNotNull(returnedEntityTypeReference, "returnedEntityTypeReference");

            this.name = name;
            this.operationImports = new ReadOnlyCollection<IEdmOperationImport>(operationImports != null ? operationImports.ToList() : new List<IEdmOperationImport>());
            this.parameters = parameters;
            this.returnedEntityTypeReference = returnedEntityTypeReference;
            this.entitySet = entitySet;
            this.source = source;
        }

        /// <summary>
        /// Gets the name of the function to call
        /// </summary>
        public string Name
        {
            get { return this.name; }
        }

        /// <summary>
        /// Gets the list of operation imports that this node represents
        /// </summary>
        public IEnumerable<IEdmOperationImport> FunctionImports
        {
            get { return this.operationImports; }
        }

        /// <summary>
        /// Gets the list of arguments provided to the function.
        /// </summary>
        public IEnumerable<QueryNode> Parameters
        {
            get { return this.parameters; }
        }

        /// <summary>
        /// Gets the return type of this function.
        /// </summary>
        public override IEdmTypeReference TypeReference
        {
            get { return this.returnedEntityTypeReference; }
        }

        /// <summary>
        /// Gets the EntitySet containing the single entity that this function returns.
        /// </summary>
        public override IEdmEntitySet EntitySet
        {
            get { return this.entitySet; }
        }

        /// <summary>
        /// Gets the return type of this function.
        /// </summary>
        public override IEdmEntityTypeReference EntityTypeReference
        {
            get { return this.returnedEntityTypeReference; }
        }

        /// <summary>
        /// Gets the semantically bound parent of this function.
        /// </summary>
        public QueryNode Source
        {
            get { return this.source; }
        }

        /// <summary>
        /// Gets the kind of this node.
        /// </summary>
        internal override InternalQueryNodeKind InternalKind
        {
            get
            {
                DebugUtils.CheckNoExternalCallers();
                return InternalQueryNodeKind.SingleEntityFunctionCall;
            }
        }

        /// <summary>
        /// Accept a <see cref="QueryNodeVisitor{T}"/> that walks a tree of <see cref="QueryNode"/>s.
        /// </summary>
        /// <typeparam name="T">Type that the visitor will return after visiting this token.</typeparam>
        /// <param name="visitor">An implementation of the visitor interface.</param>
        /// <returns>An object whose type is determined by the type parameter of the visitor.</returns>
        /// <exception cref="System.ArgumentNullException">Throws if the input visitor is null.</exception>
        public override T Accept<T>(QueryNodeVisitor<T> visitor)
        {
            DebugUtils.CheckNoExternalCallers();
            ExceptionUtils.CheckArgumentNotNull(visitor, "visitor");
            return visitor.Visit(this);
        }
    }
}
