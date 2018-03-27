using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SlackStatusMenuMac.Slack
{
    [JsonObject]
    public class Profile
    {
        [JsonProperty("avatar_hash")]
        public string AvatarHash { get; set; }

        [JsonProperty("status_text")]
        public string StatusText { get; set; }

        [JsonProperty("status_emoji")]
        public string StatusEmoji { get; set; }

        [JsonProperty("real_name")]
        public string RealName { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("real_name_normalized")]
        public string RealNameNormalized { get; set; }

        [JsonProperty("display_name_normalized")]
        public string DisplayNameNormalized { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("image_24")]
        public string Image24 { get; set; }

        [JsonProperty("image_32")]
        public string Image32 { get; set; }

        [JsonProperty("image_48")]
        public string Image48 { get; set; }

        [JsonProperty("image_72")]
        public string Image72 { get; set; }

        [JsonProperty("image_192")]
        public string Image192 { get; set; }

        [JsonProperty("image_512")]
        public string Image512 { get; set; }

        [JsonProperty("team")]
        public string team { get; set; }
    }
}
