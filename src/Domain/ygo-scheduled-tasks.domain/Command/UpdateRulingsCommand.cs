using System.Collections.Generic;
using ygo_scheduled_tasks.domain.ETL.Tips.Model;

namespace ygo_scheduled_tasks.domain.Command
{
    public class UpdateRulingsCommand
    {
        public long CardId { get; set; }
        public List<CardRulingSection> Rulings { get; set; }
    }
}