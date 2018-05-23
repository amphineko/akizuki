using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using moe.futa.akizuki.Core.Extensions.Vendors;

namespace moe.futa.akizuki.Core.Messages
{
    public abstract class AbstractStatus
    {
        public string Context { get; set; } = null;

        public string Sender { get; set; } = null;

        public AbstractVendor Vendor { get; set; } = null;
    }
}