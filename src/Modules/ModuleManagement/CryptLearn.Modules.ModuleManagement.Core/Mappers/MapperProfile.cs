using AutoMapper;
using CryptLearn.Modules.ModuleManagement.Core.DTOs;
using CryptLearn.Modules.ModuleManagement.Core.Entities;

namespace CryptLearn.Modules.ModuleManagement.Core.Mappers;
internal class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Template, LanguageTemplateDto>()
            .ForMember(x => x.Language, x => x.Ignore())
            .ConstructUsing(x => new(x.LanguageName, x.Code));

        CreateMap<Module, ModuleDto>()
            .ForMember(x => x.Templates, x => x.MapFrom(m => m.Templates));

        CreateMap<Module, ModuleListItemDto>();
    }
}
