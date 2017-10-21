using System;

namespace ygo_scheduled_tasks.domain.Command
{
    public class UpdateBanlistCommand
    {
        public long Id { get; set; }
        public long FormatId { get; set; }
        public string Name { get; set; }
        public DateTime? ReleaseDate { get; set; }
    }
}