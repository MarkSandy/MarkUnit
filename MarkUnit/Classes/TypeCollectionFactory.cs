namespace MarkUnit.Classes
{
    internal class TypeCollectionFactory : ITypeCollectionFactory
    {
        public ITypeCollection Create(ITypeCollector typeCollector, bool negate, bool not, string[] exceptions)
        {
            var types = typeCollector.Get();
            var typeFilter = new FilteredTypes(types);
            var result = new TypeCondition(new ClassCollectionFactory(),  
                new InterfaceCollectionFactory(), typeFilter, negate);
            return result.AddIgnoreList(exceptions, not);
        }
    }
}