using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace moe.futa.akizuki.Core.Extensions
{
    /// <summary>
    ///     Extracts class FullName for XML deserialization automatically
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class XmlTypeExAttribute : XmlTypeAttribute
    {
        public XmlTypeExAttribute(Type attachedType) : base(attachedType.FullName)
        {
            // XmlTypeEx is designed for fixing XML configurations only
            Debug.Assert(typeof(ExtensionConfiguration).IsAssignableFrom(attachedType));
        }
    }

    [XmlTypeEx(typeof(ExtensionConfiguration))]
    public class ExtensionConfiguration
    {
        [DefaultValue(null)] [XmlElement(IsNullable = true)]
        public string Alias;

        /// <summary>
        ///     FullName (Type.FullName) of Extension requested to be installed
        /// </summary>
        [XmlElement] public string FullName;
    }
}