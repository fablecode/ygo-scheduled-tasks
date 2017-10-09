using System;
using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.core.WebPage;
using ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor.Model;
using ygo_scheduled_tasks.domain.Services;

namespace ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor.Process
{
    public class SemanticSearchFlipMonstersProcessor : ISemanticCardItemProcess
    {
        private readonly IConfig _config;
        private readonly ICardWebPage _cardWebPage;
        private readonly IYugiohCardService _yugiohCardService;

        public SemanticSearchFlipMonstersProcessor(IConfig config, ICardWebPage cardWebPage, IYugiohCardService yugiohCardService)
        {
            _config = config;
            _cardWebPage = cardWebPage;
            _yugiohCardService = yugiohCardService;
        }

        public async Task<SemanticSearchTaskResult> ProcessItem(SemanticCard semanticCard)
        {
            var response = new SemanticSearchTaskResult { Card = semanticCard };

            var yugiohCard = _cardWebPage.GetYugiohCard(new Uri(new Uri(_config.WikiaDomainUrl), semanticCard.Url));

            const string flip = "Flip";
            if (yugiohCard != null && !yugiohCard.Types.ToLower().Contains(flip.ToLower()))
                yugiohCard.Types = $"{yugiohCard.Types} / {flip}";

            var card = await _yugiohCardService.AddOrUpdate(yugiohCard);

            if (card != null)
                response.IsSuccessfullyProcessed = true;

            return response;
        }

        public bool Handles(string category)
        {
            return category == SemanticSearchCategory.FlipMonsters;
        }
    }
}