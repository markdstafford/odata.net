//   OData .NET Libraries
//   Copyright (c) Microsoft Corporation
//   All rights reserved. 

//   Licensed under the Apache License, Version 2.0 (the ""License""); you may not use this file except in compliance with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0 

//   THIS CODE IS PROVIDED ON AN  *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR NON-INFRINGEMENT. 

//   See the Apache Version 2.0 License for specific language governing permissions and limitations under the License.

using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Library.Expressions
{
    /// <summary>
    /// Represents an EDM labeled expression.
    /// </summary>
    public class EdmLabeledExpression : EdmElement, IEdmLabeledExpression
    {
        private readonly string name;
        private readonly IEdmExpression expression;

        /// <summary>
        /// Initializes a new instance of the <see cref="EdmLabeledExpression"/> class.
        /// </summary>
        /// <param name="name">Label of the expression.</param>
        /// <param name="expression">Underlying expression.</param>
        public EdmLabeledExpression(string name, IEdmExpression expression)
        {
            EdmUtil.CheckArgumentNull(name, "name");
            EdmUtil.CheckArgumentNull(expression, "expression");

            this.name = name;
            this.expression = expression;
        }

        /// <summary>
        /// Gets the label.
        /// </summary>
        public string Name
        {
            get { return this.name; }
        }

        /// <summary>
        /// Gets the underlying expression.
        /// </summary>
        public IEdmExpression Expression
        {
            get { return this.expression; }
        }

        /// <summary>
        /// Gets the expression kind.
        /// </summary>
        public EdmExpressionKind ExpressionKind
        {
            get { return EdmExpressionKind.Labeled; }
        }
    }
}
