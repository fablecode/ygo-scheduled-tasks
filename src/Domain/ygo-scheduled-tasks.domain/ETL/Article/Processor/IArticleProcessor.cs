﻿using System.Threading.Tasks;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.domain.ETL.Article.Processor.Model;

namespace ygo_scheduled_tasks.domain.ETL.Article.Processor
{
    public interface IArticleProcessor
    {
        Task<ArticleTaskResult> Process(string category, UnexpandedArticle article);
    }
}