namespace MarkUnit
{
    public interface ICheckableCondition<out T>
        : ILogicalLink<T>,
          ICheckable { }
}
