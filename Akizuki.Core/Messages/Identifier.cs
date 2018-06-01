using System.Text.RegularExpressions;

namespace Akizuki.Core.Messages
{
    public sealed class Identifier
    {
        /// <summary>
        ///     Represents a brand/platform/product,
        ///     e.g. Vendor.TelegramPoller and Vendor.TelegramWebhook can
        ///     both provides community "telegram"
        /// </summary>
        public string Community;

        /// <summary>
        ///     Represents a internal identifier inside a community
        /// </summary>
        public string Id;

        /// <summary>
        ///     Represents the type of identifier
        ///     chat:   PtMP, e.g. group or channel
        ///     user:   PtP or one user
        /// </summary>
        public string Type;

        public Identifier(string identifier)
        {
            var split = Regex.Split(identifier, @"^(.*)\:(.*)\/(.*)$");
            Type = split[1];
            Community = split[2];
            Id = split[3];
        }

        public Identifier(string type, string community, string communityId)
        {
            Type = type;
            Community = community;
            Id = communityId;
        }

        public override string ToString()
        {
            return $"{Type}:{Community}/{Id}";
        }
    }
}