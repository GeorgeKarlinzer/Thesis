namespace CryptLearn.Modules.Languages.Core.Models;
internal class Language
{
    public string Name { get; set; }
    public bool IsActive { get; set; }

    protected Language()
    {
    }

    public Language(string name)
    {
        Name = name;
        IsActive = true;
    }
}
