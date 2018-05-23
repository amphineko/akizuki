using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using moe.futa.akizuki.Core.Messages;

namespace moe.futa.akizuki.Core.Extensions.Handlers
{
    public abstract class AbstractHandler : AbstractExtension
    {
        /// <returns>True if accepted and handled</returns>
        public virtual Task<bool> Accept(AbstractStatus status)
        {
            return Task.FromResult(false);
        }
    }
}
