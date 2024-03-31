using Program.Models.NameSpace;

namespace Test.Program.Models.NameSpace;

public class NameSpaceRegistryTests
{
    private readonly NameSpacedObject _anotherObject;
    private readonly NameSpaceRegistry _nameSpaceRegistry;
    private readonly NameSpacedObject _testObject;

    public NameSpaceRegistryTests()
    {
        var nameSpaceMap = new NameSpaceMap();
        nameSpaceMap.RegisterNamespace("TestNameSpace", "TestNameSpace");
        _nameSpaceRegistry = new NameSpaceRegistry(nameSpaceMap);
        var nameSpaceLabel = new NameSpaceLabel { Name = "TestNameSpace", DisplayName = "TestNameSpace" };
        _testObject = new NameSpacedObject(nameSpaceLabel,
            "TestID");
        _anotherObject = new NameSpacedObject(nameSpaceLabel, "AnotherID");
    }

    [Fact]
    public void RegisterObject_ObjectIsRegistered_Success()
    {
        _nameSpaceRegistry.RegisterObject(_testObject);

        var actualObject = _nameSpaceRegistry.GetObject("TestNameSpace", "TestID");
        Assert.Equal(_testObject, actualObject);
    }

    [Fact]
    public void GetObjectById_ObjectIsRetrieved_Success()
    {
        _nameSpaceRegistry.RegisterObject(_testObject);
        var actualObject = _nameSpaceRegistry.GetObjectById("TestNameSpace:TestID");

        Assert.Equal(_testObject, actualObject);
    }

    [Fact]
    public void TestForEach_Method_AppliesActionToEveryRegisteredObject()
    {
        _nameSpaceRegistry.RegisterObject(_testObject);
        _nameSpaceRegistry.RegisterObject(_anotherObject);

        var ids = new List<string>();

        _nameSpaceRegistry.ForEach(objects => objects.ToList().ForEach(obj => ids.Add(obj.Id)));

        Assert.Contains(_testObject.Id, ids);
        Assert.Contains(_anotherObject.Id, ids);
    }

    [Fact]
    public void Find_FindObjectsByCondition_Success()
    {
        _nameSpaceRegistry.RegisterObject(_testObject);
        _nameSpaceRegistry.RegisterObject(_anotherObject);

        var result =
            _nameSpaceRegistry.Find((obj, _) => (obj.NameSpace == "TestNameSpace") & (obj.LocalId == "TestID"));

        Assert.Equal("TestNameSpace:TestID", result!.Id);
    }

    [Fact]
    public void Filter_FilterObjectsByCondition_Success()
    {
        _nameSpaceRegistry.RegisterObject(_testObject);
        _nameSpaceRegistry.RegisterObject(_anotherObject);

        var filtered = _nameSpaceRegistry.Filter((obj, _) => obj.NameSpace == "TestNameSpace");

        Assert.Equal(2, filtered.Count);
    }

    [Fact]
    public void Every_ObjectMeetCondition_True()
    {
        _nameSpaceRegistry.RegisterObject(_testObject);
        _nameSpaceRegistry.RegisterObject(_anotherObject);
        var isEvery = _nameSpaceRegistry.Every((obj, _) => obj.NameSpace == "TestNameSpace");

        Assert.True(isEvery);
    }


    [Fact]
    public void EveryInNameSpace_AllObjectsMeetPredicate_ReturnsTrue()
    {
        _nameSpaceRegistry.RegisterObject(_testObject);
        _nameSpaceRegistry.RegisterObject(_anotherObject);

        var result =
            _nameSpaceRegistry.EveryInNameSpace("TestNameSpace", (obj, _) => obj.NameSpace == "TestNameSpace");

        Assert.True(result);
    }

    [Fact]
    public void EveryInNameSpace_SomeObjectsDoNotMeetPredicate_ReturnsFalse()
    {
        _nameSpaceRegistry.RegisterObject(_testObject);
        _nameSpaceRegistry.RegisterObject(_anotherObject);

        var result =
            _nameSpaceRegistry.EveryInNameSpace("TestNameSpace", (obj, _) => obj.NameSpace == "AnotherNameSpace");

        Assert.False(result);
    }

