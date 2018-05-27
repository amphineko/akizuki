using System;
using moe.futa.akizuki.Core.Extensions;
using moe.futa.akizuki.Core.Extensions.Handlers;
using moe.futa.akizuki.Core.Extensions.Hooks;
using moe.futa.akizuki.Core.Extensions.Vendors;
using moe.futa.akizuki.Core.Routing;
using NLog;

namespace moe.futa.akizuki.Core
{
    public static class ExtensionInstaller
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public static void Install(this ExtensionList list, ExtensionRepository repository, InboundRouter inRouter,
            OutboundRouter outRouter)
        {
            foreach (var configuration in list.Extensions)
            {
                var type = repository.GetExtensionType(configuration.FullName);

                if (typeof(AbstractHandler).IsAssignableFrom(type))
                    InstallHandler(type, configuration, inRouter);
                if (typeof(AbstractPreroutingHook).IsAssignableFrom(type))
                    InstallHook(type, configuration, inRouter);
                if (typeof(AbstractVendor).IsAssignableFrom(type))
                    InstallVendor(type, configuration, outRouter, inRouter);

                Logger.Info($"Extension {configuration.Alias ?? "undefined"}:{configuration.FullName} installed");
            }
        }

        private static void InstallHook(Type type, ExtensionConfiguration configuration, InboundRouter inRouter)
        {
            var hook = (AbstractPreroutingHook) Activator.CreateInstance(type, configuration);
            inRouter.AddPreroutingHook(hook);
        }

        private static void InstallHandler(Type type, ExtensionConfiguration configuration, InboundRouter inRouter)
        {
            var handler = (AbstractHandler) Activator.CreateInstance(type, configuration);
            inRouter.AddStatusHandler(handler);
        }

        private static void InstallVendor(Type type, ExtensionConfiguration configuration, OutboundRouter outRouter,
            InboundRouter inRouter)
        {
            Activator.CreateInstance(type, configuration, outRouter, inRouter);
        }
    }
}