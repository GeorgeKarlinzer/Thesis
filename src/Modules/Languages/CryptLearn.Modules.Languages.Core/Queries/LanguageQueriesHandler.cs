using AutoMapper;
using CryptLearn.Modules.Languages.Core.DTO;
using CryptLearn.Modules.Languages.Core.Interfaces;
using CryptLearn.Modules.Languages.Core.Models;
using CryptLearn.Shared.Abstractions.Cqrs;

namespace CryptLearn.Modules.Languages.Core.Queries;
internal class LanguageQueriesHandler : IQueryHandler<GetLanguages, IEnumerable<LanguageDto>>
{
    private readonly ILanguagesRepository _repository;
    private readonly IMapper _mapper;

    public LanguageQueriesHandler(ILanguagesRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public Task<IEnumerable<LanguageDto>> Handle(GetLanguages request, CancellationToken cancellationToken)
    {
        var languages = _repository.GetAll();
        var dtos = _mapper.Map<IEnumerable<Language>, IEnumerable<LanguageDto>>(languages);
        return Task.FromResult(dtos);
    }
}
