using System.Collections.Generic;

namespace ygo_scheduled_tasks.domain.Command
{
    public class UpdateArchetypeCommand
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public string Url { get; set; }
        public IEnumerable<string> Cards { get; set; }
    }
}