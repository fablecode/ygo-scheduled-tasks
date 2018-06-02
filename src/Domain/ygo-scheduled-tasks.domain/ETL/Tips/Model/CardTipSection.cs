using System.Collections.Generic;

namespace ygo_scheduled_tasks.domain.ETL.Tips.Model
{
    public class CardTipSection
    {
        public string Name { get; set; }
        public List<string> Tips { get; set; } = new List<string>();
    }
}