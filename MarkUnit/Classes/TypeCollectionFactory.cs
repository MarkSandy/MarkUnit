namespace MarkUnit.Classes
{
    internal class TypeCollectionFactory : ITypeCollectionFactory
    {
        public ITypeCollection Create(ITypeCollector typeCollector, bool negate, bool not)
        {
            var types = typeCollector.Get();
            var typeFilter = new FilteredTypes(types);
            var result = new TypeCondition(new ClassCollectionFactory(),  
                new InterfaceCollectionFactory(), typeFilter, negate);
            return not ? result.SilentNot() : result;
        }
    }
}