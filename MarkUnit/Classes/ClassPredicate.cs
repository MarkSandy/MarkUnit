using System.Runtime.CompilerServices;

namespace MarkUnit.Classes
{
    internal class ClassPredicate : IClassPredicate
    {
        private readonly IClassCollectionFactory _classCollectionFactory;
        private readonly IClassCollector _classCollector;
        private readonly bool _negate;
        private readonly bool _not;

        public ClassPredicate(IClassCollector classCollector, bool negate, bool not)
        : this(classCollector,negate,not,new ClassCollectionFactory())
        {
        }

        public ClassPredicate(IClassCollector classCollector, bool negate, bool not,IClassCollectionFactory classCollectionFactory)
        {
            _classCollector = classCollector;
            _negate = negate;
            _not = not;
            _classCollectionFactory = classCollectionFactory;
        }

        public IClassCollection That()
        {
            return ThatOrWhich("that");
        }

        private IClassCollection ThatOrWhich(string word)
        {
            PredicateString.Add(word);
            return _classCollectionFactory.Create(_classCollector, _negate, _not);
        }

        public IClassCollection Which()
        {
            return ThatOrWhich("which");
        }
    }
}
