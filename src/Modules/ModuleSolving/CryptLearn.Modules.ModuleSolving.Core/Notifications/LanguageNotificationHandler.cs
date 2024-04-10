using CryptLearn.Modules.Languages.Shared.Notifications;
using CryptLearn.Modules.ModuleSolving.Core.Interfaces;
using MediatR;

namespace CryptLearn.Modules.ModuleSolving.Core.Notifications;

internal class LanguageNotificationHandler : INotificationHandler<LanguageChangedNotification>
{
    private readonly ILanguageRepository _repository;

    public LanguageNotificationHandler(ILanguageRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(LanguageChangedNotification notification, CancellationToken cancellationToken)
    {
        var language = _repository.GetAll().FirstOrDefault(x => x.Name.ToLower() == notification.Name.ToLower());
        if(language is not null)
        {
            language.IsActive = notification.IsActive;
        }
        else
        {
            await _repository.AddAsync(new(notification.Name) { IsActive = notification.IsActive }, cancellationToken);
        }

        await _repository.SaveAsync(cancellationToken);
    }
}
