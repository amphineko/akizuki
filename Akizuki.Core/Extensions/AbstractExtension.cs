using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;

namespace Akizuki.Core.Extensions
{
    public abstract class AbstractExtension : IDisposable
    {
        protected ExtensionState State;

        protected AbstractExtension(ExtensionConfiguration configuration)
        {
            State = ExtensionState.Loaded;
        }

        public virtual void Dispose()
        {
            Debug.Assert(State is ExtensionState.Loaded); // extensions should be disabled before unloading
        }

        public virtual async void SetDisabled()
        {
            Debug.Assert(State is ExtensionState.Enabled);
            State = ExtensionState.Loaded;
            await Task.CompletedTask;
        }

        public virtual async void SetEnabled()
        {
            Debug.Assert(State is ExtensionState.Loaded);
            State = ExtensionState.Enabled;
            await Task.CompletedTask;
        }
    }
}