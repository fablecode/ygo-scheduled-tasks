using System;

namespace ygo_scheduled_tasks.cardinformation
{
    public class CardInformationService
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