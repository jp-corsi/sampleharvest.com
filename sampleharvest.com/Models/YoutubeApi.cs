using System;
namespace sampleharvest.com.Models
{
    public class YoutubeApi
    {

        private readonly HttpClient _httpClient;


        public YoutubeApi()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", "c12758149bmsh95bf845b317bc1ep161d81jsn2a02f1f9a9d5");
            _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", "youtube-mp36.p.rapidapi.com");
        }



        public async Task<string> GetVideoAsync(string videoId)
        {
            var requestUri = new Uri($"https://youtube-mp36.p.rapidapi.com/dl?id={videoId}");
            var response = await _httpClient.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            else
            {
                return null; // Or handle error as needed

            }
        }
    }
}

