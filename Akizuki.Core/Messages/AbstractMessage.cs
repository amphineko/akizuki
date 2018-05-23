namespace moe.futa.akizuki.Core.Messages
{
    public abstract class AbstractMessage : AbstractStatus
    {
        public object Content { get; set; } = null;

        public abstract void SetString(string newValue);

        public abstract override string ToString();
    }
}