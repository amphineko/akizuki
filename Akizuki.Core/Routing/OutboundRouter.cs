using System;
using System.Collections.Generic;
using System.Diagnostics;
using moe.futa.akizuki.Core.Extensions.Vendors;
using moe.futa.akizuki.Core.Messages;

namespace moe.futa.akizuki.Core.Routing
{
    public class OutboundRouter
    {
        private readonly Random _random = new Random();

        /// <summary>
        ///     Stores each Vendor for specific Identifier
        /// </summary>
        private readonly Dictionary<string, IList<AbstractVendor>> _vendors =
            new Dictionary<string, IList<AbstractVendor>>();

        public void Accept(AbstractStatus status)
        {
            var id = status.From.ToString();
            var vendors = GetVendors(id);
            if (vendors.Count < 1)
                throw new IndexOutOfRangeException();
            vendors[_random.Next(vendors.Count)].Accept(status);
        }

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
            if (_vendors.ContainsKey(identifier))
                _vendors.Add(identifier, new List<AbstractVendor>());
            return _vendors[identifier];
        }
    }
}