using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace moe.futa.akizuki.Core.Extensions.Handlers
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class AsyncHandler : Attribute
    {
        public bool IsAsync { get; set; }

        public AsyncHandler(bool isAsync)
        {
            IsAsync = isAsync;
        }
    }
}