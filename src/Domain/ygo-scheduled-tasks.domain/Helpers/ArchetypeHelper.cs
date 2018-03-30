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
    }
}