using CryptLearn.Modules.Languages.Shared.Notifications;
using CryptLearn.Modules.ModuleManagement.Core.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptLearn.Modules.ModuleManagement.Core.Notifications;
internal class LanguageChangeNotificationHandler : INotificationHandler<LanguageChangedNotification>
{
    private readonly ILanguagesRepository _repository;

    public LanguageChangeNotificationHandler(ILanguagesRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(LanguageChangedNotification notification, CancellationToken cancellationToken)
    {
        var lang = await _repository.GetAll().SingleOrDefaultAsync(x => x.Name == notification.Name, cancellationToken);
        if (lang is null)
        {
            await _repository.AddAsync(new() { Name = notification.Name, IsActive = notification.IsActive }, cancellationToken);
            await _repository.SaveAsync(cancellationToken);
        }
        else
        {
            lang.IsActive = notification.IsActive;
            await _repository.SaveAsync(cancellationToken);
        }
    }
}
