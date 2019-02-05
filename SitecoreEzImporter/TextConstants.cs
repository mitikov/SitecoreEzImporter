using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EzImporter
{
    public static class TextConstants
    {
        /// <summary>
        /// The name of work-in-progress database.
        /// <para>Defaults to "master".</para>
        /// </summary>
        public static readonly string ContentDatabaseName = "master";


        public static class PipelineNames
        {
            /// <summary>
            /// The name of import items pipeline.
            /// <para>Defaults to "importItems"</para>
            /// </summary>
            public static readonly string ImportItems = "importItems";
        }
    }
}