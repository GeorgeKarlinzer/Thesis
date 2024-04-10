namespace CryptLearn.Modules.ModuleManagement.Core.DTOs
{
    internal record ModuleDto(Guid Id, string Name, Guid AuthorId, string AuthorName, string Description, IEnumerable<LanguageTemplateDto> Templates);
}
