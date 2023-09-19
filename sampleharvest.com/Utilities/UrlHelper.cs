using System;
using System.Text.RegularExpressions;

namespace sampleharvest.com.Utilities
{
    public class UrlHelper
    {

        public string GetVideoType(string url)
        {
            if (url.Contains("instagram"))
            {
                return "instagram";
            }
            else if (url.Contains("tiktok"))
            {
                return "tiktok";
            }
            else if (url.Contains("youtube"))
            {
                return "youtube";
            }
            else
            {
                return "unknown";
            }
        }

        public string GetYoutubeIdFromLink(string youtubeLink)
        {
        // Assuming youtubeLink is the input link from your view
        // You can use regular expressions to extract the video ID

        // YouTube URL formats:
        // https://www.youtube.com/watch?v=VIDEO_ID
        // https://youtu.be/VIDEO_ID

        string videoId = null;

        // Regular expression pattern to match YouTube video IDs
        // This pattern matches both the full URL and the short URL format
        string pattern = @"(?:youtube\.com\/watch\?v=|youtu.be\/)([\w-]+)";

        var match = Regex.Match(youtubeLink, pattern);
        if (match.Success)
        {
            videoId = match.Groups[1].Value;
        }

        return videoId;


        }
    }
}

