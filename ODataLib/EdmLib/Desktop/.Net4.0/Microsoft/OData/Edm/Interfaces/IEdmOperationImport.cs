//   OData .NET Libraries
//   Copyright (c) Microsoft Corporation
//   All rights reserved. 

//   Licensed under the Apache License, Version 2.0 (the ""License""); you may not use this file except in compliance with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0 

//   THIS CODE IS PROVIDED ON AN  *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR NON-INFRINGEMENT. 

//   See the Apache Version 2.0 License for specific language governing permissions and limitations under the License.

using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm
{
    /// <summary>
    /// Represents an EDM operation import.
    /// </summary>
    public interface IEdmOperationImport : IEdmFunctionBase, IEdmEntityContainerElement
    {
        /// <summary>
        /// Gets a value indicating whether this operation import has side-effects.
        /// <see cref="IsSideEffecting"/> cannot be set to true if <see cref="IsComposable"/> is set to true.
        /// </summary>
        bool IsSideEffecting { get; }

        /// <summary>
        /// Gets a value indicating whether this functon import can be composed inside expressions.
        /// <see cref="IsComposable"/> cannot be set to true if <see cref="IsSideEffecting"/> is set to true.
        /// </summary>
        bool IsComposable { get; }

        /// <summary>
        /// Gets a value indicating whether this operation import can be used as an extension method for the type of the first parameter of this operation import.
        /// </summary>
        bool IsBindable { get; }

        /// <summary>
        /// Gets the entity set containing entities returned by this operation import.
        /// </summary>
        IEdmExpression EntitySet { get; }
    }
}
