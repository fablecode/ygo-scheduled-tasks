﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ygo_scheduled_tasks.application;
using ygo_scheduled_tasks.core.Model;
using ygo_scheduled_tasks.domain.Services;
using Attribute = ygo_scheduled_tasks.core.Model.Attribute;
using Type = ygo_scheduled_tasks.core.Model.Type;

namespace ygo_scheduled_tasks.domain.Command
{
    public class CommandMapper : ICommandMapper
    {
        private readonly ICategoryService _categoryService;
        private readonly ISubCategoryService _subCategoryService;
        private readonly ITypeService _typeService;
        private readonly IAttributeService _attributeService;
        private readonly ILinkArrowService _linkArrowService;

        public CommandMapper
        (
            ICategoryService categoryService, 
            ISubCategoryService subCategoryService, 
            ITypeService typeService, 
            IAttributeService attributeService, 
            ILinkArrowService linkArrowService
        )
        {
            _categoryService = categoryService;
            _subCategoryService = subCategoryService;
            _typeService = typeService;
            _attributeService = attributeService;
            _linkArrowService = linkArrowService;
        }


        public async Task<AddCardCommand> MapToAddCommand(YugiohCard yugiohCard)
        {
            ICollection<Category> categories = await _categoryService.GetAll();
            ICollection<SubCategory> subCategories = await _subCategoryService.GetAll();

            var command = new AddCardCommand();

            command.CardType = yugiohCard.CardType;
            command.CardNumber = yugiohCard.CardNumber;
            command.Name = yugiohCard.Name;
            command.Description = yugiohCard.Description;

            if (yugiohCard.ImageUrl != null)
                command.ImageUrl = new Uri(yugiohCard.ImageUrl);


            if (command.CardType.Equals(YgoCardType.Spell.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                command.SubCategoryIds = new List<int>
                {
                    SubCategoryId(categories, subCategories, YgoCardType.Spell, yugiohCard.Property)
                };
            }
            else if (command.CardType.Equals(YgoCardType.Trap.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                command.SubCategoryIds = new List<int>
                {
                    SubCategoryId(categories, subCategories, YgoCardType.Trap, yugiohCard.Property)
                };
            }
            else
            {
                ICollection<Type> types = await _typeService.GetAll();
                ICollection<Attribute> attributes = await _attributeService.GetAll();
                ICollection<LinkArrow> linkArrows = await _linkArrowService.GetAll();

                var monsterCategory = categories.Single(c => c.Name.Equals(YgoCardType.Monster.ToString(), StringComparison.OrdinalIgnoreCase));
                var monsterSubCategories = subCategories.Select(sc => sc).Where(sc => sc.CategoryId == monsterCategory.Id);

                command.AttributeId = MonsterAttributeId(yugiohCard, attributes);

                command.SubCategoryIds = MonsterSubCategoryIds(yugiohCard, monsterSubCategories);
                command.TypeIds = MonsterTypeIds(yugiohCard, types);

                if (yugiohCard.LinkArrows != null)
                {
                    command.LinkArrowIds = MonsterLinkArrowIds(yugiohCard, linkArrows);
                }


                if (yugiohCard.Level.HasValue)
                    command.CardLevel = yugiohCard.Level;

                if (yugiohCard.Rank.HasValue)
                    command.CardRank = yugiohCard.Rank;

                if (!string.IsNullOrWhiteSpace(yugiohCard.AtkDef))
                {
                    var atk = Atk(yugiohCard);
                    var def = DefOrLink(yugiohCard);

                    int.TryParse(atk, out var cardAtk);
                    int.TryParse(def, out var cardDef);

                    command.Atk = cardAtk;
                    command.Def = cardDef;
                }

                if (!string.IsNullOrWhiteSpace(yugiohCard.AtkLink))
                {
                    var atk = Atk(yugiohCard);

                    int.TryParse(atk, out var cardAtk);

                    command.Atk = cardAtk;
                }
            }

            return command;
        }

        public async Task<UpdateCardCommand> MapToUpdateCommand(YugiohCard yugiohCard, Card cardToUpdate)
        {
            ICollection<Category> categories = await _categoryService.GetAll();
            ICollection<SubCategory> subCategories = await _subCategoryService.GetAll();

            var command = new UpdateCardCommand();

            command.Id = cardToUpdate.Id;
            command.CardType = yugiohCard.CardType;
            command.CardNumber = yugiohCard.CardNumber;
            command.Name = yugiohCard.Name;
            command.Description = yugiohCard.Description;

            if (yugiohCard.ImageUrl != null)
                command.ImageUrl = new Uri(yugiohCard.ImageUrl);

            if (command.CardType.Equals(YgoCardType.Spell.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                command.SubCategoryIds = new List<int>
                {
                    SubCategoryId(categories, subCategories, YgoCardType.Spell, yugiohCard.Property)
                };
            }
            else if (command.CardType.Equals(YgoCardType.Trap.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                command.SubCategoryIds = new List<int>
                {
                    SubCategoryId(categories, subCategories, YgoCardType.Trap, yugiohCard.Property)
                };
            }
            else
            {
                ICollection<Type> types = await _typeService.GetAll();
                ICollection<Attribute> attributes = await _attributeService.GetAll();
                ICollection<LinkArrow> linkArrows = await _linkArrowService.GetAll();

                var monsterCategory = categories.Single(c => c.Name.Equals(YgoCardType.Monster.ToString(), StringComparison.OrdinalIgnoreCase));
                var monsterSubCategories = subCategories.Select(sc => sc).Where(sc => sc.CategoryId == monsterCategory.Id);

                command.AttributeId = MonsterAttributeId(yugiohCard, attributes);

                command.SubCategoryIds = MonsterSubCategoryIds(yugiohCard, monsterSubCategories);

                command.TypeIds = MonsterTypeIds(yugiohCard, types);

                if (yugiohCard.LinkArrows != null)
                {
                    command.LinkArrowIds = MonsterLinkArrowIds(yugiohCard, linkArrows);
                }

                if (yugiohCard.Level.HasValue)
                    command.CardLevel = yugiohCard.Level;

                if(yugiohCard.Rank.HasValue)
                    command.CardRank = yugiohCard.Rank;

                if (!string.IsNullOrWhiteSpace(yugiohCard.AtkDef))
                {
                    var atk = Atk(yugiohCard);
                    var def = DefOrLink(yugiohCard);

                    int.TryParse(atk, out var cardAtk);
                    int.TryParse(def, out var cardDef);

                    command.Atk = cardAtk;
                    command.Def = cardDef;
                }

                if (!string.IsNullOrWhiteSpace(yugiohCard.AtkLink))
                {
                    var atk = Atk(yugiohCard);

                    int.TryParse(atk, out var cardAtk);

                    command.Atk = cardAtk;
                }
            }

            return command;
        }

        #region private helpers

        private int SubCategoryId(IEnumerable<Category> categories, IEnumerable<SubCategory> subCategories, YgoCardType cardType, string subCategory)
        {
            return
                (
                    from sc in subCategories
                    join c in categories on sc.CategoryId equals c.Id
                    where c.Name.Equals(cardType.ToString(), StringComparison.OrdinalIgnoreCase) && sc.Name.Equals(subCategory, StringComparison.OrdinalIgnoreCase)
                    select sc.Id
                )
                .Single();
        }

        private static List<int> MonsterLinkArrowIds(YugiohCard yugiohCard, ICollection<LinkArrow> linkArrows)
        {
            return linkArrows
                .Where(mla => yugiohCard.MonsterLinkArrows.Any(ymla => ymla.Equals(mla.Name, StringComparison.OrdinalIgnoreCase)))
                .Select(la => (int)la.Id)
                .ToList();
        }

        private static List<int> MonsterTypeIds(YugiohCard yugiohCard, ICollection<Type> types)
        {
            return types
                .Where(mt => yugiohCard.MonsterSubCategoriesAndTypes.Any(yt => yt.Equals(mt.Name, StringComparison.OrdinalIgnoreCase)))
                .Select(smt => (int)smt.Id)
                .ToList();
        }

        private static List<int> MonsterSubCategoryIds(YugiohCard yugiohCard, IEnumerable<SubCategory> monsterSubCategories)
        {
            return monsterSubCategories
                .Where(msb => yugiohCard.MonsterSubCategoriesAndTypes.Any(ysc => ysc.Equals(msb.Name, StringComparison.OrdinalIgnoreCase)))
                .Select(ssc => (int)ssc.Id)
                .ToList();
        }

        private static int MonsterAttributeId(YugiohCard yugiohCard, ICollection<Attribute> attributes)
        {
            return
                (
                    from a in attributes
                    where a.Name.Equals(yugiohCard.Attribute, StringComparison.OrdinalIgnoreCase)
                    select a.Id
                )
                .Single();
        }

        private static string Atk(YugiohCard yugiohCard)
        {
            return yugiohCard.AtkDef.Split('/').First();
        }

        private static string DefOrLink(YugiohCard yugiohCard)
        {
            return yugiohCard.AtkDef.Split('/').Last();
        }

        #endregion
    }
}