using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Akizuki.Core.Extensions
{
    /// <summary>
    ///     Extends System.Reflection.Assembly for Extension loading
    /// </summary>
    internal static class AssemblyExtensions
    {
        /// <summary>
        ///     Get exported types which are sub-classes of T
        /// </summary>
        public static IList<Type> GetExportedTypes<T>(this Assembly assembly)
        {
            return assembly.GetExportedTypes().Where(type => type.IsSubclassOf(typeof(T))).ToList();
        }
    }
}