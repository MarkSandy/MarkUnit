namespace MarkUnit.Classes
{
    public interface ICondition<out T>
    {
        T Not();
    }
}
