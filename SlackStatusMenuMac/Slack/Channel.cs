using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SlackStatusMenuMac.Slack
{
    [JsonObject]
    public class Channel
    {
        [JsonProperty("id")]
        public string ID { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("unread_count")]
        public int UnreadCount { get; set; }

        [JsonProperty("unread_count_display")]
        public int UnreadCountDisplay { get; set; }
    }
}
