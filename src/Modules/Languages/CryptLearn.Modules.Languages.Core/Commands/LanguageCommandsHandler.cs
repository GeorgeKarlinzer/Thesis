using CryptLearn.Modules.Languages.Core.Interfaces;
using CryptLearn.Modules.Languages.Core.Models;
using CryptLearn.Modules.Languages.Shared.Notifications;
using CryptLearn.Shared.Abstractions.Cqrs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptLearn.Modules.Languages.Core.Commands;

internal class LanguageCommandsHandler : ICommandHandler<ChangeActivityState>, ICommandHandler<CreateLanguage>
{
    private readonly ILanguagesRepository _repository;
    private readonly IPublisher _publisher;

    public LanguageCommandsHandler(ILanguagesRepository repository, IPublisher publisher)
    {
        _repository = repository;
        _publisher = publisher;
    }

    public async Task Handle(ChangeActivityState request, CancellationToken cancellationToken)
    {
        var lang = await _repository.GetAll().SingleOrDefaultAsync(x => x.Name == request.Name.ToUpperInvariant(), cancellationToken);
        lang.IsActive = !lang.IsActive;
        await _repository.SaveAsync(cancellationToken);
        await _publisher.Publish(new LanguageChangedNotification(lang.Name, lang.IsActive), cancellationToken);
    }

    public async Task Handle(CreateLanguage request, CancellationToken cancellationToken)
    {
        var lang = new Language(request.Name.ToLowerInvariant());
        await _repository.AddAsync(lang, cancellationToken);
        await _repository.SaveAsync(cancellationToken);
        await _publisher.Publish(new LanguageChangedNotification(lang.Name, lang.IsActive), cancellationToken);
    }
}