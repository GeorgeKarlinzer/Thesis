namespace CryptLearn.Modules.ModuleSolving.Core.Entities
{
    internal class Solution
    {
        public Guid Id { get; set; }
        public Guid ModuleId { get; set; }
        public Guid UserId { get; set; }
        public Language Language { get; set; }
        public string Code { get; set; }

        public Solution()
        {
        }

        public Solution(Guid moduleId, Guid userId, Language language, string code)
        {
            ModuleId = moduleId;
            UserId = userId;
            Language = language;
            Code = code;
        }
    }
}
