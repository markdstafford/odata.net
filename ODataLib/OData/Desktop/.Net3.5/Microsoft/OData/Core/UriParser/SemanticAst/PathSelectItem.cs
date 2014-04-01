//   OData .NET Libraries
//   Copyright (c) Microsoft Corporation
//   All rights reserved. 

//   Licensed under the Apache License, Version 2.0 (the ""License""); you may not use this file except in compliance with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0 

//   THIS CODE IS PROVIDED ON AN  *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR NON-INFRINGEMENT. 

//   See the Apache Version 2.0 License for specific language governing permissions and limitations under the License.

namespace Microsoft.OData.Core.UriParser.Semantic
{
    /// <summary>
    /// Class to represent the selection of a specific path.
    /// </summary>
    public sealed class PathSelectItem : SelectItem
    {
        /// <summary>
        /// The selected path.
        /// </summary>
        private readonly ODataSelectPath selectedPath;

        /// <summary>
        /// Constructs a <see cref="SelectItem"/> to indicate that a specific path is selected.
        /// </summary>
        /// <param name="selectedPath">The selected path.</param>
        /// <exception cref="System.ArgumentNullException">Throws if the input selectedPath is null.</exception>
        public PathSelectItem(ODataSelectPath selectedPath)
        {
            ExceptionUtils.CheckArgumentNotNull(selectedPath, "selectedPath");
            this.selectedPath = selectedPath;
        }

        /// <summary>
        /// Gets the selected path.
        /// </summary>
        public ODataSelectPath SelectedPath
        {
            get { return this.selectedPath; }
        }
    }
}
