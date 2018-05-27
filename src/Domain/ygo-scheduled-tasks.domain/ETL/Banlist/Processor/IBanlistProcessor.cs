using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Enums;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Model;

namespace ygo_scheduled_tasks.domain.ETL.Banlist.Processor
{
    public interface IBanlistProcessor
    {
        Task<ArticleBatchTaskResult> Process(BanlistType banlistType);
    }
}