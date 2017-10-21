using System.Collections.Generic;
using ygo_scheduled_tasks.core.Model;

namespace ygo_scheduled_tasks.domain.Command
{
    public class UpdateBanlistCardsCommand
    {
        public long BanlistId { get; set; }
        public IEnumerable<BanlistCard> BanlistCards { get; set; }
    }
}