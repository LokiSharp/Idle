using Program.Models.NameSpace;

namespace Test.Program.Models.NameSpace;

public class NameSpaceMapTests
{
    private readonly NameSpaceMap _nameSpaceMap = new();

    [Fact]
    public void RegisterNameSpace_ShouldRegisterSuccessfullyWhenNameIsUnique()
    {
        const string nameSpaceName = "uniqueNameSpace";
        const string displayName = "Unique display name";

        var registeredNameSpace = _nameSpaceMap.RegisterNamespace(nameSpaceName, displayName);

        Assert.NotNull(registeredNameSpace);
        Assert.Equal(nameSpaceName, registeredNameSpace.Name);
        Assert.Equal(displayName, registeredNameSpace.DisplayName);
    }

    [Fact]
    public void RegisterNameSpace_ShouldThrowArgumentExceptionWhenNameExists()
    {
        const string nameSpaceName = "existingNameSpace";
        const string displayName = "Existing display name";
        _nameSpaceMap.RegisterNamespace(nameSpaceName, displayName); // 创建已存在的命名空间

        var exception =
            Assert.Throws<ArgumentException>(() => _nameSpaceMap.RegisterNamespace(nameSpaceName, displayName));
        Assert.Contains("already exists", exception.Message);
    }

    [Fact]
    public void GetNameSpace_ShouldReturnTheSameNamespaceThatWasRegistered()
    {
        const string nameSpaceName = "testNameSpace";
        const string displayName = "Test NameSpace";
        var registeredNameSpace = _nameSpaceMap.RegisterNamespace(nameSpaceName, displayName);

        var retrievedNameSpace = _nameSpaceMap.GetNameSpace(nameSpaceName);

        Assert.Equal(registeredNameSpace, retrievedNameSpace);
    }
}