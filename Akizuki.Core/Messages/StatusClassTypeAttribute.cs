using System;

namespace moe.futa.akizuki.Core.Messages
{
    [AttributeUsage(AttributeTargets.Class)]
    public class StatusClassTypeAttribute : Attribute
    {
        public StatusClassTypeAttribute(StatusType type)
        {
            Type = type;
        }

        public StatusType Type { get; }
    }
}