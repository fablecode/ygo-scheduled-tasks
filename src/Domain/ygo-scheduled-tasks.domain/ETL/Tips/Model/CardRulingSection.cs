﻿using System.Collections.Generic;

namespace ygo_scheduled_tasks.domain.ETL.Tips.Model
{
    public class CardRulingSection
    {
        public string Name { get; set; }
        public List<string> Tips { get; set; } = new List<string>();
    }
}