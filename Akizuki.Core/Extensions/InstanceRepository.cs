using System.Collections.Generic;
using System.Threading;
using System;

namespace Akizuki.Core.Extensions
{
    public class InstanceRepository
    {
        ICollection<AbstractExtension> _instances = new List<AbstractExtension>();
        ReaderWriterLockSlim _instancesLock = new ReaderWriterLockSlim();

        public void AddInstance(AbstractExtension instance)
        {
            try
            {
                _instancesLock.EnterWriteLock();
                _instances.Add(instance);
            }
            finally
            {
                _instancesLock.ExitWriteLock();
            }
        }

        public void DisableInstances()
        {
            throw new NotImplementedException();
        }

        public void EnableInstances()
        {
            try
            {
                _instancesLock.EnterReadLock();
                foreach (var i in _instances)
                    i.SetEnabled();
            }
            finally
            {
                _instancesLock.ExitReadLock();
            }
        }

        public void UnloadInstances()
        {
            try
            {
                _instancesLock.EnterWriteLock();
                foreach (var i in _instances)
                {
                    i.SetDisabled();
                    _instances.Remove(i);
                }
            }
            finally
            {
                _instancesLock.ExitWriteLock();
            }
        }
    }
}