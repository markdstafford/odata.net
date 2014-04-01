//   OData .NET Libraries
//   Copyright (c) Microsoft Corporation
//   All rights reserved. 

//   Licensed under the Apache License, Version 2.0 (the ""License""); you may not use this file except in compliance with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0 

//   THIS CODE IS PROVIDED ON AN  *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR NON-INFRINGEMENT. 

//   See the Apache Version 2.0 License for specific language governing permissions and limitations under the License.

namespace Microsoft.OData.Core.UriParser.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.OData.Core.UriParser.Syntactic;
    using Microsoft.OData.Core.UriParser.Visitors;
    using ODataErrorStrings = Microsoft.OData.Core.Strings;

    /// <summary>
    /// Translator from the old expand syntax tree to the new Expand Option syntax tree
    /// </summary>
    //// TODO 1466134 This class becomes ExpandTreeNormalizer when we switch over to V4.
    internal sealed class V4ExpandTreeNormalizer : IExpandTreeNormalizer
    {
        /// <summary>
        /// Normalize an expand syntax tree into the new ExpandOption syntax.
        /// </summary>
        /// <param name="treeToNormalize">the tree to normalize</param>
        /// <returns>a new tree, in the new ExpandOption syntax</returns>
        public ExpandToken NormalizeExpandTree(ExpandToken treeToNormalize)
        {
            // To normalize the expand tree we need to
            // 1) invert the path tree on each of its expand term tokens
            // 2) combine terms that start with the path tree
            ExpandToken invertedPathTree = InvertPaths(treeToNormalize);

            return CombineTerms(invertedPathTree);
        }

        /// <summary>
        /// Invert the all of the paths in an expandToken, such that they are now in the same order as they are present in the 
        /// base url
        /// </summary>
        /// <param name="treeToInvert">the tree to invert paths on</param>
        /// <returns>a new tree with all of its paths inverted</returns>
        public ExpandToken InvertPaths(ExpandToken treeToInvert)
        {
            // iterate through each expand term token, and reverse the tree in its path property
            List<ExpandTermToken> updatedTerms = new List<ExpandTermToken>();
            foreach (ExpandTermToken term in treeToInvert.ExpandTerms)
            {
                PathReverser pathReverser = new PathReverser();
                PathSegmentToken reversedPath = term.PathToNavProp.Accept(pathReverser);
                ExpandToken subExpandTree;
                if (term.ExpandOption != null)
                {
                    subExpandTree = this.InvertPaths(term.ExpandOption);
                }
                else
                {
                    subExpandTree = null;
                }

                ExpandTermToken newTerm = new ExpandTermToken(reversedPath, term.FilterOption, term.OrderByOption, term.TopOption, term.SkipOption, term.CountQueryOption, term.SelectOption, subExpandTree);
                updatedTerms.Add(newTerm);
            }

            return new ExpandToken(updatedTerms);
        }

        /// <summary>
        /// Collapse all redundant terms in an expand tree
        /// </summary>
        /// <param name="treeToCollapse">the tree to collapse</param>
        /// <returns>A new tree with all redundant terms collapsed.</returns>
        public ExpandToken CombineTerms(ExpandToken treeToCollapse)
        {
            var combinedTerms = new Dictionary<PathSegmentToken, ExpandTermToken>(new PathSegmentTokenEqualityComparer());
            foreach (ExpandTermToken termToken in treeToCollapse.ExpandTerms)
            {
                ExpandTermToken finalTermToken = termToken;
                if (termToken.ExpandOption != null)
                {
                    ExpandToken newSubExpand = CombineTerms(termToken.ExpandOption);
                    finalTermToken = new ExpandTermToken(
                                                              termToken.PathToNavProp,
                                                              termToken.FilterOption,
                                                              termToken.OrderByOption,
                                                              termToken.TopOption,
                                                              termToken.SkipOption,
                                                              termToken.CountQueryOption,
                                                              termToken.SelectOption,
                                                              newSubExpand);
                }   

                AddOrCombine(combinedTerms, finalTermToken);
            }

            return new ExpandToken(combinedTerms.Values);
        }

        /// <summary>
        /// Expand all the PathTokens in a particular term into their own separate terms.
        /// </summary>
        /// <param name="termToExpand">the term to expand</param>
        /// <returns>a new ExpandTermToken with each PathToken at its own level.</returns>
        public ExpandTermToken BuildSubExpandTree(ExpandTermToken termToExpand)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// add a new expandTermToken into an exisiting token, adding any additional levels and trees along the way.
        /// </summary>
        /// <param name="existingToken">the exisiting (already expanded) token</param>
        /// <param name="newToken">the new (already expanded) token</param>
        /// <returns>the combined token, or, if the two are mutually exclusive, the same tokens</returns>
        public ExpandTermToken CombineTerms(ExpandTermToken existingToken, ExpandTermToken newToken)
        {
            Debug.Assert(new PathSegmentTokenEqualityComparer().Equals(existingToken.PathToNavProp, newToken.PathToNavProp), "Paths should be equal.");

            List<ExpandTermToken> childNodes = CombineChildNodes(existingToken, newToken).ToList();
            return new ExpandTermToken(
                    existingToken.PathToNavProp,
                    existingToken.FilterOption,
                    existingToken.OrderByOption,
                    existingToken.TopOption,
                    existingToken.SkipOption,
                    existingToken.CountQueryOption,
                    existingToken.SelectOption,
                    new ExpandToken(childNodes));
        }

        /// <summary>
        /// Combine the child nodes of twoExpandTermTokens into one list of tokens
        /// </summary>
        /// <param name="existingToken">the existing token to to</param>
        /// <param name="newToken">the new token containing terms to add</param>
        /// <returns>a combined list of the all child nodes of the two tokens.</returns>
        public IEnumerable<ExpandTermToken> CombineChildNodes(ExpandTermToken existingToken, ExpandTermToken newToken)
        {
            if (existingToken.ExpandOption == null && newToken.ExpandOption == null)
            {
                return new List<ExpandTermToken>(); 
            }

            var childNodes = new Dictionary<PathSegmentToken, ExpandTermToken>(new PathSegmentTokenEqualityComparer());
            if (existingToken.ExpandOption != null)
            {
                AddChildOptionsToDictionary(existingToken, childNodes);
            }

            if (newToken.ExpandOption != null)
            {
                AddChildOptionsToDictionary(newToken, childNodes);
            }

            return childNodes.Values;
        }

        /// <summary>
        /// Add child options to a new dictionary
        /// </summary>
        /// <param name="newToken">the token with child nodes to add to the dictionary</param>
        /// <param name="combinedTerms">dictionary to add child nodes to</param>
        private void AddChildOptionsToDictionary(ExpandTermToken newToken, Dictionary<PathSegmentToken, ExpandTermToken> combinedTerms)
        {
            foreach (ExpandTermToken expandedTerm in newToken.ExpandOption.ExpandTerms)
            {
                AddOrCombine(combinedTerms, expandedTerm);
            }
        }

        /// <summary>
        /// Adds the expand token to the dictionary or combines it with an existing  or combines it with another existing token with an equivalent path.
        /// </summary>
        /// <param name="combinedTerms">The combined terms dictionary.</param>
        /// <param name="expandedTerm">The expanded term to add or combine.</param>
        private void AddOrCombine(IDictionary<PathSegmentToken, ExpandTermToken> combinedTerms, ExpandTermToken expandedTerm)
        {
            ExpandTermToken existingTerm;
            if (combinedTerms.TryGetValue(expandedTerm.PathToNavProp, out existingTerm))
            {
                combinedTerms[expandedTerm.PathToNavProp] = CombineTerms(expandedTerm, existingTerm);
            }
            else
            {
                combinedTerms.Add(expandedTerm.PathToNavProp, expandedTerm);
            }
        }
    }
}
