using Bogus;
using Extension.Interfaces;

namespace Program.DB;

public class Character : DataBaseModelBase, IModelFaker<Character>
{
    private readonly Faker<Character> _faker = new Faker<Character>()
        .RuleFor(t => t.Id, f => f.IndexFaker)
        .RuleFor(t => t.CreateTime, f => f.Date.Between(new DateTime(2024, 1, 1), DateTime.Now))
        .RuleFor(t => t.Name, f => f.Name.FindName())
        .RuleFor(t => t.Age, f => f.Random.Int(10, 30));

    public string? Name { get; set; }
    public int Age { get; set; }

    public Character FakerOne()
    {
        return _faker.Generate();
    }

    public IEnumerable<Character> FakeMany(int num)
    {
        return _faker.Generate(num);
    }
}