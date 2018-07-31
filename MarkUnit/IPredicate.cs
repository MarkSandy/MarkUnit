using MarkUnit.Classes;

namespace MarkUnit
{
    public interface IPredicate<out TCondition> 
    {
        TCondition That();
        TCondition Which();
        
    }
}
