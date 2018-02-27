using System.Collections.Generic;

namespace ygo_scheduled_tasks.core.Model
{
    public class YugiohArchetype
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public int ArchetypeNumber { get; set; }
        public IEnumerable<string> Cards { get; set; }
    }
}