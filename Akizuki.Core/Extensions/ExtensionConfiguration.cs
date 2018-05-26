using System.ComponentModel;
using System.Xml.Serialization;

namespace moe.futa.akizuki.Core.Extensions
{
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