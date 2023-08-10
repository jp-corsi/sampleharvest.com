using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using sampleharvest.com.Models;
using sampleharvest.com.Utilities;

namespace sampleharvest.com.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly YoutubeApi _youtubeApi;
    private readonly UrlConversionHelper _urlConversionHelper;

    public HomeController(ILogger<HomeController> logger, YoutubeApi youtubeApi)
    {
        _logger = logger;
        _youtubeApi = youtubeApi;
        _urlConversionHelper = new UrlConversionHelper(); // Initialize the helper
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
    public async Task<IActionResult> Index(string youtubeLink)
    {
        if (string.IsNullOrEmpty(youtubeLink))
        {
            ViewBag.Error = "Please provide a valid YouTube link.";
            return View();
        }

        string videoId = _urlConversionHelper.GetVideoIdFromLink(youtubeLink);

        if (!string.IsNullOrEmpty(videoId))
        {
            try
            {
                string responseJson = await _youtubeApi.GetVideoAsync(videoId);

                if (!string.IsNullOrEmpty(responseJson))
                {
                    var responseObject = JsonSerializer.Deserialize<ApiResponse>(responseJson);

                    if (responseObject != null && responseObject.Status == "ok")
                    {
                        ViewBag.DownloadLink = responseObject.Link;
                        ViewBag.Title = responseObject.Title;
                    }
                    else
                    {
                        ViewBag.Error = "An error occurred while fetching the video details.";
                    }
                }
                else
                {
                    ViewBag.Error = "An error occurred while fetching the video details.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while calling YoutubeApi.GetVideoAsync");
                ViewBag.Error = "An error occurred while processing your request.";
            }
        }
        else
        {
            ViewBag.Error = "Invalid YouTube link.";
        }

        return View();
    }
}

