using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using sampleharvest.com.Models;
using sampleharvest.com.Utilities;

namespace sampleharvest.com.Repository
{
    public class ApiRepository
    {
        private readonly HttpClient _httpClient;
        private readonly UrlHelper _urlHelper;

        public ApiRepository()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", "c12758149bmsh95bf845b317bc1ep161d81jsn2a02f1f9a9d5");
            _urlHelper = new UrlHelper();
        }

        public async Task<(string responseJson, object responseObject)> GetVideoAsync(string videoId, string videoType)
        {
            switch (videoType.ToLower())
            {
                case "tiktok":
                    return await GetTikTokVideoAsync(videoId);
                case "instagram":
                    return await GetInstagramVideoAsync(videoId);
                case "youtube":
                    return await GetYouTubeVideoAsync(videoId);
                default:
                    return (null, null); // Handle unknown video type as needed
            }
        }

        public async Task<(string responseJson, object responseObject)> GetTikTokVideoAsync(string videoUrl)
        {
            var requestUri = new Uri($"https://tiktok-downloader-download-tiktok-videos-without-watermark.p.rapidapi.com/vid/index?url={videoUrl}");
            return await SendRequestAsync(requestUri, typeof(TiktokApiResponse));
        }

        public async Task<(string responseJson, object responseObject)> GetInstagramVideoAsync(string videoUrl)
        {
            var requestUri = new Uri($"https://instagram-downloader-download-instagram-videos-stories.p.rapidapi.com/index?url={Uri.EscapeDataString(videoUrl)}");
            return await SendRequestAsync(requestUri, typeof(InstagramApiResponse));
        }

        public async Task<(string responseJson, object responseObject)> GetYouTubeVideoAsync(string videoUrl)
        {
            var requestUri = new Uri($"https://ytconvert2.p.rapidapi.com/youtube/url/generate?Title=name_music&Url={Uri.EscapeDataString(videoUrl)}&Type=MP3");
            return await SendRequestAsync(requestUri, typeof(YoutubeApiResponse));

        }

        private async Task<(string responseJson, object responseObject)> SendRequestAsync(Uri requestUri, Type responseType)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = requestUri,
            };
            request.Headers.Add("X-RapidAPI-Host", requestUri.Host);

            using (var response = await _httpClient.SendAsync(request))
            {
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonSerializer.Deserialize(responseJson, responseType);
                    return (responseJson, responseObject);
                }
                else
                {
                    // Handle error as needed
                    return (null, null);
                }
            }
        }
    }
}
