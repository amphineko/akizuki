using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace moe.futa.akizuki.Core
{
    [XmlRoot]
    public class Configuration
    {
        [XmlElement]
        public ExtensionConfiguration Extensions;

        public static Configuration LoadFile(string path)
        {
            var serializer = new XmlSerializer(typeof(Configuration));
            using (var stream = new FileStream(path, FileMode.Open))
                return (Configuration) serializer.Deserialize(stream);
        }
    }

    public class ExtensionConfiguration
    {
        [XmlElement]
        public string SearchPath;     // defaults to current path
    }
}
