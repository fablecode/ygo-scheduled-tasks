using System.Collections.Generic;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Model;

namespace ygo_scheduled_tasks.application.ScheduledTasks.ArchetypeInformation
{
    public class ArchetypeInformationTaskResult
    {
        public IEnumerable<ArticleBatchTaskResult> ArticleTaskResults { get; set; }

        public List<string> Errors { get; set; }

    }
}