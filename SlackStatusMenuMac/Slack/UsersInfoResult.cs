using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SlackStatusMenuMac.Slack
{
    [JsonObject]
    public class UsersInfoResult : AbstractResult
    {
        [JsonProperty("user")]
        public User User { get; set; }
    }
}
