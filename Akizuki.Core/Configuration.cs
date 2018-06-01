using System.Xml.Serialization;
using Akizuki.Core.Extensions;

namespace Akizuki.Core
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