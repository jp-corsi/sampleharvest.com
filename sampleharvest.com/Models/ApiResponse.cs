using System;
using System.Text.Json.Serialization;

namespace sampleharvest.com.Models
{
    public class ApiResponse
    {
        [JsonPropertyName("link")]
        public string Link { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("progress")]
        public int Progress { get; set; }

        [JsonPropertyName("duration")]
        public double Duration { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("msg")]
        public string Msg { get; set; }
    }
}

