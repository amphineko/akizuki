using System;
using System.Threading.Tasks;
using moe.futa.akizuki.Core.Messages;

namespace moe.futa.akizuki.Core.Extensions.Handlers
{
    [AsyncHandler(true)]
    public abstract class AbstractHandler : AbstractExtension
    {
        public virtual bool Accept(AbstractStatus status)
        {
            throw new NotImplementedException();
        }

        /// <returns>True if accepted and handled</returns>
        public virtual async Task<bool> AcceptAsync(AbstractStatus status)
        {
            return await Task.FromResult(false);
        }
    }
}