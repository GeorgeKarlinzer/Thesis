using MediatR;

namespace CryptLearn.Modules.ModuleManagement.Shared.Notifications;

public record ModuleUpdated(Guid Id, IEnumerable<(string Language, string Solution, string Test)> Data) : INotification;