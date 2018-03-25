namespace MarkUnit
{
    public interface IPredicate<out TCondition>
    {
        TCondition That();
    }
}