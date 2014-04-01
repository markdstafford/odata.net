//   OData .NET Libraries
//   Copyright (c) Microsoft Corporation
//   All rights reserved. 

//   Licensed under the Apache License, Version 2.0 (the ""License""); you may not use this file except in compliance with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0 

//   THIS CODE IS PROVIDED ON AN  *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR NON-INFRINGEMENT. 

//   See the Apache Version 2.0 License for specific language governing permissions and limitations under the License.

using System;
using Microsoft.OData.Edm.Csdl.Internal.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;
using Microsoft.OData.Edm.Internal;
using Microsoft.OData.Edm.Library.Expressions;

namespace Microsoft.OData.Edm.Csdl.Internal.CsdlSemantics
{
    internal abstract class CsdlSemanticsOperationImport : CsdlSemanticsFunctionBase, IEdmOperationImport
    {
        private readonly CsdlOperationImport operationImport;
        private readonly CsdlSemanticsEntityContainer container;

        private readonly Cache<CsdlSemanticsOperationImport, IEdmExpression> entitySetCache = new Cache<CsdlSemanticsOperationImport, IEdmExpression>();
        private static readonly Func<CsdlSemanticsOperationImport, IEdmExpression> ComputeEntitySetFunc = (me) => me.ComputeEntitySet();

        protected CsdlSemanticsOperationImport(CsdlSemanticsEntityContainer container, CsdlOperationImport operationImport)
            : base(container.Context, operationImport)
        {
            this.container = container;
            this.operationImport = operationImport;
        }

        public bool IsSideEffecting
        {
            get { return this.operationImport.SideEffecting; }
        }

        public bool IsComposable
        {
            get { return this.operationImport.Composable; }
        }

        public bool IsBindable
        {
            get { return this.operationImport.Bindable; }
        }

        public IEdmEntityContainer Container
        {
            get { return this.container; }
        }

        public IEdmExpression EntitySet
        { 
            get 
            {
                return this.entitySetCache.GetValue(this, ComputeEntitySetFunc, null);
            } 
        }

        private IEdmExpression ComputeEntitySet()
        {
            if (this.operationImport.EntitySet != null)
            {
                IEdmEntitySet entitySet = this.container.FindEntitySet(this.operationImport.EntitySet) ??
                    new UnresolvedEntitySet(this.operationImport.EntitySet, this.Container, this.Location);
                return new OperationImportEntitySetReferenceExpression(entitySet) { Location = this.Location };
            }

            if (this.operationImport.EntitySetPath != null)
            {
                return new OperationImportPathExpression(this.operationImport.EntitySetPath) { Location = this.Location };
            }

            return null;
        }

        private sealed class OperationImportEntitySetReferenceExpression : EdmEntitySetReferenceExpression, IEdmLocatable
        {
            internal OperationImportEntitySetReferenceExpression(IEdmEntitySet referencedEntitySet)
                : base(referencedEntitySet)
            {
            }

            public EdmLocation Location
            {
                get;
                set;
            }
        }

        private sealed class OperationImportPathExpression : EdmPathExpression, IEdmLocatable
        {
            internal OperationImportPathExpression(string path)
                : base(path)
            {
            }

            public EdmLocation Location
            {
                get;
                set;
            }
        }
    }
}
