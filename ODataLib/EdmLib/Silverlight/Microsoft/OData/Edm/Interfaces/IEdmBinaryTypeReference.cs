//   OData .NET Libraries
//   Copyright (c) Microsoft Corporation
//   All rights reserved. 

//   Licensed under the Apache License, Version 2.0 (the ""License""); you may not use this file except in compliance with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0 

//   THIS CODE IS PROVIDED ON AN  *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR NON-INFRINGEMENT. 

//   See the Apache Version 2.0 License for specific language governing permissions and limitations under the License.

namespace Microsoft.OData.Edm
{
    /// <summary>
    /// Represents a reference to an EDM binary type.
    /// </summary>
    public interface IEdmBinaryTypeReference : IEdmPrimitiveTypeReference
    {
        /// <summary>
        /// Gets a value indicating whether this type specifies fixed length.
        /// </summary>
        bool? IsFixedLength { get; }

        /// <summary>
        /// Gets a value indicating whether this type specifies the maximum allowed length.
        /// </summary>
        bool IsUnbounded { get; }

        /// <summary>
        /// Gets the maximum length of this type.
        /// </summary>
        int? MaxLength { get; }
    }
}
