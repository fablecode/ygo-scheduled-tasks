using HtmlAgilityPack;
using ygo_scheduled_tasks.domain.WebPage;

namespace ygo_scheduled_tasks.domain.services.WebPage
{
    public class HtmlWebPage : IHtmlWebPage
    {
        public HtmlDocument Load(string webPageUrl)
        {
            var htmlWeb = new HtmlWeb
            {
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.135 Safari/537.36 Edge/12.246"
            };

            htmlWeb.PreRequest += request =>
            {
                request.CookieContainer = new System.Net.CookieContainer();
                return true;
            };

            return htmlWeb.Load(webPageUrl);
        }
    }
}