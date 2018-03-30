using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor;

namespace ygo_scheduled_tasks.application.ScheduledTasks.ArchetypeInformation
{
    public class ArchetypeInformationTaskHandler : IRequestHandler<ArchetypeInformationTask, ArchetypeInformationTaskResult>
    {
        private readonly IArticleCategoryProcessor _articleCategoryProcessor;
        private readonly IValidator<ArchetypeInformationTask> _validator;

        public ArchetypeInformationTaskHandler(IArticleCategoryProcessor articleCategoryProcessor, IValidator<ArchetypeInformationTask> validator)
        {
            _articleCategoryProcessor = articleCategoryProcessor;
            _validator = validator;
        }
        public async Task<ArchetypeInformationTaskResult> Handle(ArchetypeInformationTask request, CancellationToken cancellationToken)
        {
            var response = new ArchetypeInformationTaskResult();

            var validationResults = _validator.Validate(request);

            if (validationResults.IsValid)
            {
                var results = await _articleCategoryProcessor.Process(request.Categories, request.PageSize);

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