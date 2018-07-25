using System.Xml.Serialization;
using Akizuki.Core.Extensions;
using Akizuki.Core.Extensions.Handlers;
using Akizuki.Core.Extensions.Hooks;
using Akizuki.Core.Extensions.Vendors;
using Akizuki.Core.Routing;

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

        public void Install(ClassRepository classRepo, InstanceRepository instanceRepo, InboundRouter inboundRouter, OutboundRouter outboundRouter)
        {
            foreach (var config in Extensions)
            {
                var type = classRepo.GetExtensionType(config.FullName);

                if (typeof(AbstractHandler).IsAssignableFrom(type))
                    instanceRepo.AddInstance(ExtensionInstaller.InstallHandler(type, config, inboundRouter, outboundRouter));
                if (typeof(AbstractPreroutingHook).IsAssignableFrom(type))
                    instanceRepo.AddInstance(ExtensionInstaller.InstallHook(type, config, inboundRouter));
                if (typeof(AbstractVendor).IsAssignableFrom(type))
                    instanceRepo.AddInstance(ExtensionInstaller.InstallVendor(type, config, outboundRouter, inboundRouter));
            }
        }


    }
}