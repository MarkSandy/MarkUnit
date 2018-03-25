using MarkUnit.Assemblies;
using MarkUnit.Classes;
using Ninject.Modules;

namespace MarkUnit
{
    public class NinjectSetup : NinjectModule
    {
        public override void Load()
        {
            Bind<IAssemblyReader>().To<AssemblyReader>().InSingletonScope();
            Bind<AssemblyCollector>().ToSelf();
            Bind<DirectoryAssemblyCollector>().ToSelf();
            Bind<IClassInfoCollector>().To<ClassInfoCollector>();

            Bind<IClassReader>().To<ClassReader>().InSingletonScope();
            Bind<IClassCollector>().To<ClassCollector>();
        }
    }
}
