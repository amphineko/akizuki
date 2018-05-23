using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using moe.futa.akizuki.Core.Extensions.Handlers;
using moe.futa.akizuki.Core.Extensions.Hooks;
using moe.futa.akizuki.Core.Messages;

namespace moe.futa.akizuki.Core.Routing
{
    internal class InboundRouter
    {
        private readonly List<AbstractPreroutingHook> _preroutingHooks = new List<AbstractPreroutingHook>();
        private readonly List<AbstractHandler> _handlers = new List<AbstractHandler>();

        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();

        public void AddPreroutingHook(AbstractPreroutingHook hook)
        {
            try
            {
                _lock.EnterWriteLock();
                _preroutingHooks.Add(hook);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public void AddStatusHandler(AbstractHandler handler)
        {
            try
            {
                _lock.EnterWriteLock();
                _handlers.Add(handler);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public async void Execute(AbstractStatus rawStatus)
        {
            var statuses = new List<AbstractStatus>() {rawStatus};

            try
            {
                _lock.EnterReadLock();

                foreach (var hook in _preroutingHooks)
                    await hook.Accept(statuses);

                foreach (var status in statuses)
                    foreach (var handler in _handlers)
                        if (await handler.Accept(status))
                            break;
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
    }
}