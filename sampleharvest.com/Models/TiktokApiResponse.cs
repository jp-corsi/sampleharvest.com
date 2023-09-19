using System;
using System.Text.Json.Serialization;

namespace sampleharvest.com.Models
{
    public class TiktokApiResponse
    {
        [JsonPropertyName("video")]
        public List<string> video { get; set; }

        [JsonPropertyName("music")]
        public List<string> music { get; set; }

        [JsonPropertyName("cover")]
        public List<string> cover { get; set; }

        [JsonPropertyName("originvideo")]
        public List<string> originvideo { get; set; }

    }

}

