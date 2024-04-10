namespace CryptLearn.Modules.ModuleSolving.Core.Entities
{
    internal class Language
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }

        protected Language() { }

        public Language(string name)
        {
            Name = name;
        }
    }
}
