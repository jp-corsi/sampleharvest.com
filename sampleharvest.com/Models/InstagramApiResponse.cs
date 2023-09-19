using System.Text.Json.Serialization;

namespace sampleharvest.com.Models
{
    public class InstagramApiResponse
    {
        [JsonPropertyName("media")]
        public string media { get; set; }

        [JsonPropertyName("title")]
        public string title { get; set; }

        [JsonPropertyName("Type")]
        public string Type { get; set; }

        [JsonPropertyName("thumbnail")]
        public string thumbnail { get; set; }

        [JsonPropertyName("API")]
        public string API { get; set; }
    }
}
