using System;
using System.Text.RegularExpressions;

namespace ygo_scheduled_tasks.domain.Helpers
{
    public static class ArchetypeHelper
    {
        public static string ExtractArchetypeName(string title)
        {
            if (!string.IsNullOrWhiteSpace(title))
            {
                var match = Regex.Match(title, "(?<=\\\")(.*?)(?=\\\")");

                return match.Success ? match.Value : null;
            }

            return null;
        }

        public static string ExtractThumbnailUrl(string thumbnailUrl)
        {
            return new Regex(@"(?<Protocol>\w+):(.+?).(jpg|jpeg|tif|tiff|png|gif|bmp|wmf)").Match(thumbnailUrl).Value;
        }
    }
}