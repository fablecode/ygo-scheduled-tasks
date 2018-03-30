using System.Threading.Tasks;

namespace ygo_scheduled_tasks.domain.WebPage.Archetypes
{
    public interface IArchetypeThumbnail
    {
        Task<string> FromArticleId(int articleId);
        string FromWebPage(string url);
    }
}