using System.Threading.Tasks;
using moe.futa.akizuki.Core.Extensions;
using moe.futa.akizuki.Core.Extensions.Vendors;
using moe.futa.akizuki.Core.Messages;
using moe.futa.akizuki.Core.Routing;
using NLog;

namespace moe.futa.akizuki.Vendor.Null
{
    /// <summary>
    ///     A virtual vendor blackholes any statuses which are sent to null identifier
    ///     Statically registers at chat:null/null and user:null/null
    /// </summary>
    public sealed class NullVendor : AbstractVendor
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public NullVendor(ExtensionConfiguration configuration, OutboundRouter outRouter) : base(configuration,
            outRouter)
        {
            // TODO: set null endpoints by configuration
            outRouter.Register(new Identifier("chat", "null", "null"), this);
            outRouter.Register(new Identifier("user", "null", "null"), this);
        }

        public override async Task Accept(AbstractStatus status)
        {
            _logger.Info($"OUTBOUND from {status.From}: {status}");
            await Task.CompletedTask;
        }
    }
}