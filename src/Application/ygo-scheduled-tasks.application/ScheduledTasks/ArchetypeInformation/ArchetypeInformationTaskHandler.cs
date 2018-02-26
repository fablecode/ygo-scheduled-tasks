﻿using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor;

namespace ygo_scheduled_tasks.application.ScheduledTasks.ArchetypeInformation
{
    public class ArchetypeInformationTaskHandler : IAsyncRequestHandler<ArchetypeInformationTask, ArchetypeInformationTaskResult>
    {
        private readonly IArticleCategoryProcessor _articleCategoryProcessor;
        private readonly IValidator<ArchetypeInformationTaskResult> _validator;

        public ArchetypeInformationTaskHandler(IArticleCategoryProcessor articleCategoryProcessor, IValidator<ArchetypeInformationTaskResult> validator)
        {
            _articleCategoryProcessor = articleCategoryProcessor;
            _validator = validator;
        }
        public async Task<ArchetypeInformationTaskResult> Handle(ArchetypeInformationTask message)
        {
            var response = new ArchetypeInformationTaskResult();

            var validationResults = _validator.Validate(message);

            if (validationResults.IsValid)
            {
                var results = await _articleCategoryProcessor.Process(message.Categories, message.PageSize);

                response.ArticleTaskResults = results;
            }
            else
            {
                response.Errors = validationResults.Errors.Select(err => err.ErrorMessage).ToList();
            }

            return response;
        }
    }
}