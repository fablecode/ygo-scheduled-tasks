using System.Text.RegularExpressions;

namespace ygo_scheduled_tasks.domain.Helpers
{
    public static class StringHelpers
    {
        public static string RemoveBetween(string text, char begin, char end)
        {
            var regex = new Regex(string.Format("\\{0}.*?\\{1}", begin, end));
            return regex.Replace(text, string.Empty).Trim();
        }
    }
}