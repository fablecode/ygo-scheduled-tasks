using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ygo_scheduled_tasks.domain;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor;
using ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor;
using ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor.Model;

namespace ygo_scheduled_tasks.application.ScheduledTasks.CardInformation
{
    public class CardInformationTaskHandler : IRequestHandler<CardInformationTask, CardInformationTaskResult>
    {
        private readonly IArticleCategoryProcessor _articleCategoryProcessor;
        private readonly ISemanticSearchProcessor _semanticSearchProcessor;
        private readonly IValidator<CardInformationTask> _validator;

        public CardInformationTaskHandler(IArticleCategoryProcessor articleCategoryProcessor, ISemanticSearchProcessor semanticSearchProcessor, IValidator<CardInformationTask> validator)
        {
            _articleCategoryProcessor = articleCategoryProcessor;
            _semanticSearchProcessor = semanticSearchProcessor;
            _validator = validator;
        }

        public async Task<CardInformationTaskResult> Handle(CardInformationTask request, CancellationToken cancellationToken)
        {
            var response = new CardInformationTaskResult();

            var validationResults = _validator.Validate(request);

            if (validationResults.IsValid)
            {
                foreach (var category in request.Categories)
                {
                    var categoryResult = await _articleCategoryProcessor.Process(category, request.PageSize);

                    response.ArticleTaskResults.Add(categoryResult);
                }

                await _semanticSearchProcessor.ProcessUrl(SemanticSearchCategory.NormalMonsters, SemanticSearchUrls.NormalMonsterCardsSearch);
                await _semanticSearchProcessor.ProcessUrl(SemanticSearchCategory.FlipMonsters, SemanticSearchUrls.FlipMonsterCardsSearch);
            }
            else
            {
                response.Errors = validationResults.Errors.Select(err => err.ErrorMessage).ToList();
            }

            return response;
        }
    }
}