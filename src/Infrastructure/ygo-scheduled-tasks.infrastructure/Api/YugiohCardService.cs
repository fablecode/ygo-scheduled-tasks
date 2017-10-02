﻿using System.Threading.Tasks;
using ygo_scheduled_tasks.application.Api;
using ygo_scheduled_tasks.application.Command;
using ygo_scheduled_tasks.application.Dto;
using ygo_scheduled_tasks.domain.Model;

namespace ygo_scheduled_tasks.infrastructure.Api
{
    public class YugiohCardService : IYugiohCardService
    {
        private readonly ICardService _cardService;
        private readonly ICommandMapper _commandMapper;

        public YugiohCardService(ICardService cardService, ICommandMapper commandMapper)
        {
            _cardService = cardService;
            _commandMapper = commandMapper;
        }

        public async Task<CardDto> AddOrUpdate(YugiohCard yugiohCard)
        {
            var card = await _cardService.CardByName(yugiohCard.Name);

            return card == null
                ? await _cardService.Add(await _commandMapper.MapToAddCommand(yugiohCard))
                : await _cardService.Update(await _commandMapper.MapToUpdateCommand(yugiohCard, card));
        }
    }
}