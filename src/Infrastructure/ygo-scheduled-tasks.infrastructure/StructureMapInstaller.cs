using System.Collections.Generic;
using StructureMap;
using ygo_scheduled_tasks.application.Dto;
using ygo_scheduled_tasks.application.Services;
using ygo_scheduled_tasks.infrastructure.Services;

namespace ygo_scheduled_tasks.infrastructure
{
    public class StructureMapInstaller : Registry
    {
        public StructureMapInstaller()
        {

            Scan
            (
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                }

            );

            For<IYugiohCardService>().Use<YugiohCardService>();

            For<ICardService>().Use<CardService>();
            For<ICategoryService>().Use<CategoryService>();
            For<ISubCategoryService>().Use<SubCategoryService>();
            For<ITypeService>().Use<TypeService>();
            For<IAttributeService>().Use<AttributeService>();
            For<ILinkArrowService>().Use<LinkArrowService>();

                //ICategoryService categoryService,
                //ISubCategoryService subCategoryService, 
                //ITypeService typeService,
                //IAttributeService attributeService, 
                //ILinkArrowService linkArrowService,
                //ICardService cardService

        }
    }

    public class LinkArrowService : ILinkArrowService
    {
        public ICollection<LinkArrowDto> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }

    public class AttributeService : IAttributeService
    {
        public ICollection<AttributeDto> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }

    public class TypeService : ITypeService
    {
        public ICollection<TypeDto> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }

    public class SubCategoryService : ISubCategoryService
    {
        public ICollection<SubCategoryDto> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }

    public class CategoryService : ICategoryService
    {
        public ICollection<CategoryDto> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}