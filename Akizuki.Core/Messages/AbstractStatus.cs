using moe.futa.akizuki.Core.Extensions.Vendors;

namespace moe.futa.akizuki.Core.Messages
{
    public abstract class AbstractStatus
    {
        public Identifier Context { get; set; } = null;

        public Identifier From { get; set; } = null;

        public Identifier To { get; set; } = null;

        public AbstractVendor Vendor { get; set; } = null;
    }
}