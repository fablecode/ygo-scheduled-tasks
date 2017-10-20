using System.Collections.Generic;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Model;

namespace ygo_scheduled_tasks.application.ScheduledTasks.LatestBanlist
{
    public class BanlistInformationTaskResult
    {
        public ArticleBatchTaskResult ArticleTaskResults { get; set; }

        public List<string> Errors { get; set; }
    }
}