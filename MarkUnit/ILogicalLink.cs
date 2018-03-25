namespace MarkUnit
{
    public interface ILogicalLink<out T>
    {
        T And();
    }
}