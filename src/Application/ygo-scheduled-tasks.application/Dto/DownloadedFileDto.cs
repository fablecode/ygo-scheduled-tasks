using System;

namespace ygo_scheduled_tasks.application.Dto
{
    public class DownloadedFileDto
    {
        public Uri Source { get; set; }
        public string Destination { get; set; }
        public string ContentType { get; set; }
    }
}