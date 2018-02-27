namespace ygo_scheduled_tasks.core.Model
{
    public class ArchetypeCard
    {
        public long ArchetypeId { get; set; }
        public long CardId { get; set; }

        public Archetype Archetype { get; set; }
        public Card Card { get; set; }
    }
}