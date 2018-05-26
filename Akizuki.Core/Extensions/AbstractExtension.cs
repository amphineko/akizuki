using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace moe.futa.akizuki.Core.Extensions
{
    public abstract class AbstractExtension<TConfiguration> : IDisposable where TConfiguration : ExtensionConfiguration
    {
        private readonly TConfiguration _configuration;

        protected ExtensionState State;

        protected AbstractExtension(TConfiguration configuration)
        {
            _configuration = configuration;
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

    public abstract class AbstractExtension : AbstractExtension<ExtensionConfiguration>
    {
        protected AbstractExtension(ExtensionConfiguration configuration) : base(configuration)
        {
        }
    }
}