using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Akizuki.Core.Extensions.Handlers;
using Akizuki.Core.Extensions.Hooks;
using Akizuki.Core.Messages;
using NLog;

namespace Akizuki.Core.Routing
{
    public sealed class InboundRouter
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly List<AbstractHandler> _handlers = new List<AbstractHandler>();

        private readonly ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
        private readonly List<AbstractPreroutingHook> _preroutingHooks = new List<AbstractPreroutingHook>();

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
            var statuses = new List<AbstractStatus> {rawStatus};

            try
            {
                _lock.EnterReadLock();

                foreach (var hook in _preroutingHooks)
                    await hook.Accept(statuses);

                foreach (var status in statuses)
                foreach (var handler in _handlers)
                    try
                    {
                        if (handler.GetType().GetCustomAttribute<AsyncHandler>().IsAsync
                            ? await handler.AcceptAsync(status)
                            : handler.Accept(status))
                            break;
                    }
                    catch (Exception e)
                    {
                        Logger.Warn($"Unhandled Handler exception: {e}");
                    }
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
    }
}