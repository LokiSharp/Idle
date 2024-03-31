namespace Program.Models.NameSpace;

public class NameSpaceMap
{
    private Dictionary<string, NameSpaceLabel> RegisteredNameSpaces { get; set; } = new();

    public bool HasNameSpace(string name)
    {
        return RegisteredNameSpaces.ContainsKey(name);
    }

    public NameSpaceLabel GetNameSpace(string name)
    {
        return RegisteredNameSpaces[name];
    }

    public NameSpaceLabel RegisterNamespace(string name, string displayName)
    {
        if (HasNameSpace(name))
            throw new ArgumentException(
                $"Tried to register namespace: {name}, but it already exists");
        var nameSpace = new NameSpaceLabel { Name = name, DisplayName = displayName };
        RegisteredNameSpaces.Add(name, nameSpace);
        return nameSpace;
    }
}