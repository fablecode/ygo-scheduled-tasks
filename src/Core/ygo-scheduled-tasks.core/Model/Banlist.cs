using System;
using System.Collections.Generic;
using ygo_scheduled_tasks.core.Enums;

namespace ygo_scheduled_tasks.core.Model
{
    public class Banlist
    {
        public BanlistType BanlistType { get; set; }
        public DateTime StartDate { get; set; }
        public List<BanlistSection> Sections { get; set; } = new List<BanlistSection>();
    }
}