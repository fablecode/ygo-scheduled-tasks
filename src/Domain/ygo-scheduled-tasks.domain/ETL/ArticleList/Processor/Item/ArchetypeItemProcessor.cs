﻿using System.Threading.Tasks;
using wikia.Models.Article.AlphabeticalList;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Model;

namespace ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Item
{
    public class ArchetypeItemProcessor : IArticleItemProcessor
    {
        public Task<ArticleTaskResult> ProcessItem(UnexpandedArticle item)
        {
            throw new System.NotImplementedException();
        }

        public bool Handles(string category)
        {
            return category == ArticleCategory.Archetype;
        }
    }
}