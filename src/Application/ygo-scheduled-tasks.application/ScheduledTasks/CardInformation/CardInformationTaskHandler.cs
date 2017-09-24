using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.application.ETL.Processor;

namespace ygo_scheduled_tasks.application.ScheduledTasks.CardInformation
{
    public class CardInformationTaskHandler : IAsyncRequestHandler<CardInformationTask, CardInformationTaskResult>
    {
        private readonly ICategoryProcessor _categoryProcessor;
        private readonly IValidator<CardInformationTask> _validator;

        public CardInformationTaskHandler(ICategoryProcessor categoryProcessor, IValidator<CardInformationTask> validator)
        {
            _categoryProcessor = categoryProcessor;
            _validator = validator;
        }

        public async Task<CardInformationTaskResult> Handle(CardInformationTask message)
        {
            var response = new CardInformationTaskResult();

            var validationResults = _validator.Validate(message);

            if (validationResults.IsValid)
            {
                foreach (var category in message.Categories)
                {
                    var categoryResult = await _categoryProcessor.Process(category, message.PageSize);

                    response.ArticleTaskResults.Add(categoryResult);
                }
            }
            else
            {
                response.Errors = validationResults.Errors.Select(err => err.ErrorMessage).ToList();
            }

            return response;
        }
    }

    public class CardInformationTaskResult
    {
        public List<ArticleBatchTaskResult> ArticleTaskResults { get; set; } = new List<ArticleBatchTaskResult>();

        public List<string> Errors { get; set; }
    }

    public class ArticleBatchTaskResult
    {
        public string Category { get; set; }

        public int Processed { get; set; }

        public List<ArticleException> Failed { get; set; } = new List<ArticleException>();
    }

    public class ArticleTaskResult
    {
        public bool Processed { get; set; }

        public UnexpandedArticle Article { get; set; }

        public ArticleException Failed { get; set; }
    }


    public class ArticleException
    {
        public UnexpandedArticle Article { get; set; }

        public Exception Exception { get; set; }
    }
}