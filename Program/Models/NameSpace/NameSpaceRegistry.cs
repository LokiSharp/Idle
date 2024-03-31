namespace Program.Models.NameSpace;

public class NameSpaceRegistry(NameSpaceMap rootNamespaceMap)
{
    private NameSpaceMap RootNameSpaceMap { get; set; } = rootNamespaceMap;
    private Dictionary<string, Dictionary<string, NameSpacedObject>> NameSpaceMaps { get; set; } = new();
    private Dictionary<string, NameSpacedObject> RegisteredObjects { get; set; } = new();
    private Dictionary<string, NameSpacedObject> NamespaceChanges { get; set; } = new();

    public int Size => RegisteredObjects.Count;
    public List<NameSpacedObject> AllObjects => RegisteredObjects.Values.ToList();
    public NameSpacedObject FirstObject => RegisteredObjects.Values.First();

    public void RegisterObject(NameSpacedObject obj)
    {
        if (!RootNameSpaceMap.HasNameSpace(obj.NameSpace))
            throw new Exception(
                $"Tried to register object with namespace: {obj.NameSpace}, but namespace does not exist.");
        Dictionary<string, NameSpacedObject> nameMap;
        if (NameSpaceMaps.TryGetValue(obj.NameSpace, out var map))
        {
            nameMap = map;
        }
        else
        {
            nameMap = new Dictionary<string, NameSpacedObject>();
            NameSpaceMaps.Add(obj.NameSpace, nameMap);
        }

        if (!nameMap.TryAdd(obj.LocalId, obj))
            throw new Exception(
                $"Tried to register object with id: {obj.LocalId} and namespace: {obj.NameSpace}, but object with that id is already registered.");

        RegisteredObjects.Add($"{obj.NameSpace}:{obj.LocalId}", obj);
    }

    public NameSpacedObject? GetObject(string namespaceName, string id)
    {
        return NameSpaceMaps.TryGetValue(namespaceName, out var value) ? value.GetValueOrDefault(id) : null;
    }

    public NameSpacedObject? GetObjectById(string id)
    {
        return RegisteredObjects.GetValueOrDefault(id);
    }

    public void ForEach(Action<IEnumerable<NameSpacedObject>> callbackFn)
    {
        callbackFn(RegisteredObjects.Values);
    }

    public NameSpacedObject? Find(Func<NameSpacedObject, string, bool> predicate)
    {
        return (from entry in RegisteredObjects where predicate(entry.Value, entry.Key) select entry.Value)
            .FirstOrDefault();
    }

    public List<NameSpacedObject> Filter(Func<NameSpacedObject, string, bool> predicate)
    {
        return (from entry in RegisteredObjects where predicate(entry.Value, entry.Key) select entry.Value).ToList();
    }

    public bool Every(Func<NameSpacedObject, string, bool> predicate)
    {
        return RegisteredObjects.All(entry => predicate(entry.Value, entry.Key));
    }

    public bool EveryInNameSpace(string namespaceValue, Func<NameSpacedObject, string, bool> predicate)
    {
        return !NameSpaceMaps.TryGetValue(namespaceValue, out var namespaceMap) ||
               namespaceMap.All(entry => predicate(entry.Value, entry.Key));
    }

    public bool Some(Func<NameSpacedObject, string, bool> predicate)
    {
        return RegisteredObjects.Any(entry => predicate(entry.Value, entry.Key));
    }

    public bool SomeInNameSpace(string namespaceValue, Func<NameSpacedObject, string, bool> predicate)
    {
        return NameSpaceMaps.TryGetValue(namespaceValue, out var namespaceMap) &&
               namespaceMap.Any(entry => predicate(entry.Value, entry.Key));
    }

    public TReduce Reduce<TReduce>(Func<TReduce, NameSpacedObject, string, TReduce> callbackFn, TReduce initialValue)
    {
        return RegisteredObjects.Aggregate(initialValue,
            (current, entry) => callbackFn(current, entry.Value, entry.Key));
    }

    public HashSet<NameSpacedObject> GetSetForConstructor(List<string> ids, string objectBeingConstructed,
        string unregisteredName)
    {
        var resultSet = new HashSet<NameSpacedObject>();
        foreach (var id in ids)
        {
            if (!RegisteredObjects.TryGetValue(id, out var objectValue))
                throw new Exception(
                    $"Unregistered construction error. Object: {objectBeingConstructed}, Unregistered Name: {unregisteredName}, ID: {id}");
            resultSet.Add(objectValue);
        }

        return resultSet;
    }

    public (NameSpacedObject item, int quantity) GetQuantity((string id, int quantity) quantity)
    {
        if (!RegisteredObjects.TryGetValue(quantity.id, out var item))
            throw new Exception($"Error getting quantity. Object with id: {quantity.id} is not registered.");
        return (item, quantity.quantity);
    }

    public List<(NameSpacedObject item, int quantity)> GetQuantities(List<(string id, int quantity)> quantities)
    {
        return quantities.Select(GetQuantity).ToList();
    }

    public bool HasObjectInNamespace(string nameSpace)
    {
        return NameSpaceMaps.ContainsKey(nameSpace);
    }
}