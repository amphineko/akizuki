using System.Threading.Tasks;
using Akizuki.Core.Extensions;
using Akizuki.Core.Extensions.Vendors;
using Akizuki.Core.Messages;
using Akizuki.Core.Routing;
using NLog;

namespace Akizuki.Vendor.Null
{
    /// <summary>
    ///     A virtual vendor blackholes any statuses which are sent to null identifier
    ///     Statically registers at chat:null/null and user:null/null
    /// </summary>
    [ExtensionConfigurationType(typeof(Configuration))]
    public sealed class NullVendor : AbstractVendor
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public NullVendor(ExtensionConfiguration configuration, OutboundRouter outRouter, InboundRouter inRouter) :
            base(configuration, outRouter, inRouter)
        {
            var config = (Configuration) configuration;
            foreach (var identifier in config.Identifier)
                outRouter.Register(new Identifier(identifier), this);
        }

        public override async Task Accept(AbstractStatus status)
        {
            _logger.Info($"OUTBOUND from {status.From}: {status}");
            await Task.CompletedTask;
        }
    }
}