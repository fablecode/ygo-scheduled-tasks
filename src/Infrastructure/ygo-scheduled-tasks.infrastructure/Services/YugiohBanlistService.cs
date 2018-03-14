using System;
using System.Threading.Tasks;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain.Command;
using ygo_scheduled_tasks.domain.Services;

namespace ygo_scheduled_tasks.infrastructure.Services
{
    public class YugiohBanlistService : IYugiohBanlistService
    {
        private readonly IFormatService _formatService;
        private readonly IBanlistService _banlistService;
        private readonly IBanlistCardsService _banlistCardsService;

        public YugiohBanlistService(IFormatService formatService, IBanlistService banlistService, IBanlistCardsService banlistCardsService)
        {
            _formatService = formatService;
            _banlistService = banlistService;
            _banlistCardsService = banlistCardsService;
        }

        public async Task<Banlist> AddOrUpdate(YugiohBanlist yugiohBanlist)
        {
            var format = await _formatService.FormatByAcronym(yugiohBanlist.BanlistType.ToString());

            if(format == null)
                throw new ArgumentException($"Format with acronym '{yugiohBanlist.BanlistType.ToString()}' not found.");

            var banlist = await _banlistService.BanlistById(yugiohBanlist.ArticleId);

            if (banlist == null)
            {
                var addCommand = new AddBanlistCommand
                {
                    Id = yugiohBanlist.ArticleId,
                    FormatId = format.Id,
                    Name = yugiohBanlist.Title,
                    ReleaseDate = yugiohBanlist.StartDate
                };

                banlist = await _banlistService.Add(addCommand);
            }
            else
            {
                var updateCommand = new UpdateBanlistCommand
                {
                    Id = yugiohBanlist.ArticleId,
                    FormatId = format.Id,
                    Name = yugiohBanlist.Title,
                    ReleaseDate = yugiohBanlist.StartDate
                };

                banlist = await _banlistService.Update(updateCommand);
            }

            var banlistCards = await _banlistCardsService.MapToBanlistCards(banlist.Id, yugiohBanlist.Sections);
            banlist.Cards = await _banlistService.Update(banlist.Id, new UpdateBanlistCardsCommand { BanlistCards = banlistCards });

            return banlist;
        }
    }
}