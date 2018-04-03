namespace MarkUnit.Classes
{
    internal class ClassPredicate : IClassPredicate
    {
        private readonly IClassCollector _classCollector;
        private readonly bool _negate;
        private readonly bool _not;

        public ClassPredicate(IClassCollector classCollector, bool negate, bool not)
        {
            _classCollector = classCollector;
            _negate = negate;
            _not = not;
        }

        public IClassCollection That()
        {
            return ThatOrWhich("that");
        }

        private IClassCollection ThatOrWhich(string word)
        {
            PredicateString.Add(word);
            var classes = _classCollector.Get();
            var classFilter = new FilteredClasses(classes);
            var result = new ClassCondition(classFilter, _negate);
            return _not ? result.Not() : result;
        }

        public IClassCollection Which()
        {
            return ThatOrWhich("which");
        }
    }
}
