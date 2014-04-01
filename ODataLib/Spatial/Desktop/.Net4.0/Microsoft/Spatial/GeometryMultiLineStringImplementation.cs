//   OData .NET Libraries
//   Copyright (c) Microsoft Corporation
//   All rights reserved. 

//   Licensed under the Apache License, Version 2.0 (the ""License""); you may not use this file except in compliance with the License. You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0 

//   THIS CODE IS PROVIDED ON AN  *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR NON-INFRINGEMENT. 

//   See the Apache Version 2.0 License for specific language governing permissions and limitations under the License.

namespace Microsoft.Data.Spatial
{
    using System;
    using System.Collections.ObjectModel;
#if WINDOWS_PHONE
    using System.Runtime.Serialization;
#endif
    using Microsoft.Spatial;

    /// <summary>
    /// Geometry Multi-LineString
    /// </summary>
#if WINDOWS_PHONE
    [DataContract]
#endif
    internal class GeometryMultiLineStringImplementation : GeometryMultiLineString
    {
        /// <summary>
        /// Line Strings
        /// </summary>
        private GeometryLineString[] lineStrings;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="coordinateSystem">The CoordinateSystem</param>
        /// <param name="creator">The implementation that created this instance.</param>
        /// <param name="lineStrings">Line Strings</param>
        internal GeometryMultiLineStringImplementation(CoordinateSystem coordinateSystem, SpatialImplementation creator, params GeometryLineString[] lineStrings)
            : base(coordinateSystem, creator)
        {
            this.lineStrings = lineStrings ?? new GeometryLineString[0];
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="creator">The implementation that created this instance.</param>
        /// <param name="lineStrings">Line Strings</param>
        internal GeometryMultiLineStringImplementation(SpatialImplementation creator, params GeometryLineString[] lineStrings)
            : this(CoordinateSystem.DefaultGeometry, creator, lineStrings)
        {
        }

        /// <summary>
        /// Is MultiLineString Empty
        /// </summary>
        public override bool IsEmpty
        {
            get
            {
                return this.lineStrings.Length == 0;
            }
        }

        /// <summary>
        /// Geometry
        /// </summary>
        public override ReadOnlyCollection<Geometry> Geometries
        {
            get { return new ReadOnlyCollection<Geometry>(this.lineStrings); }
        }

        /// <summary>
        /// Line Strings
        /// </summary>
        public override ReadOnlyCollection<GeometryLineString> LineStrings
        {
            get { return new ReadOnlyCollection<GeometryLineString>(this.lineStrings); }
        }

#if WINDOWS_PHONE
        /// <summary>
        /// internal GeometryLineString array property to support serializing and de-serializing this instance.
        /// </summary>
        [DataMember]
        internal GeometryLineString[] LineStringsArray
        {
            get { return this.lineStrings; }

            set { this.lineStrings = value; }
        }
#endif
        /// <summary>
        /// Sends the current spatial object to the given pipeline
        /// </summary>
        /// <param name="pipeline">The spatial pipeline</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "base does the validation")]
        public override void SendTo(GeometryPipeline pipeline)
        {
            base.SendTo(pipeline);
            pipeline.BeginGeometry(SpatialType.MultiLineString);

            for (int i = 0; i < this.lineStrings.Length; ++i)
            {
                this.lineStrings[i].SendTo(pipeline);
            }

            pipeline.EndGeometry();
        }
    }
}
