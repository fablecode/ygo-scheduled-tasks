using System;
using System.Collections.Generic;

namespace ygo_scheduled_tasks.application.Command
{
    public class AddCardCommand
    {
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CardLevel { get; set; }
        public int? CardRank { get; set; }
        public int? Atk { get; set; }
        public int? Def { get; set; }
        public Uri ImageUrl { get; set; }
        public int? AttributeId { get; set; }
        public List<int> SubCategoryIds { get; set; }
        public List<int> TypeIds { get; set; }
        public List<int> LinkArrowIds { get; set; }
    }
}