using System.Collections.Generic;

namespace ygo_scheduled_tasks.domain.Command
{
    public class UpdateArchetypeCardsCommand
    {
        public long ArchetypeId { get; set; }
        public IEnumerable<string> Cards { get; set; }
    }
}