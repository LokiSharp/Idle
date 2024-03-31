namespace Program.Models.NameSpace;

public class NameSpacedObject(NameSpaceLabel nameSpaceLabel, string id)
{
    public string NameSpace => nameSpaceLabel.Name;
    public string Id => $"{nameSpaceLabel.Name}:{id}";
    public string LocalId => id;
}