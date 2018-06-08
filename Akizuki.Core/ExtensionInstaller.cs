using System;
using System.Collections.Generic;
using Akizuki.Core.Extensions;
using Akizuki.Core.Extensions.Handlers;
using Akizuki.Core.Extensions.Hooks;
using Akizuki.Core.Extensions.Vendors;
using Akizuki.Core.Routing;
using NLog;

namespace Akizuki.Core
{
    public static class ExtensionInstaller
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static IList<AbstractExtension> Install(this ExtensionList list, ClassRepository repository,
            InboundRouter inRouter,
            OutboundRouter outRouter)
        {
            var instances = new List<AbstractExtension>();

            foreach (var configuration in list.Extensions)
            {
                var type = repository.GetExtensionType(configuration.FullName);

                if (typeof(AbstractHandler).IsAssignableFrom(type))
                    instances.Add(InstallHandler(type, configuration, inRouter, outRouter));
                if (typeof(AbstractPreroutingHook).IsAssignableFrom(type))
                    instances.Add(InstallHook(type, configuration, inRouter));
                if (typeof(AbstractVendor).IsAssignableFrom(type))
                    instances.Add(InstallVendor(type, configuration, outRouter, inRouter));

                Logger.Info($"Extension {configuration.Alias ?? "undefined"}:{configuration.FullName} installed");
            }

            return instances;
        }

        private static AbstractPreroutingHook InstallHook(Type type, ExtensionConfiguration configuration,
            InboundRouter inRouter)
        {
            var hook = (AbstractPreroutingHook) Activator.CreateInstance(type, configuration);
            inRouter.AddPreroutingHook(hook);
            return hook;
        }

        private static AbstractHandler InstallHandler(Type type, ExtensionConfiguration configuration,
            InboundRouter inRouter, OutboundRouter outRouter)
        {
            var handler = (AbstractHandler) Activator.CreateInstance(type, configuration, outRouter);
            inRouter.AddStatusHandler(handler);
            return handler;
        }

        private static AbstractVendor InstallVendor(Type type, ExtensionConfiguration configuration,
            OutboundRouter outRouter, InboundRouter inRouter)
        {
            return (AbstractVendor) Activator.CreateInstance(type, configuration, outRouter, inRouter);
        }
    }
}