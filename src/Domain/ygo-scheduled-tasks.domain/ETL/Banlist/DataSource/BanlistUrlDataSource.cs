using System.Collections.Generic;
using System.Text.RegularExpressions;
using ygo_scheduled_tasks.core.Enums;
using ygo_scheduled_tasks.core.WebPage;
using ygo_scheduled_tasks.domain.WebPage.Banlists;

namespace ygo_scheduled_tasks.domain.ETL.Banlist.DataSource
{
    public class BanlistUrlDataSource : IBanlistUrlDataSource
    {
        private readonly IBanlistWebPage _banlistWebPage;
        private readonly IHtmlWebPage _htmlWebPage;

        public BanlistUrlDataSource(IBanlistWebPage banlistWebPage, IHtmlWebPage htmlWebPage)
        {
            _banlistWebPage = banlistWebPage;
            _htmlWebPage = htmlWebPage;
        }

        public IDictionary<int, List<int>> GetBanlists(BanlistType banlistType, string banlistUrl)
        {
            var articleIdsList = new Dictionary<int, List<int>>();

            var banlistUrlsByYear = _banlistWebPage.GetBanlistUrlList(banlistType, banlistUrl);

            foreach (var banlist in banlistUrlsByYear)
            {
                var banlistYear = int.Parse(banlist.Key);
                var articleIds = new List<int>();

                foreach (var url in banlist.Value)
                {
                    var banlistPageHtml = _htmlWebPage.Load(url).DocumentNode.InnerHtml;

                    Regex regex = new Regex("wgArticleId=([^,]*),");
                    Match match = regex.Match(banlistPageHtml);

                    var wgArticleId = int.Parse(match.Groups[1].Value);

                    articleIds.Add(wgArticleId);
                }

                articleIdsList.Add(banlistYear, articleIds);
            }

            return articleIdsList;
        }
    }
}