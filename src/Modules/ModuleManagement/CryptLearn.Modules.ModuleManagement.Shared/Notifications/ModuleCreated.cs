using MediatR;

namespace CryptLearn.Modules.ModuleManagement.Shared.Notifications;
public record ModuleCreated(Guid Id, Guid AuthorId, IEnumerable<(string Language, string Solution, string Test)> Data) : INotification;
