using System.Collections.Generic;

namespace ygo_scheduled_tasks.core.Model
{
    public class Card
    {
        public long Id { get; set; }
        public string CardNumber { get; set; }
        public string ImageUrl { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int? CardLevel { get; set; }
        public int? CardRank { get; set; }
        public int? Atk { get; set; }
        public int? Def { get; set; }
        public int? Link { get; set; }
        public List<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
        public List<Type> Types { get; set; } = new List<Type>();
        public Attribute Attribute { get; set; }
        public List<LinkArrow> LinkArrows { get; set; } = new List<LinkArrow>();
    }
}