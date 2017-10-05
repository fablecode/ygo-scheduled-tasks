namespace ygo_scheduled_tasks.core.Model
{
    public class TrapCard
    {
        public long Id { get; set; }
        public string CardNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public SubCategory SubCategory { get; set; }
    }
}