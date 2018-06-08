using System;
using System.Threading.Tasks;
using Akizuki.Core.Messages;
using Akizuki.Core.Routing;

namespace Akizuki.Core.Extensions.Handlers
{
    [AsyncHandler(true)]
    public abstract class AbstractHandler : AbstractExtension
    {
        protected readonly OutboundRouter _outRouter;

        protected AbstractHandler(ExtensionConfiguration configuration, OutboundRouter outRouter) : base(configuration)
        {
            _outRouter = outRouter;
        }

        public virtual bool Accept(AbstractStatus status)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Accepts an incoming status
        /// </summary>
        /// <returns>
        ///     True for accepted statuses, false for passing to next handlers
        /// </returns>
        public virtual async Task<bool> AcceptAsync(AbstractStatus status)
        {
            return await Task.FromResult(false);
        }
    }
}