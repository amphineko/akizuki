using System.Xml.Serialization;

namespace moe.futa.akizuki.Core.Extensions
{
    public sealed class ExtensionRepositoryConfiguration
    {
        /// <summary>
        ///     Path to search extension assemblies
        /// </summary>
        [XmlElement] public string SearchPath;
    }
}