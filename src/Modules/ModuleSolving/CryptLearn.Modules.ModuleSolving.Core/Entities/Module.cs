namespace CryptLearn.Modules.ModuleSolving.Core.Entities
{
    internal class Module
    {
        public Guid Id { get; set; }
        public Guid AthorId { get; set; }
        public List<Test> Tests { get; set; }
        public List<Solution> Solutions { get; set; }
    }
}
