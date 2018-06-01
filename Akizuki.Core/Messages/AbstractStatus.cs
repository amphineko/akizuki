using System;
using System.Reflection;
using Akizuki.Core.Extensions.Vendors;

namespace Akizuki.Core.Messages
{
    [StatusClassType(StatusType.Abstract)]
    public abstract class AbstractStatus
    {
        protected AbstractStatus()
        {
            Type = GetType().GetCustomAttribute<StatusClassTypeAttribute>().Type;
            if (Type == StatusType.Abstract)
                throw new InvalidOperationException("Constructing abstract status classes is not allowed");
        }

        /// <remarks>
        ///     Different media types are stored in separated objects,
        ///     e.g. one message contains two parts of text sections and one image [string, VendorImage, string].
        ///     Must be left empty unless this Status is an Message
        /// </remarks>
        public object[] Content { get; set; } = null;

        public Identifier Context { get; set; } = null;

        public Identifier From { get; set; } = null;

        public Identifier To { get; set; } = null;

        public StatusType Type { get; }

        /// <remarks>
        ///     Vendor instances shouldn't be accessed directly by design.
        ///     In order to create replies and send outgoing statuses,
        ///     use Vendor.GetType() and OutboundRouter.
        /// </remarks>
        public AbstractVendor Vendor { get; set; } = null;
    }
}