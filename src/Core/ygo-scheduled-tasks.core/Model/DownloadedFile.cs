using System;

namespace ygo_scheduled_tasks.core.Model
{
    public class DownloadedFile
    {
        public Uri Source { get; set; }
        public string Destination { get; set; }
        public string ContentType { get; set; }
    }
}