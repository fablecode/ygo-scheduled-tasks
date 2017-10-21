using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain.Services;

namespace ygo_scheduled_tasks.infrastructure.Services
{
    public class BanlistCardsService : IBanlistCardsService
    {
        private readonly ILimitService _limitService;
        private readonly ICardService _cardService;

        public BanlistCardsService(ILimitService limitService, ICardService cardService)
        {
            _limitService = limitService;
            _cardService = cardService;
        }

        public async Task<IList<BanlistCard>> MapToBanlistCards(long banlistId, IList<YugiohBanlistSection> yugiohBanlistSections)
        {
            var banlistCards = new List<BanlistCard>();

            var cardlimits = await _limitService.GetAll();

            const string forbidden = "forbidden";
            const string limited = "limited";
            const string semiLimited = "semi-limited";
            const string unlimited = "unlimited";

            var forbiddenBanSection = yugiohBanlistSections.Single(bs => bs.Title.Equals(forbidden, StringComparison.OrdinalIgnoreCase));
            var limitedBanSection = yugiohBanlistSections.Single(bs => bs.Title.Equals(limited, StringComparison.OrdinalIgnoreCase));
            var semiLimitedBanSection = yugiohBanlistSections.Single(bs => bs.Title.Equals(semiLimited, StringComparison.OrdinalIgnoreCase));
            var unlimitedBanSection = yugiohBanlistSections.Single(bs => bs.Title.Equals(unlimited, StringComparison.OrdinalIgnoreCase));

            if(forbiddenBanSection.Content.Any())
                await AddCardsToBanlist(banlistCards, forbiddenBanSection, banlistId, cardlimits, forbidden);

            if (limitedBanSection.Content.Any())
                await AddCardsToBanlist(banlistCards, limitedBanSection, banlistId, cardlimits, limited);

            if (semiLimitedBanSection.Content.Any())
                await AddCardsToBanlist(banlistCards, semiLimitedBanSection, banlistId, cardlimits, semiLimited);

            if (unlimitedBanSection.Content.Any())
                await AddCardsToBanlist(banlistCards, unlimitedBanSection, banlistId, cardlimits, unlimited);

            return banlistCards;
        }


        #region private helpers

        private async Task AddCardsToBanlist(List<BanlistCard> banlistCards, YugiohBanlistSection forbiddenBanSection, long banlistId, ICollection<Limit> cardlimits, string limit)
        {
            var selectedLimit = cardlimits.Single(l => l.Name.Equals(limit, StringComparison.OrdinalIgnoreCase));

            foreach (var cardName in forbiddenBanSection.Content)
            {
                var card = await _cardService.CardByName(cardName);

                if(card != null)
                    banlistCards.Add(new BanlistCard {BanlistId = banlistId, CardId = card.Id, Limit = selectedLimit.Id});
            }
        }

        #endregion
    }
}