using System.Xml.Serialization;
using moe.futa.akizuki.Core.Extensions;

namespace moe.futa.akizuki.Vendor.Null
{
    [XmlTypeEx(typeof(Configuration))]
    public class Configuration : ExtensionConfiguration
    {
        [XmlElement] public string[] Identifier;
    }
}