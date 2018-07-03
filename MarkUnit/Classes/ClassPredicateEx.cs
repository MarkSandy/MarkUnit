namespace MarkUnit.Classes
{
    internal class ClassPredicateEx : IClassPredicateEx
    {
        private readonly IAssertionVerifier<IClass> _verifier;
        private string[] _exceptions;

        public ClassPredicateEx(IAssertionVerifier<IClass> verifier)
        {
            _verifier = verifier;
        }

        public IClassUsesClassCondition That()
        {
            return WhichOrThat("that");
        }

        private IClassUsesClassCondition WhichOrThat(string word)
        {
            var classUsingClassCondition = new ClassUsingClassCondition(_verifier.Items, false);
            classUsingClassCondition.AddIgnoreList(_exceptions);
            var result=classUsingClassCondition;
            PredicateString.Add(word);
            return result;
        }

        public IClassUsesClassCondition Which()
        {
            return WhichOrThat("which");
        }
    }
}