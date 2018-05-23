using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace moe.futa.akizuki.Core.Extensions
{
    public abstract class AbstractExtension : IDisposable
    {
        private ExtensionState _state;

        protected AbstractExtension()
        {
            _state = ExtensionState.Loaded;
        }

        public virtual void SetDisabled()
        {
            Debug.Assert(_state is ExtensionState.Enabled);
            _state = ExtensionState.Loaded;
        }

        public virtual void SetEnabled()
        {
            Debug.Assert(_state is ExtensionState.Loaded);
            _state = ExtensionState.Enabled;
        }

        public virtual void Dispose()
        {
            Debug.Assert(_state is ExtensionState.Loaded);      // extensions should be disabled before unloading
        }
    }
}
