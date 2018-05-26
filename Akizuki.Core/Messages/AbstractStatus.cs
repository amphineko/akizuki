using moe.futa.akizuki.Core.Extensions.Vendors;

namespace moe.futa.akizuki.Core.Messages
{
    public abstract class AbstractStatus
    {
        public Identifier Context { get; set; } = null;

        public Identifier From { get; set; } = null;

        public Identifier To { get; set; } = null;

        /// <remarks>
        ///     Vendor instances shouldn't be accessed directly by design.
        ///     In order to create replies and send outgoing statuses,
        ///     use Vendor.GetType() and OutboundRouter.
        /// </remarks>
        public AbstractVendor Vendor { get; set; } = null;
    }
}