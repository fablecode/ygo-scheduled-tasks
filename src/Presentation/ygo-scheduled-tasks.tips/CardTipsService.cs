using System;

namespace ygo_scheduled_tasks.tips
{
    public class CardTipsService
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