using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using sampleharvest.com.Models;
using sampleharvest.com.Utilities;
using System.Threading.Tasks;
using sampleharvest.com.Repository;

namespace sampleharvest.com.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UrlHelper _urlHelper;
        private readonly ApiRepository _apiRepository;

        public HomeController(ILogger<HomeController> logger, ApiRepository apiRepository)
        {
            _logger = logger;
            _urlHelper = new UrlHelper(); // Initialize the helper
            _apiRepository = apiRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                ViewBag.Error = "Please provide a valid Tiktok, Instagram, or Youtube link.";
                return View();
            }

            string videoType = _urlHelper.GetVideoType(url);

            try
            {
                (string responseJson, object responseObject) = await _apiRepository.GetVideoAsync(url, videoType);

                if (!string.IsNullOrEmpty(responseJson))
                {
                    if (responseObject != null)
                    {
                        if (videoType.Equals("tiktok", StringComparison.OrdinalIgnoreCase))
                        {
                            ViewBag.DownloadLink = ((TiktokApiResponse)responseObject).video[0];
                        }
                        else if (videoType.Equals("instagram", StringComparison.OrdinalIgnoreCase))
                        {
                            ViewBag.DownloadLink = ((InstagramApiResponse)responseObject).media;
                        }
                        else if (videoType.Equals("youtube", StringComparison.OrdinalIgnoreCase))
                        {
                            ViewBag.DownloadLink = ((YoutubeApiResponse)responseObject).urlStream;
                        }
                    }
                    else
                    {
                        ViewBag.Error = $"An error occurred while fetching the {videoType} video details.";
                    }
                }
                else
                {
                    ViewBag.Error = $"An error occurred while fetching the {videoType} video details.";
                }
            }
            catch (HttpRequestException ex) when (videoType.Equals("tiktok", StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogError(ex, "Error while calling Tiktok API");
                ViewBag.Error = "An error occurred while calling the Tiktok API.";
            }
            catch (HttpRequestException ex) when (videoType.Equals("instagram", StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogError(ex, "Error while calling Instagram API");
                ViewBag.Error = "An error occurred while calling the Instagram API.";
            }
            catch (HttpRequestException ex) when (videoType.Equals("youtube", StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogError(ex, "Error while calling YouTube API");
                ViewBag.Error = "An error occurred while calling the YouTube API.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while processing the request.");
                ViewBag.Error = "An error occurred while processing your request.";
            }

            return View();
        }
    }
}
