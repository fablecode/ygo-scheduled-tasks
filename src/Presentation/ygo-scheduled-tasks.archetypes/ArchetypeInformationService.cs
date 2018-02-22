using System;

namespace ygo_scheduled_tasks.archetypes
{
    public class ArchetypeInformationService
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