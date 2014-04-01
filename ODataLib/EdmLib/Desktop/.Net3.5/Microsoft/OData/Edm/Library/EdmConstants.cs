//   OData .NET Libraries
//   Copyright (c) Microsoft Corporation
//   All rights reserved. 

//   Licensed under the Apache License, Version 2.0 (the ""License""); you may not use this file except in compliance with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0 

//   THIS CODE IS PROVIDED ON AN  *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR NON-INFRINGEMENT. 

//   See the Apache Version 2.0 License for specific language governing permissions and limitations under the License.

using System;

namespace Microsoft.OData.Edm.Library
{
    /// <summary>
    /// Contains constant values that apply to the EDM model, regardless of source (for CSDL/EDMX specific constants see <see cref="Microsoft.OData.Edm.Csdl.CsdlConstants"/>).
    /// </summary>
    public static class EdmConstants
    {
        /// <summary>
        /// Version 4.0 of EDM. Corresponds to CSDL namespace "http://docs.oasis-open.org/odata/ns/edmx".
        /// </summary>
        public static readonly Version EdmVersion4 = new Version(4, 0);
     
        /// <summary>
        /// The current latest version of EDM.
        /// </summary>
        public static readonly Version EdmVersionLatest = EdmVersion4;

        internal const string EdmNamespace = "Edm";
        internal const string TransientNamespace = "Transient";

        internal const string XmlPrefix = "xml";
        internal const string XmlNamespacePrefix = "xmlns";

        /// <summary>
        /// The URI of annotations that will be serialized as documentation elements.
        /// </summary>
        internal const string DocumentationUri = "http://schemas.microsoft.com/ado/2011/04/edm/documentation";

        /// <summary>
        /// The local name of annotations that will be serialized as documentation elements.
        /// </summary>
        internal const string DocumentationAnnotation = "Documentation";

        /// <summary>
        /// The URI of annotations that are internal and will not be serialized.
        /// </summary>
        internal const string InternalUri = "http://schemas.microsoft.com/ado/2011/04/edm/internal";

        /// <summary>
        /// The local name of the annotation that stores the data services version attribute for EDMX serialization.
        /// </summary>
        internal const string DataServiceVersion = "DataServiceVersion";

        /// <summary>
        /// The local name of the annotation that stores the max data services version attribute for EDMX serialization.
        /// </summary>
        internal const string MaxDataServiceVersion = "MaxDataServiceVersion";

        /// <summary>
        /// The local name of the annotation that stores EDM version of a model.
        /// </summary>
        internal const string EdmVersionAnnotation = "EdmVersion";

        internal const string FacetName_Nullable = "Nullable";
        internal const string FacetName_Precision = "Precision";
        internal const string FacetName_Scale = "Scale";
        internal const string FacetName_MaxLength = "MaxLength";
        internal const string FacetName_FixedLength = "FixedLength";
        internal const string FacetName_Unicode = "Unicode";
        internal const string FacetName_Collation = "Collation";
        internal const string FacetName_Srid = "SRID";

        internal const string Value_UnknownType = "UnknownType";
        internal const string Value_UnnamedType = "UnnamedType";
        internal const string Value_Max = "Max";
        internal const string Value_SridVariable = "Variable";

        internal const string Type_Association = "Association";
        internal const string Type_Collection = "Collection";
        internal const string Type_Complex = "Complex";
        internal const string Type_Entity = "Entity";
        internal const string Type_EntityReference = "EntityReference";
        internal const string Type_Enum = "Enum";
        internal const string Type_Row = "Row";

        internal const string Type_Primitive = "Primitive";
        internal const string Type_Binary = "Binary";
        internal const string Type_Decimal = "Decimal";
        internal const string Type_String = "String";
        internal const string Type_Stream = "Stream";
        internal const string Type_Spatial = "Spatial";
        internal const string Type_Temporal = "Temporal";

        internal const string Type_Structured = "Structured";

        internal const int Max_Precision = Int32.MaxValue;
        internal const int Min_Precision = 0;
    }
}
