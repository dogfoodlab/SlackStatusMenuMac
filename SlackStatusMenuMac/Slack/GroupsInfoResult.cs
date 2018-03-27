﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SlackStatusMenuMac.Slack
{
    [JsonObject]
    public class GroupsInfoResult : AbstractResult
    {
        [JsonProperty("group")]
        public Group Group { get; set; }
    }
}
