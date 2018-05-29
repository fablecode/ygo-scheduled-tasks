using System.Collections.Generic;
using System.Threading.Tasks;
using wikia.Api;
using wikia.Models.Article.AlphabeticalList;
using wikia.Models.Article.Simple;
using ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Model;
using ygo_scheduled_tasks.domain.ETL.Tips.Model;
using ygo_scheduled_tasks.domain.Services;

namespace ygo_scheduled_tasks.domain.ETL.ArticleList.Processor.Item
{
    public class CardTipItemProcessor : IArticleItemProcessor
    {
        private readonly IWikiArticle _wikiArticle;
        private readonly ICardService _cardService;
        private readonly ICardTipService _cardTipService;

        public CardTipItemProcessor(IWikiArticle wikiArticle, ICardService cardService, ICardTipService cardTipService)
        {
            _wikiArticle = wikiArticle;
            _cardService = cardService;
            _cardTipService = cardTipService;
        }

        public async Task<ArticleTaskResult> ProcessItem(UnexpandedArticle item)
        {
            var response = new ArticleTaskResult { Article = item };

            var card = await _cardService.CardByName(item.Title);

            if (card != null)
            {
                var tipSections = new List<CardTipSection>();

                var articleCardTips = await _wikiArticle.Simple(item.Id);

                foreach (var cardTipSection in articleCardTips.Sections)
                {
                    var tipSection = new CardTipSection
                    {
                        Title = cardTipSection.Title,
                        Content = GetSectionContentList(cardTipSection)
                    };

                    tipSections.Add(tipSection);
                }

                _cardTipService.Update(card.Id, tipSections);
            }

            return response;
        }

        private List<string> GetSectionContentList(Section cardTipSection)
        {
            var content = new List<string>();

            foreach (var c in cardTipSection.Content)
            {
                GetContentList(c.Elements, content);
            }

            return content;
        }

        public void GetContentList(IEnumerable<ListElement> elementList, List<string> contentlist)
        {
            if (elementList != null)
            {
                foreach (var e in elementList)
                {
                    if (e != null)
                    {
                        if (!string.IsNullOrEmpty(e.Text))
                            contentlist.Add(e.Text);

                        GetContentList(e.Elements, contentlist);
                    }
                }
            }
        }

        public bool Handles(string category)
        {
            return category == ArticleCategory.CardTips;
        }
    }
}