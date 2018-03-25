namespace MarkUnit.Classes
{
    internal class InterfacePredicate : IInterfacePredicate
    {
        private readonly AssertionVerifier<IClass> _verifier;

        public InterfacePredicate(AssertionVerifier<IClass> verifier)
        {
            _verifier = verifier;
        }

        public IClassMatchingInterfaceCondition That()
        {
            PredicateString.Add("that");
            return  new ClassMatchingInterfaceCondition(new ClassToInterfaceFilterMapper(_verifier.Items),false);
        }
    }
}