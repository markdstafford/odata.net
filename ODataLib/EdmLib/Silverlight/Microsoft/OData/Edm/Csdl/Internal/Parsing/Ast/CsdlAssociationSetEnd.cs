//   OData .NET Libraries
//   Copyright (c) Microsoft Corporation
//   All rights reserved. 

//   Licensed under the Apache License, Version 2.0 (the ""License""); you may not use this file except in compliance with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0 

//   THIS CODE IS PROVIDED ON AN  *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR NON-INFRINGEMENT. 

//   See the Apache Version 2.0 License for specific language governing permissions and limitations under the License.

namespace Microsoft.OData.Edm.Csdl.Internal.Parsing.Ast
{
    /// <summary>
    /// Represents a CSDL association set end.
    /// </summary>
    internal class CsdlAssociationSetEnd : CsdlElementWithDocumentation
    {
        private readonly string role;
        private readonly string entitySet;

        public CsdlAssociationSetEnd(string role, string entitySet, CsdlDocumentation documentation, CsdlLocation location)
            : base(documentation, location)
        {
            this.role = role;
            this.entitySet = entitySet;
        }

        public string Role
        {
            get { return this.role; }
        }

        public string EntitySet
        {
            get { return this.entitySet; }
        }
    }
}
