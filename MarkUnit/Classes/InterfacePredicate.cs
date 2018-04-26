namespace MarkUnit.Classes
{
    internal class InterfacePredicate : IInterfacePredicate
    {
        private readonly ClassMatchingInterfaceCondition _classMatchingInterfaceCondition;

        public InterfacePredicate(IAssertionVerifier<IClassInfo> verifier)
        {
            _classMatchingInterfaceCondition = new ClassMatchingInterfaceCondition(new ClassToInterfaceFilterMapper(verifier.Items), false);
        }

        public IClassMatchingInterfaceCondition That()
        {
            return WhichOrThat("that");
        }

        private IClassMatchingInterfaceCondition WhichOrThat(string word)
        {
            PredicateString.Add(word);
            return _classMatchingInterfaceCondition;
        }

        public IClassMatchingInterfaceCondition Which()
        {
            return WhichOrThat("which");
        }
    }
}