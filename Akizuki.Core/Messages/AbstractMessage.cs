namespace moe.futa.akizuki.Core.Messages
{
    public abstract class AbstractMessage : AbstractStatus
    {
        /// <remarks>
        ///     Different media types are stored in separated objects,
        ///     e.g. one message contains two parts of text sections and one image [string, VendorImage, string]
        /// </remarks>
        public object[] Content { get; set; } = null;

        public abstract void SetString(string newValue);

        public abstract override string ToString();
    }
}