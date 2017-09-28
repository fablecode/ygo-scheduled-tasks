using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ygo_scheduled_tasks.application.Dto;
using ygo_scheduled_tasks.application.Services;
using ygo_scheduled_tasks.domain.Model;

namespace ygo_scheduled_tasks.application.Command
{
    public class CommandMapper : ICommandMapper
    {
        private readonly ICategoryService _categoryService;
        private readonly ISubCategoryService _subCategoryService;
        private readonly ITypeService _typeService;
        private readonly IAttributeService _attributeService;
        private readonly ILinkArrowService _linkArrowService;
        private readonly ICardService _cardService;

        public CommandMapper
        (
            ICategoryService categoryService, 
            ISubCategoryService subCategoryService, 
            ITypeService typeService, 
            IAttributeService attributeService, 
            ILinkArrowService linkArrowService,
            ICardService cardService
        )
        {
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
            _typeService = typeService;
            _attributeService = attributeService;
            _linkArrowService = linkArrowService;
            _cardService = cardService;
        }


        public async Task<AddCardCommand> MapToAddCommand(YugiohCard yugiohCard)
        {
            ICollection<CategoryDto> categories = await _categoryService.GetAll();
            ICollection<SubCategoryDto> subCategories = await _subCategoryService.GetAll();

            var command = new AddCardCommand();

            command.CardType = yugiohCard.CardType;
            command.CardNumber = yugiohCard.CardNumber;
            command.Name = yugiohCard.Name;
            command.Description = yugiohCard.Description;


            if (command.CardType.Equals("Spell Card", StringComparison.OrdinalIgnoreCase))
            {
                var spellCategory = categories.Single(c => c.Name.Equals(YgoCardType.Spell.ToString(), StringComparison.OrdinalIgnoreCase));
                var spellSubCategory = subCategories.Single(sc => sc.CategoryId == spellCategory.Id && sc.Name.Equals(yugiohCard.Property));

                command.SubCategoryIds = new List<int>
                {
                    (int) spellSubCategory.Id.GetValueOrDefault(0)
                };
            }
            else if (command.CardType.Equals("Trap Card", StringComparison.OrdinalIgnoreCase))
            {
                var trapCategory = categories.Single(c => c.Name.Equals(YgoCardType.Trap.ToString(), StringComparison.OrdinalIgnoreCase));
                var trapSubCategory = subCategories.Single(sc => sc.CategoryId == trapCategory.Id && sc.Name.Equals(yugiohCard.Property));


                command.SubCategoryIds = new List<int>
                {
                    (int) trapSubCategory.Id.GetValueOrDefault(0)
                };
            }
            else
            {
                ICollection<TypeDto> types = await _typeService.GetAll();
                ICollection<AttributeDto> attributes = await _attributeService.GetAll();
                ICollection<LinkArrowDto> linkArrows = await _linkArrowService.GetAll();

                var monsterCategory = categories.Single(c => c.Name.Equals(YgoCardType.Monster.ToString(), StringComparison.OrdinalIgnoreCase));
                var monsterSubCategories = subCategories.Select(sc => sc).Where(sc => sc.CategoryId == monsterCategory.Id);

                command.AttributeId = (int)attributes.Single(sb => sb.Name.Equals(yugiohCard.Attribute, StringComparison.OrdinalIgnoreCase)).Id;

                command.SubCategoryIds = monsterSubCategories
                    .Where(sb => yugiohCard.MonsterSubCategoriesAndTypes.Any(sc => sc.Equals(sb.Name, StringComparison.OrdinalIgnoreCase)))
                    .Select(ssc => (int)ssc.Id)
                    .ToList();
                command.TypeIds = types
                    .Where(sb => yugiohCard.MonsterSubCategoriesAndTypes.Any(t => t.Equals(sb.Name, StringComparison.OrdinalIgnoreCase)))
                    .Select(smt => (int)smt.Id)
                    .ToList();

                command.LinkArrowIds = linkArrows
                    .Where(sb => yugiohCard.MonsterLinkArrows.Any(t => t.Equals(sb.Name, StringComparison.OrdinalIgnoreCase)))
                    .Select(smt => (int)smt.Id)
                    .ToList();

                command.CardLevel = yugiohCard.Level;
                command.CardRank = yugiohCard.Rank;
                command.Atk = 0;
                command.Def = 0;

                if (yugiohCard.Level > 0 || yugiohCard.Rank > 0)
                {
                    string atk = string.Empty;
                    string def = string.Empty;

                    try
                    {
                        atk = yugiohCard.AtkDef.Split('/')[0];
                        def = yugiohCard.AtkDef.Split('/')[1];
                    }
                    catch
                    {
                        //logger.Error(errorMessage, yugiohWikiaCard.Name);
                    }

                    int.TryParse(atk, out var cardAtk);
                    int.TryParse(def, out var cardDef);

                    command.Atk = cardAtk;
                    command.Def = cardDef;
                }
            }

            return command;
        }

