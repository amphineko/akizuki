using System;

namespace moe.futa.akizuki.Core.Extensions.Handlers
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class AsyncHandler : Attribute
    {
        public AsyncHandler(bool isAsync)
        {
            IsAsync = isAsync;
        }

        public bool IsAsync { get; set; }
    }
}