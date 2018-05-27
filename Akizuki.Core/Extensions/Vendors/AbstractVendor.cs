﻿using System.Threading.Tasks;
using moe.futa.akizuki.Core.Messages;
using moe.futa.akizuki.Core.Routing;

namespace moe.futa.akizuki.Core.Extensions.Vendors
{
    public delegate void InboundStatusHandler(AbstractStatus status);

    public abstract class AbstractVendor<TConfiguration> : AbstractExtension<TConfiguration>
        where TConfiguration : ExtensionConfiguration
    {
        protected InboundRouter _inRouter;
        protected OutboundRouter _outRouter;

        protected AbstractVendor(TConfiguration configuration, OutboundRouter outRouter, InboundRouter inRouter) : base(
            configuration)
        {
            _outRouter = outRouter;
            _inRouter = inRouter;
        }

        /// <summary>
        ///     Sends an outgoing status
        /// </summary>
        public abstract Task Accept(AbstractStatus status);
    }

    public abstract class AbstractVendor : AbstractVendor<ExtensionConfiguration>
    {
        protected AbstractVendor(ExtensionConfiguration configuration, OutboundRouter outRouter, InboundRouter inRouter)
            : base(configuration, outRouter, inRouter)
        {
        }
    }
}