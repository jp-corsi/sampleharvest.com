using System;
using System.Net.Http;
using sampleharvest.com.Utilities;
using System.Threading.Tasks;

namespace sampleharvest.com.Models
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

        public async Task<string> GetVideoAsync(string videoId, string videoType)
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
                    return null; 
            }
        }

        public async Task<string> GetTikTokVideoAsync(string videoUrl)
        {
            var requestUri = new Uri($"https://tiktok-downloader-download-tiktok-videos-without-watermark.p.rapidapi.com/vid/index?url={videoUrl}");
            return await SendRequestAsync(requestUri);
        }

        public async Task<string> GetInstagramVideoAsync(string videoUrl)
        {
            var requestUri = new Uri($"https://instagram-downloader-download-instagram-videos-stories.p.rapidapi.com/index?url={Uri.EscapeDataString(videoUrl)}");
            return await SendRequestAsync(requestUri);
        }

        public async Task<string> GetYouTubeVideoAsync(string videoUrl)
        {
            string videoId = _urlHelper.GetYoutubeIdFromLink(videoUrl);

            if (!string.IsNullOrEmpty(videoId))
            {
                var requestUri = new Uri($"https://youtube-mp3-download1.p.rapidapi.com/dl?id={videoId}");
                return await SendRequestAsync(requestUri);
            }
            else
            {
                // Handle invalid YouTube URL
                return null;
            }
        }

        private async Task<string> SendRequestAsync(Uri requestUri)
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
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    // Handle error as needed
                    return null;
                }
            }
        }
    }
}
