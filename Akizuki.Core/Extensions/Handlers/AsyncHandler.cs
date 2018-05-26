using System;

namespace moe.futa.akizuki.Core.Extensions.Handlers
{
    /// <summary>
    ///     Indicates if a handler either sync or async handler
    /// </summary>
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