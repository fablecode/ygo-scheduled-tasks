using System;
using System.Collections.Generic;

namespace ygo_scheduled_tasks.core.Model
{
    public class Banlist
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long FormatId { get; set; }
        public DateTime ReleaseDate { get; set; }
        public IEnumerable<BanlistCard> Cards { get; set; }
    }
}