namespace Extension.Interfaces;

public interface IModelFaker<out T> where T : class
{
    public T FakerOne();

    public IEnumerable<T> FakeMany(int num);
}