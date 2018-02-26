using System.Collections.Generic;
using System.Threading.Tasks;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Model;

namespace ygo_scheduled_tasks.domain.ETL.ArticleList.Processor
{
    public interface IArticleCategoryProcessor
    {
        Task<IEnumerable<ArticleBatchTaskResult>> Process(IEnumerable<string> categories, int pageSize);
        Task<ArticleBatchTaskResult> Process(string category, int pageSize);
    }
}