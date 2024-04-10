using AutoMapper;
using CryptLearn.Modules.ModuleSolving.Core.DTOs;
using CryptLearn.Modules.ModuleSolving.Core.Entities;
using System.Reflection;

namespace CryptLearn.Modules.ModuleSolving.Core.Mappers;
internal class MapperProfile : Profile
{
	public MapperProfile()
	{
        CreateMap<Solution, SolutionDto>()
            .ConstructUsing(x => new(x.Language.Name, x.Code));
    }
}
