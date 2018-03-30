using System.Linq;
using System.Threading.Tasks;
using wikia.Api;
using ygo_scheduled_tasks.core.WebPage;
using ygo_scheduled_tasks.domain.Helpers;

namespace ygo_scheduled_tasks.domain.WebPage.Archetypes
{
    public class ArchetypeThumbnail : IArchetypeThumbnail
    {
        private readonly IWikiArticle _wikiArticle;
        private readonly IHtmlWebPage _htmlWebPage;
        private readonly IConfig _config;

        public ArchetypeThumbnail(IWikiArticle wikiArticle, IHtmlWebPage htmlWebPage, IConfig config)
        {
            _wikiArticle = wikiArticle;
            _htmlWebPage = htmlWebPage;
            _config = config;
        }

        public async Task<string> FromArticleId(int articleId)
        {
            var profileDetailsList = await _wikiArticle.Details(articleId);
            var profileDetails = profileDetailsList.Items.First();

            return ArchetypeHelper.ExtractThumbnailUrl(profileDetails.Value.Thumbnail);
        }

        public string FromWebPage(string url)
        {
            var archetypeWebPage = _htmlWebPage.Load(_config.WikiaDomainUrl + url);

            var srcElement = archetypeWebPage.DocumentNode.SelectSingleNode("//img[@class='pi-image-thumbnail']");

            var srcAttribute = srcElement?.Attributes?["src"].Value;

            return srcAttribute != null ? ArchetypeHelper.ExtractThumbnailUrl(srcAttribute) : null;
        }
    }
}