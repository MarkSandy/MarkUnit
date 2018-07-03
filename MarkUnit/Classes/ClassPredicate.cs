﻿using System.Runtime.CompilerServices;

namespace MarkUnit.Classes
{
    internal class ClassPredicate : IClassPredicate
    {
        private readonly IClassCollectionFactory _classCollectionFactory;
        private readonly IClassCollector _classCollector;
        private readonly bool _negate;
        private readonly bool _not;
        private string[] _exceptions=new string[0];

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

        public IPredicate<IClassCollection> Except(params string[] exceptionPatterns)
        {
            _exceptions = exceptionPatterns;
            return this;
        }

        public IClassCollection That()
        {
            return ThatOrWhich("that");
        }

        private IClassCollection ThatOrWhich(string word)
        {
            var result= _classCollectionFactory.Create(Instances.ClassRuleFactory, _classCollector, _negate, _not, _exceptions);
            PredicateString.Add(word);
            return result;
        }

        public IClassCollection Which()
        {
            return ThatOrWhich("which");
        }
    }
}
