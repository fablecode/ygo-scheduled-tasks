using System;
using System.Collections.Generic;
using MediatR;
using Quartz;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Model;

namespace ygo_scheduled_tasks.archetypes
{
    public class ArchetypeInformationJob : IJob
    {
        private readonly IMediator _mediator;

        public ArchetypeInformationJob(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async void Execute(IJobExecutionContext context)
        {
            const int pageSize = 500;
            var category = "Forbidden & Limited Lists";

            await _mediator.Send(new ArchetypeInformationTask { Categories = new[] { "Cards by archetype", "Cards by archetype support" }, PageSize = pageSize });
        }
    }

    public class ArchetypeInformationTask : IRequest<ArchetypeInformationTaskResult>
    {
        public string[] Categories { get; set; }

        public int PageSize { get; set; }
    }

    public class ArchetypeInformationTaskResult
    {
        public ArticleBatchTaskResult ArticleTaskResults { get; set; }

        public List<string> Errors { get; set; }

    }
}