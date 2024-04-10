namespace CryptLearn.Modules.ModuleManagement.Core.Entities
{
    internal class Template
    {
        public Guid Id { get; set; }
        public Module Module { get; set; }
        public string LanguageName { get; set; }
        public string Code { get; set; }
    }
}
