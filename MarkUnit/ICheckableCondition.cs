namespace MarkUnit
{
    public interface ICheckableCondition<out T>
        : IRule<T>,
          ICheckable { }
}
