using System.Collections.Generic;

namespace ygo_scheduled_tasks.domain.ETL.Tips.Model
{
    public class CardTriviaSection
    {
        public string Name { get; set; }
        public List<string> Trivia{ get; set; } = new List<string>();
    }
}