using System;
using System.Text.Json.Serialization;

namespace sampleharvest.com.Models
{
    public class YoutubeApiResponse
    {
        [JsonPropertyName("urlDownload")]
        public string urlDownload { get; set; }

        [JsonPropertyName("urlStream")]
        public string urlStream { get; set; }

        [JsonPropertyName("keyDownload")]
        public string keyDownload { get; set; }

        [JsonPropertyName("keyStream")]
        public string keyStream { get; set; }

    }
}

