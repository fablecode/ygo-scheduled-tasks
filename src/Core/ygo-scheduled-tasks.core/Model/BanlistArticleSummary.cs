using System;
using ygo_scheduled_tasks.core.Enums;

namespace ygo_scheduled_tasks.core.Model
{
    public class BanlistArticleSummary
    {
        public int ArticleId { get; set; }
        public BanlistType BanlistType { get; set; }
        public DateTime StartDate { get; set; }
    }
}