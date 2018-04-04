﻿using System.Collections.Generic;

namespace ygo_scheduled_tasks.domain.Command
{
    public class AddArchetypeCommand
    {
        public long ArchetypeNumber { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string ProfileUrl { get; set; }
        public IEnumerable<string> Cards { get; set; }
    }
}