using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SlackStatusMenuMac.Slack
{
    public class SlackDateTimeConverter : DateTimeConverterBase
    {
		public override void WriteJson(JsonWriter writer,
                                       object value,
                                       JsonSerializer serializer)
		{
            throw new NotImplementedException();
		}

        public override object ReadJson(JsonReader reader,
                                        Type objectType,
                                        object existingValue,
                                        JsonSerializer serializer)
        {
            if (reader.Value == null) {
                return DateTime.MinValue;
            }

            if (string.IsNullOrWhiteSpace(reader.Value.ToString()) == true) {
                return DateTime.MinValue;
            }

            var value = reader.Value;
            var time = (int)decimal.Parse(value.ToString());
            var offset = DateTimeOffset.FromUnixTimeSeconds(time).ToLocalTime();
            return offset.DateTime;
        }

	}
}
