using Program.Models.NameSpace;

namespace Test.Program.Models.NameSpace;

public class NameSpacedObjectTests
{
    private readonly string _id;
    private readonly NameSpacedObject _nameSpacedObject;
    private readonly NameSpaceLabel _nameSpaceLabel;

    public NameSpacedObjectTests()
    {
        _nameSpaceLabel = new NameSpaceLabel { Name = "testNamespace", DisplayName = "Test Namespace" };
        _id = "testID";
        _nameSpacedObject = new NameSpacedObject(_nameSpaceLabel, _id);
    }

    [Fact]
    public void NameSpace_ReturnsCorrectNameSpace()
    {
        Assert.Equal(_nameSpaceLabel.Name, _nameSpacedObject.NameSpace);
    }

    [Fact]
    public void Id_ReturnsCorrectId()
    {
        Assert.Equal($"{_nameSpaceLabel.Name}:{_id}", _nameSpacedObject.Id);
    }

    [Fact]
    public void LocalId_ReturnsCorrectLocalId()
    {
        Assert.Equal(_id, _nameSpacedObject.LocalId);
    }
}