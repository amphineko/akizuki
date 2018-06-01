using System;
using System.Collections.Generic;
using System.Diagnostics;
using Akizuki.Core.Extensions.Vendors;
using Akizuki.Core.Messages;

namespace Akizuki.Core.Routing
{
    public sealed class OutboundRouter
    {
        private readonly Random _random = new Random();

        /// <summary>
        ///     Stores each Vendor for specific Identifier
        /// </summary>
        private readonly Dictionary<string, IList<AbstractVendor>> _vendors =
            new Dictionary<string, IList<AbstractVendor>>();

        /// <summary>
        ///     Accepts an outgoing status with Source-based Routing
        ///     Vendors are choosen by Status.From
        /// </summary>
        /// <param name="status"></param>
        public void Accept(AbstractStatus status)
        {
            var id = status.From.ToString();
            var vendors = GetVendors(id);
            if (vendors.Count < 1)
                throw new IndexOutOfRangeException();
            vendors[_random.Next(vendors.Count)].Accept(status);
        }

        /// <summary>
        ///     Registers a Identifier for Source-based Routing
        ///     Statuses sent from this Identifier will be accepted by this vendor
        /// </summary>
        public void Register(Identifier identifier, AbstractVendor vendor)
        {
            GetVendors(identifier.ToString()).Add(vendor);
        }

        public void Unregister(Identifier identifier, AbstractVendor vendor)
        {
            Debug.Assert(
                GetVendors(identifier.ToString()).Remove(vendor)
            );
        }

        private IList<AbstractVendor> GetVendors(string identifier)
        {
            if (!_vendors.ContainsKey(identifier))
                _vendors.Add(identifier, new List<AbstractVendor>());
            return _vendors[identifier];
        }
    }
}