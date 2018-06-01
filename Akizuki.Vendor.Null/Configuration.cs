using System.Xml.Serialization;
using Akizuki.Core.Extensions;

namespace Akizuki.Vendor.Null
{
    [XmlTypeEx(typeof(Configuration))]
    public class Configuration : ExtensionConfiguration
    {
        [XmlElement] public string[] Identifier;
    }
}