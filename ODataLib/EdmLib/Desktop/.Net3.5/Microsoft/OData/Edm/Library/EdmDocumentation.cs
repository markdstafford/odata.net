//   OData .NET Libraries
//   Copyright (c) Microsoft Corporation
//   All rights reserved. 

//   Licensed under the Apache License, Version 2.0 (the ""License""); you may not use this file except in compliance with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0 

//   THIS CODE IS PROVIDED ON AN  *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR NON-INFRINGEMENT. 

//   See the Apache Version 2.0 License for specific language governing permissions and limitations under the License.

namespace Microsoft.OData.Edm.Library
{
    /// <summary>
    /// Represents an EDM documentation.
    /// </summary>
    public class EdmDocumentation : IEdmDocumentation
    {
        private readonly string summary;
        private readonly string description;

        /// <summary>
        /// Initializes a new instance of the <see cref="EdmDocumentation"/> class.
        /// </summary>
        /// <param name="summary">Summary of the documentation.</param>
        /// <param name="description">The documentation contents.</param>
        public EdmDocumentation(string summary, string description)
        {
            this.summary = summary;
            this.description = description;
        }

        /// <summary>
        /// Gets summary of this documentation.
        /// </summary>
        public string Summary
        {
            get { return this.summary; }
        }

        /// <summary>
        /// Gets documentation.
        /// </summary>
        public string Description
        {
            get { return this.description; }
        }
    }
}
