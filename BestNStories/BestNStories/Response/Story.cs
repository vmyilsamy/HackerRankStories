using System;
using System.Text.Json.Serialization;
using BestNStories.Components;

namespace BestNStories.Response
{
    public class Story
    {
        public string Title { get; set; }
        public string Url { get; set; }
        [JsonPropertyName("by")]
        public string PostedBy { get; set; }

        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime Time { get; set; }
        public short Score { get; set; }
        public int CommentCount { get; set; }
    }
}