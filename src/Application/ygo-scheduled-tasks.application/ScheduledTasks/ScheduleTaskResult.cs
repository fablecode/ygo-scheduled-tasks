using System.Collections.Generic;

namespace ygo_scheduled_tasks.application.ScheduledTasks
{
    public class ScheduleTaskResult
    {
        public bool IsSuccessful { get; set; }

        public string Category { get; set; }

        public List<string> Processed { get; set; }

        public List<string> Errors { get; set; }

        public ScheduleTaskStatus ScheduleTaskStatus { get; set; }
    }
}