using MediatR;

namespace CryptLearn.Modules.ModuleManagement.Shared.Notifications;

public record ModuleDeleted(Guid Id) : INotification;
