using System;
using System.Threading.Tasks;
using moe.futa.akizuki.Core.Messages;

namespace moe.futa.akizuki.Core.Extensions.Handlers
{
    [AsyncHandler(true)]
    public abstract class AbstractHandler : AbstractExtension
    {
        protected AbstractHandler(ExtensionConfiguration configuration) : base(configuration)
        {
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