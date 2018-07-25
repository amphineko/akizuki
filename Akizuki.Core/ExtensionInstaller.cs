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
        public static AbstractPreroutingHook InstallHook(Type type, ExtensionConfiguration configuration,
            InboundRouter inRouter)
        {
            var hook = (AbstractPreroutingHook) Activator.CreateInstance(type, configuration);
            inRouter.AddPreroutingHook(hook);
            return hook;
        }

        public static AbstractHandler InstallHandler(Type type, ExtensionConfiguration configuration,
            InboundRouter inRouter, OutboundRouter outRouter)
        {
            var handler = (AbstractHandler) Activator.CreateInstance(type, configuration, outRouter);
            inRouter.AddStatusHandler(handler);
            return handler;
        }

        public static AbstractVendor InstallVendor(Type type, ExtensionConfiguration configuration,
            OutboundRouter outRouter, InboundRouter inRouter)
        {
            return (AbstractVendor) Activator.CreateInstance(type, configuration, outRouter, inRouter);
        }
    }
}