using System.Collections.Generic;
using ygo_scheduled_tasks.core.Enums;

namespace ygo_scheduled_tasks.domain.ETL.Banlist.DataSource
{
    public interface IBanlistUrlDataSource
    {
        IDictionary<int, List<int>> GetBanlists(BanlistType banlistType, string banlistUrl);
    }
}