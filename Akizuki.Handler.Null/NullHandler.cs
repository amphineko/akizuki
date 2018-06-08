using System.Threading.Tasks;
using Akizuki.Core.Extensions;
using Akizuki.Core.Extensions.Handlers;
using Akizuki.Core.Messages;
using Akizuki.Core.Routing;
using NLog;

namespace Akizuki.Handler.Null
{
    public sealed class NullHandler : AbstractHandler
    {
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public NullHandler(ExtensionConfiguration configuration, OutboundRouter outRouter) : base(configuration,
            outRouter)
        {
        }

        public override async Task<bool> AcceptAsync(AbstractStatus status)
        {
            // return true to indicate handled
            // statuses won't be passed to next handlers
            // TODO: prevent duplicated dump with consoledump handler
            _logger.Debug($"INBOUND: {status}");
            return await Task.FromResult(true);
        }

        public override void SetDisabled()
        {
            base.SetDisabled();
            _logger.Info("NullHandler has been disabled");
        }

        public override void SetEnabled()
        {
            base.SetEnabled();
            _logger.Warn("NullHandler has been enabled, next handlers won't receive any statuses");
        }
    }
}