    [Fact]
    public void EveryInNameSpace_NameSpaceDoesNotExist_ReturnsTrue()
    {
        _nameSpaceRegistry.RegisterObject(_testObject);
        _nameSpaceRegistry.RegisterObject(_anotherObject);

        var result =
            _nameSpaceRegistry.EveryInNameSpace("NonExistentNameSpace", (obj, _) => obj.NameSpace == "TestNameSpace");

        Assert.True(result);
    }


    [Fact]
    public void Some_ObjectMeetCondition_True()
    {
        _nameSpaceRegistry.RegisterObject(_testObject);
        var isSome = _nameSpaceRegistry.Some((obj, _) => obj.NameSpace == "TestNameSpace");

        Assert.True(isSome);
    }

    [Fact]
    public void SomeInNameSpace_SomeObjectsMeetPredicate_ReturnsTrue()
    {
        _nameSpaceRegistry.RegisterObject(_testObject);
        _nameSpaceRegistry.RegisterObject(_anotherObject);

        var result = _nameSpaceRegistry.SomeInNameSpace("TestNameSpace", (obj, _) => obj.NameSpace == "TestNameSpace");

        Assert.True(result);
    }

    [Fact]
    public void SomeInNameSpace_NoObjectsMeetPredicate_ReturnsFalse()
    {
        _nameSpaceRegistry.RegisterObject(_testObject);
        _nameSpaceRegistry.RegisterObject(_anotherObject);

        var result =
            _nameSpaceRegistry.SomeInNameSpace("TestNameSpace", (obj, _) => obj.NameSpace == "AnotherNameSpace");

        Assert.False(result);
    }

    [Fact]
    public void SomeInNameSpace_NameSpaceDoesNotExist_ReturnsFalse()
    {
        _nameSpaceRegistry.RegisterObject(_testObject);
        _nameSpaceRegistry.RegisterObject(_anotherObject);

        var result =
            _nameSpaceRegistry.SomeInNameSpace("NonExistentNameSpace", (obj, _) => obj.NameSpace == "TestNameSpace");

        Assert.False(result);
    }

    [Fact]
    public void Reduce_ReduceObjectsByCondition_Success()
    {
        _nameSpaceRegistry.RegisterObject(_testObject);
        _nameSpaceRegistry.RegisterObject(_anotherObject);
        var count = _nameSpaceRegistry.Reduce((acc, _, _) => acc + 1, 0);

        Assert.Equal(2, count);
    }

    [Fact]
    public void GetSetForConstructor_ObjectsForConstructor_Success()
    {
        _nameSpaceRegistry.RegisterObject(_testObject);
        _nameSpaceRegistry.RegisterObject(_anotherObject);
        var ids = new List<string> { "TestNameSpace:TestID", "TestNameSpace:AnotherID" };
        var set = _nameSpaceRegistry.GetSetForConstructor(ids, "Constructing", "UnregisteredName");

        Assert.Equal(2, set.Count);
    }

    [Fact]
    public void GetQuantity_ReturnsRegisteredObjectAndQuantity()
    {
        _nameSpaceRegistry.RegisterObject(_testObject);

        var (item, quantity) = _nameSpaceRegistry.GetQuantity((_testObject.Id, 10));

        Assert.Equal(_testObject, item);
        Assert.Equal(10, quantity);
    }

    [Fact]
    public void GetQuantity_ObjectIsNotRegistered_ThrowsException()
    {
        _nameSpaceRegistry.RegisterObject(_testObject);

        var ex = Assert.Throws<Exception>(() => _nameSpaceRegistry.GetQuantity(("InvalidId", 10)));

        Assert.Equal($"Error getting quantity. Object with id: InvalidId is not registered.", ex.Message);
    }

    [Fact]
    public void GetQuantities_QuantitiesObtained_Success()
    {
        _nameSpaceRegistry.RegisterObject(_testObject);
        var quantities = new List<(string id, int quantity)> { ("TestNameSpace:TestID", 1) };
        var result = _nameSpaceRegistry.GetQuantities(quantities);

        Assert.Single(result);
        Assert.Equal(_testObject, result[0].item);
        Assert.Equal(1, result[0].quantity);
    }

    [Fact]
    public void HasObjectInNamespace_ExistObjectInNamespace_True()
    {
        _nameSpaceRegistry.RegisterObject(_anotherObject);
        var isExist = _nameSpaceRegistry.HasObjectInNamespace("TestNameSpace");

        Assert.True(isExist);
    }
}