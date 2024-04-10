namespace CryptLearn.Modules.ModuleManagement.Core.Entities
{
    internal class Module
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public List<Template> Templates { get; set; }
    }
}
