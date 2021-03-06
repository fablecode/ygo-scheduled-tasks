﻿using System.Collections.Generic;

namespace ygo_scheduled_tasks.core.Model
{
    public class MonsterCard
    {
        public long Id { get; set; }
        public string CardNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CardLevel { get; set; }
        public int? CardRank { get; set; }
        public int? Atk { get; set; }
        public int? Def { get; set; }
        public string ImageUrl { get; set; }
        public List<SubCategory> SubCategories { get; set; }
    }
}