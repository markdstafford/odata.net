//   OData .NET Libraries
//   Copyright (c) Microsoft Corporation
//   All rights reserved. 

//   Licensed under the Apache License, Version 2.0 (the ""License""); you may not use this file except in compliance with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0 

//   THIS CODE IS PROVIDED ON AN  *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR NON-INFRINGEMENT. 

//   See the Apache Version 2.0 License for specific language governing permissions and limitations under the License.

namespace Microsoft.OData.Core.UriParser.Parsers
{
    #region Namespaces

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.OData.Core.UriParser.Syntactic;
    using Microsoft.OData.Core.UriParser.TreeNodeKinds;
    using ODataErrorStrings = Microsoft.OData.Core.Strings;

    #endregion Namespaces

    /// <summary>
    /// Class that knows how to parse an identifier using an ExpressionLexer that is appropriately positioned.
    /// </summary>
    internal sealed class IdentifierTokenizer
    {
        /// <summary>
        /// Reference to the lexer.
        /// </summary>
        private readonly ExpressionLexer lexer;

        /// <summary>
        /// parameters from the expression parser
        /// </summary>
        private readonly HashSet<string> parameters;

        /// <summary>
        /// Object to handle the parsing of things that look like function calls.
        /// </summary>
        private readonly IFunctionCallParser functionCallParser;

        /// <summary>
        /// Parse an Identifier into the right QueryToken
        /// </summary>
        /// <param name="parameters">parameters passed in to the UriQueryExpressionParser</param>
        /// <param name="functionCallParser">Object to use to handle parsing function calls.</param>
        public IdentifierTokenizer(HashSet<string> parameters, IFunctionCallParser functionCallParser)
        {
            ExceptionUtils.CheckArgumentNotNull(parameters, "parameters");
            ExceptionUtils.CheckArgumentNotNull(functionCallParser, "functionCallParser");
            this.lexer = functionCallParser.Lexer;
            this.parameters = parameters;
            this.functionCallParser = functionCallParser;
        }

        /// <summary>
        /// Parses identifiers.
        /// </summary>
        /// <param name="parent">the syntactically bound parent of this identifier.</param>
        /// <returns>The lexical token representing the expression.</returns>
        public QueryToken ParseIdentifier(QueryToken parent)
        {
            this.lexer.ValidateToken(ExpressionTokenKind.Identifier);

            // An open paren here would indicate calling a method in regular C# syntax.
            // ToDo: Make this more generalized to work with any kind of function. 
            bool identifierIsFunction = this.lexer.ExpandIdentifierAsFunction();
            if (identifierIsFunction)
            {
                return this.functionCallParser.ParseIdentifierAsFunction(parent);
            }

            if (this.lexer.PeekNextToken().Kind == ExpressionTokenKind.Dot)
            {
                string fullIdentifier = this.lexer.ReadDottedIdentifier(false);
                return new DottedIdentifierToken(fullIdentifier, parent); 
            }
            
            return this.ParseMemberAccess(parent);
        }

        /// <summary>
        /// Parses member access.
        /// </summary>
        /// <param name="instance">Instance being accessed.</param>
        /// <returns>The lexical token representing the expression.</returns>
        public QueryToken ParseMemberAccess(QueryToken instance)
        {
            if (this.lexer.CurrentToken.Text == UriQueryConstants.Star)
            {
                return this.ParseStarMemberAccess(instance);
            }

            string identifier = this.lexer.CurrentToken.GetIdentifier();
            if (instance == null && this.parameters.Contains(identifier))
            {
                this.lexer.NextToken();
                return new RangeVariableToken(identifier);
            }

            this.lexer.NextToken();
            return new EndPathToken(identifier, instance);
        }

        /// <summary>
        /// Parses * (all member) access at the beginning of a select expression.
        /// </summary>
        /// <param name="instance">Instance being accessed.</param>
        /// <returns>The lexical token representing the expression.</returns>
        public QueryToken ParseStarMemberAccess(QueryToken instance)
        {
            if (this.lexer.CurrentToken.Text != UriQueryConstants.Star)
            {
                throw ParseError(ODataErrorStrings.UriQueryExpressionParser_CannotCreateStarTokenFromNonStar(this.lexer.CurrentToken.Text));
            }

            this.lexer.NextToken();
            return new StarToken(instance);
        }

        /// <summary>Creates an exception for a parse error.</summary>
        /// <param name="message">Message text.</param>
        /// <returns>A new Exception.</returns>
        private static Exception ParseError(string message)
        {
            return new ODataException(message);
        }
    }
}