        public async Task<UpdateCardCommand> MapToUpdateCommand(YugiohCard yugiohCard, CardDto cardToUpdate)
        {
            ICollection<CategoryDto> categories = await _categoryService.GetAll();
            ICollection<SubCategoryDto> subCategories = await _subCategoryService.GetAll();

            var command = new UpdateCardCommand();

            command.Id = cardToUpdate.Id;
            command.CardType = yugiohCard.CardType;
            command.CardNumber = yugiohCard.CardNumber;
            command.Name = yugiohCard.Name;
            command.Description = yugiohCard.Description;


            if (command.CardType.Equals("Spell Card", StringComparison.OrdinalIgnoreCase))
            {
                var spellCategory = categories.Single(c => c.Name.Equals(YgoCardType.Spell.ToString(), StringComparison.OrdinalIgnoreCase));
                var spellSubCategory = subCategories.Single(sc => sc.CategoryId == spellCategory.Id && sc.Name.Equals(yugiohCard.Property));

                command.SubCategoryIds = new List<int>
                {
                    (int) spellSubCategory.Id.GetValueOrDefault(0)
                };
            }
            else if (command.CardType.Equals("Trap Card", StringComparison.OrdinalIgnoreCase))
            {
                var trapCategory = categories.Single(c => c.Name.Equals(YgoCardType.Trap.ToString(), StringComparison.OrdinalIgnoreCase));
                var trapSubCategory = subCategories.Single(sc => sc.CategoryId == trapCategory.Id && sc.Name.Equals(yugiohCard.Property));


                command.SubCategoryIds = new List<int>
                {
                    (int) trapSubCategory.Id.GetValueOrDefault(0)
                };
            }
            else
            {
                ICollection<TypeDto> types = await _typeService.GetAll();
                ICollection<AttributeDto> attributes = await _attributeService.GetAll();
                ICollection<LinkArrowDto> linkArrows = await _linkArrowService.GetAll();

                var monsterCategory = categories.Single(c => c.Name.Equals(YgoCardType.Monster.ToString(), StringComparison.OrdinalIgnoreCase));
                var monsterSubCategories = subCategories.Select(sc => sc).Where(sc => sc.CategoryId == monsterCategory.Id);

                command.AttributeId = (int)attributes.Single(sb => sb.Name.Equals(yugiohCard.Attribute, StringComparison.OrdinalIgnoreCase)).Id;

                command.SubCategoryIds = monsterSubCategories
                    .Where(sb => yugiohCard.MonsterSubCategoriesAndTypes.Any(sc => sc.Equals(sb.Name, StringComparison.OrdinalIgnoreCase)))
                    .Select(ssc => (int)ssc.Id)
                    .ToList();
                command.TypeIds = types
                    .Where(sb => yugiohCard.MonsterSubCategoriesAndTypes.Any(t => t.Equals(sb.Name, StringComparison.OrdinalIgnoreCase)))
                    .Select(smt => (int)smt.Id)
                    .ToList();

                command.LinkArrowIds = linkArrows
                    .Where(sb => yugiohCard.MonsterLinkArrows.Any(t => t.Equals(sb.Name, StringComparison.OrdinalIgnoreCase)))
                    .Select(smt => (int)smt.Id)
                    .ToList();

                command.CardLevel = yugiohCard.Level;
                command.CardRank = yugiohCard.Rank;
                command.Atk = 0;
                command.Def = 0;

                if (yugiohCard.Level > 0 || yugiohCard.Rank > 0)
                {
                    string atk = string.Empty;
                    string def = string.Empty;

                    try
                    {
                        atk = yugiohCard.AtkDef.Split('/')[0];
                        def = yugiohCard.AtkDef.Split('/')[1];
                    }
                    catch
                    {
                        //logger.Error(errorMessage, yugiohWikiaCard.Name);
                    }

                    int.TryParse(atk, out var cardAtk);
                    int.TryParse(def, out var cardDef);

                    command.Atk = cardAtk;
                    command.Def = cardDef;
                }
            }

            return command;
        }
    }
}