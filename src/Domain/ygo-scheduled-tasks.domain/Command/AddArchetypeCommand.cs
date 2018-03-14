using System.Collections.Generic;

namespace ygo_scheduled_tasks.domain.Command
{
    public class AddArchetypeCommand
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public int ArchetypeNumber { get; set; }
        public IEnumerable<string> Cards { get; set; }
    }

    public class UpdateArchetypeCommand
    {
        public string Name { get; set; }
        public string Alias { get; set; }
        public int ArchetypeNumber { get; set; }
        public IEnumerable<string> Cards { get; set; }
    }

}