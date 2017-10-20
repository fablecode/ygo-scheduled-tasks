using System;

namespace ygo_scheduled_tasks.latestbanlists
{
    public class BanlistInformationService
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