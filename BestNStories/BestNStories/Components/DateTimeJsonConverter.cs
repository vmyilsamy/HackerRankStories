using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BestNStories.Components
{
    public class DateTimeJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var ticks = reader.GetInt64();

            var date = new DateTime(1970, 1, 1);

            date = date.AddSeconds(ticks);

            return date;
        }

        public override void Write(Utf8JsonWriter writer, DateTime dateTimeValue, JsonSerializerOptions options)
        {
            writer.WriteStringValue(dateTimeValue);
        }
    }
}