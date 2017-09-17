namespace ygo_scheduled_tasks.domain.Model
{
    public sealed class YugiohCard
    {
        public string Name { get; set; }
        public string Types { get; set; }
        public string CardType { get; set; }
        public string Attribute { get; set; }
        public int Level { get; set; }
        public int Rank { get; set; }
        public long PendulumScale { get; set; }
        public string AtkDef { get; set; }
        public string AtkLink { get; set; }
        public string CardNumber { get; set; }
        public string Materials { get; set; }
        public string CardEffectTypes { get; set; }
        public string Property { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string LinkArrows { get; set; }
    }
}