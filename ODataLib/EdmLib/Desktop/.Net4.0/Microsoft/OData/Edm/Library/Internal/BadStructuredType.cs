//   OData .NET Libraries
//   Copyright (c) Microsoft Corporation
//   All rights reserved. 

//   Licensed under the Apache License, Version 2.0 (the ""License""); you may not use this file except in compliance with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0 

//   THIS CODE IS PROVIDED ON AN  *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR NON-INFRINGEMENT. 

//   See the Apache Version 2.0 License for specific language governing permissions and limitations under the License.

using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Library.Internal
{
    /// <summary>
    /// Represents a semantically invalid EDM structured type definition.
    /// </summary>
    internal abstract class BadStructuredType : BadType, IEdmStructuredType, IEdmCheckable
    {
        protected BadStructuredType(IEnumerable<EdmError> errors)
            : base(errors)
        {
        }

        public IEdmStructuredType BaseType
        {
            get { return null; }
        }

        public IEnumerable<IEdmProperty> DeclaredProperties
        {
            get { return Enumerable.Empty<IEdmProperty>(); }
        }

        public bool IsAbstract
        {
            get { return false; }
        }

        public bool IsOpen
        {
            get { return false; }
        }

        public IEdmProperty FindProperty(string name)
        {
            return null;
        }
    }
}
