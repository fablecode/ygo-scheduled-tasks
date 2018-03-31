using System;
using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.core.WebPage;
using ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor.Model;
using ygo_scheduled_tasks.domain.Services;

namespace ygo_scheduled_tasks.domain.ETL.SemanticSearch.Processor.Process
{
    public class SemanticSearchNormalMonstersProcessor : ISemanticCardItemProcess
    {
        private readonly IConfig _config;
        private readonly ICardWebPage _cardWebPage;
        private readonly IYugiohCardService _yugiohCardService;

        public SemanticSearchNormalMonstersProcessor(IConfig config, ICardWebPage cardWebPage, IYugiohCardService yugiohCardService)
        {
            _config = config;
            _cardWebPage = cardWebPage;
            _yugiohCardService = yugiohCardService;
        }

        public async Task<SemanticSearchTaskResult> ProcessItem(SemanticCard semanticCard)
        {
            var response = new SemanticSearchTaskResult {  Card = semanticCard };

            var yugiohCard = _cardWebPage.GetYugiohCard(new Uri(new Uri(_config.WikiaDomainUrl), semanticCard.Url));

            if (yugiohCard != null)
            {
                response.YugiohCard = yugiohCard;

                const string normal = "Normal";

                if (!yugiohCard.Types.ToLower().Contains(normal.ToLower()))
                    yugiohCard.Types = $"{yugiohCard.Types} / {normal}";

                var card = await _yugiohCardService.AddOrUpdate(yugiohCard);

                if (card != null)
                    response.IsSuccessfullyProcessed = true;
            }

            return response;
        }

        public bool Handles(string category)
        {
            return category == SemanticSearchCategory.NormalMonsters;
        }
    }
}