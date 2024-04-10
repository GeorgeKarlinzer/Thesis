using AutoMapper;
using CryptLearn.Modules.Languages.Core.DTO;
using CryptLearn.Modules.Languages.Core.Models;

namespace CryptLearn.Modules.Languages.Core.Mappers;
internal class MapperProfile : Profile
{
	public MapperProfile()
	{
		CreateMap<Language, LanguageDto>();
	}
}
