namespace MarkUnit
{
    public interface IRule<out T>
    {
        T And(); 
    }
}
