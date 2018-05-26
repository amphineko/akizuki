using System.Diagnostics;
using System.Threading.Tasks;
using moe.futa.akizuki.Core.Messages;
using moe.futa.akizuki.Core.Routing;

namespace moe.futa.akizuki.Core.Extensions.Vendors
{
    public delegate void InboundStatusHandler(AbstractStatus status);

    public abstract class AbstractVendor : AbstractExtension
    {
        private OutboundRouter _outRouter;

        protected AbstractVendor(OutboundRouter outRouter)
        {
            _outRouter = outRouter;
        }

        /// <summary>
        ///     Send a outgoing status
        /// </summary>
        public abstract Task Accept(AbstractStatus status);

        protected virtual void ExecuteInboundStatus(AbstractStatus status)
        {
            InboundStatusEvent?.Invoke(status);
        }

        /// <summary>
        ///     Transfer an incoming status to Router, registered in bootstrap process
        /// </summary>
        public event InboundStatusHandler InboundStatusEvent;
    }
}