using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using moe.futa.akizuki.Core.Messages;

namespace moe.futa.akizuki.Core.Extensions.Vendors
{
    public delegate void InboundStatusHandler(AbstractStatus status);

    public abstract class AbstractVendor : AbstractExtension
    {
        /// <summary>
        /// Send a outgoing status
        /// </summary>
        public abstract Task Accept(AbstractStatus status);

        protected virtual void ExecuteInboundStatus(AbstractStatus status) => InboundStatusEvent?.Invoke(status);

        /// <summary>
        /// Transfer an incoming status to Router, registered in bootstrap process
        /// </summary>
        public event InboundStatusHandler InboundStatusEvent;
    }
}