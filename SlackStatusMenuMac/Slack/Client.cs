using System;
using System.Net;
using Newtonsoft.Json;

namespace SlackStatusMenuMac.Slack
{
    public class Client
    {
        private const string API = "https://slack.com/api/";
        private string token = string.Empty;


        private Client() { }


        public Client(string token) : base()
        {
            this.token = token;
        }


        private WebClient build()
        {
            var client = new WebClient();
            client.BaseAddress = API;
            client.QueryString.Add("token", this.token);
            return client;
        }


        public ChannelsListResult ChannelsList(
            bool exclude_archived = false,
            bool exclude_members = false)
        {
            var client = this.build();
            client.QueryString.Add("exclude_archived", exclude_archived.ToString());
            client.QueryString.Add("exclude_members", exclude_members.ToString());

            var response = client.DownloadString("channels.list");
            var list = JsonConvert.DeserializeObject<ChannelsListResult>(response);

            return list;
        }


        public ChannelsInfoResult ChannelsInfo(
            string channel,
            bool include_locale = false)
        {
            var client = this.build();
            client.QueryString.Add("channel", channel);
            client.QueryString.Add("include_locale", include_locale.ToString());

            var response = client.DownloadString("channels.info");
            var info = JsonConvert.DeserializeObject<ChannelsInfoResult>(response);

            return info;
        }


        public GroupsListResult GroupsList(
            bool exclude_archived = false,
            bool exclude_members = false)
        {
            var client = this.build();
            client.QueryString.Add("exclude_archived", exclude_archived.ToString());
            client.QueryString.Add("exclude_members", exclude_members.ToString());

            var response = client.DownloadString("groups.list");
            var list = JsonConvert.DeserializeObject<GroupsListResult>(response);

            return list;
        }


        public GroupsInfoResult GroupsInfo(
            string channel,
            bool include_locale = false)
        {
            var client = this.build();
            client.QueryString.Add("channel", channel);
            client.QueryString.Add("include_locale", include_locale.ToString());

            var response = client.DownloadString("groups.info");
            var info = JsonConvert.DeserializeObject<GroupsInfoResult>(response);

            return info;
        }


    }
}
