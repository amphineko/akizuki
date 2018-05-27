using System.Xml.Serialization;
using moe.futa.akizuki.Core.Extensions;

namespace moe.futa.akizuki.Core
{
    [XmlRoot]
    public sealed class Configuration
    {
        [XmlElement] public ExtensionList Extensions;
    }

    public sealed class ExtensionList
    {
        [XmlElement("Extension", typeof(ExtensionConfiguration))]
        public ExtensionConfiguration[] Extensions;
    }
}