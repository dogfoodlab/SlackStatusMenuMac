using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SlackStatusMenuMac.Slack
{
    [JsonObject]
    public class Message
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("sub_type")]
        public string SubType { get; set; }

        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("user")]
        public string User { get; set; }

        [JsonIgnore]
        public User UserRef { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonConverter(typeof(SlackDateTimeConverter))]
        [JsonProperty("ts")]
        public DateTime Ts { get; set; }

        [JsonProperty("attachments")]
        public List<Attachment> Attachments { get; set; }
    }
}
