namespace MarkUnit
{
    public interface ICheckable
    {
        void Check();
    }

    internal interface IInternalCheckable
    {
        void Check();
    }

    internal class BinaryAssertionOperator<T> : LogicalLink<T>   where T : ICheckable
    {
        public BinaryAssertionOperator(T followUp) : base(followUp)
        {
        }

        public void Check()
        {
            FollowUp.Check();
        }
    }
}