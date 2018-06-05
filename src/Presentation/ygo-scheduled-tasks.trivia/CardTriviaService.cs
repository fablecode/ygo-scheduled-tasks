using System;

namespace ygo_scheduled_tasks.trivia
{
    public class CardTriviaService
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