namespace CryptLearn.Modules.ModuleSolving.Core.Entities
{
    internal class Test
    {
        public Guid Id { get; set; }
        public Guid ModuleId { get; set; }
        public Language Language { get; set; }
        public string Code { get; set; }

        protected Test()
        {
        }

        public Test(Guid moduleId, Language language, string code)
        {
            ModuleId = moduleId;
            Language = language;
            Code = code;
        }
    }
}
