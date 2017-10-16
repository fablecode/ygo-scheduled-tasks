using System.Collections.Generic;
using ygo_scheduled_tasks.domain.ETL;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Model;

namespace ygo_scheduled_tasks.application.ScheduledTasks.CardInformation
{
    public class CardInformationTaskResult
    {
        public List<ArticleBatchTaskResult> ArticleTaskResults { get; set; } = new List<ArticleBatchTaskResult>();

        public List<string> Errors { get; set; }
    }
}