using System;

namespace ygo_scheduled_tasks.rulings
{
    public class CardRulingsService
    {
        public void OnStart()
        {
            Console.WriteLine("On Start");
        }

        public void OnStop()
        {
            Console.WriteLine("On Stop");
        }
    }
}