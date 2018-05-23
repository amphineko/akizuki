using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using moe.futa.akizuki.Core.Messages;

namespace moe.futa.akizuki.Core.Extensions.Hooks
{
    public abstract class AbstractPreroutingHook : AbstractExtension
    {
        public virtual Task<List<AbstractStatus>> Accept(List<AbstractStatus> statuses)
        {
            return Task.FromResult(statuses);
        }
    }
}
