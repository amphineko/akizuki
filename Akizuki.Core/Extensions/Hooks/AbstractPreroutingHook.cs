using System.Collections.Generic;
using System.Threading.Tasks;
using moe.futa.akizuki.Core.Messages;

namespace moe.futa.akizuki.Core.Extensions.Hooks
{
    public abstract class AbstractPreroutingHook<TConfiguration> : AbstractExtension<TConfiguration>
        where TConfiguration : ExtensionConfiguration
    {
        protected AbstractPreroutingHook(TConfiguration configuration) : base(configuration)
        {
        }

        public virtual async Task<List<AbstractStatus>> Accept(List<AbstractStatus> statuses)
        {
            return await Task.FromResult(statuses);
        }
    }

    public abstract class AbstractPreroutingHook : AbstractPreroutingHook<ExtensionConfiguration>
    {
        protected AbstractPreroutingHook(ExtensionConfiguration configuration) : base(configuration)
        {
        }
    }
}