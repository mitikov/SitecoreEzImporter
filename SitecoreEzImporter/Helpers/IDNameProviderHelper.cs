using Sitecore;
using Sitecore.Data;
using Sitecore.Diagnostics.Debugger;
using System;

namespace EzImporter.Helpers
{
    /// <summary>
    /// Adds human-friendly description for <see cref="ID"/> during debugging safely.
    /// <para>In certain contexts, related assemblies might be missing causing parent API to fail, thus <see cref="TypeInitializationException"/> might bubble.</para>
    /// <para>This API guards against exceptions.</para>
    /// </summary>
    public static class IDNameProviderHelper
    {
        /// <summary>
        /// Safely adds description for <paramref name="id"/> during debugging.
        /// <para>Instead of dealing with unfriendly <see cref="Guid"/>, developer can get friendly description where well-known guid is coming from.</para>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ownerClass"></param>
        /// <param name="propertyName"></param>
        public static void AddBindingWrapped(ID id, Type ownerClass, string propertyName)
        {
            try
            {
                IDNameProvider.AddBinding(id, $"{ownerClass.Name}{propertyName}");
            }
            catch 
            {
                MainUtil.Nop("Nothing bad happened due to id was not added to debug view.");
            }            
        }
    }
}