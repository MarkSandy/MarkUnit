namespace MarkUnit.Classes
{
    internal interface IClassCollectionFactory
    {
        IClassCollection Create(IClassCollector classCollector, bool negate, bool not);
    }

    internal class ClassCollectionFactory : IClassCollectionFactory
    {
        public IClassCollection Create(IClassCollector classCollector, bool negate, bool not)
        {
            var classes = classCollector.Get();
            var classFilter = new FilteredClasses(classes);
            var result = new ClassCondition(classFilter, negate);
            return not ? result.Not() : result;
        }
    }
}