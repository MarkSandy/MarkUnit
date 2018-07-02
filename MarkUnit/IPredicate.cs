using MarkUnit.Classes;

namespace MarkUnit
{
    public interface IPredicate<out TCondition>
    {
        IPredicate<TCondition> Except(params string[] exceptionPatterns);
        TCondition That();
        TCondition Which();
    }
}
