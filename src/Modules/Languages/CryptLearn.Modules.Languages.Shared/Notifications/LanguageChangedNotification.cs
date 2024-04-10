using MediatR;

namespace CryptLearn.Modules.Languages.Shared.Notifications
{
    public record LanguageChangedNotification(string Name, bool IsActive) : INotification;
}
