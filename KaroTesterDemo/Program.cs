using System;
using System.Collections.Generic;
using System.Diagnostics;
using MarkUnit;
using MarkUnit.Classes;

namespace KaroTesterDemo
{
    internal class Program
    {
        private static List<string> _longMethods = new List<string>();

        static bool CheckAndCollect(IMethod method)
        {
            if (!method.IsPublic || method.MethodName.Matches("*Row")) return true;
            if (method.Parameters.Length > 9) _longMethods.Add(method.MethodName);
            return method.Parameters.Length < 10;
        }

        private static void Main(string[] args)
        {
            string path = @"C:\Projects\Repos\KtDb\Build\Debug\";
            
            var kaRo = Solution.Create().FromPath(path).Matching("DCX.KT.*").WithoutException();

            Measure(() =>
                kaRo.EachClass().Except("Archi*")
                    .That()
                    .Is(c=>c.IsPublic)
                    .And()
                    .IsDeclaredInAssemblyMatching("DCX.KT.UnitTests")
                    .And()
                    .HasNameMatching("*Tests")
                    .Should()
                    .UsesClassMatching("(\\w+)Test", "$1")
                    .Check()
            );

         
            Measure(() =>
            kaRo.EachClass().That()
                .Not().IsDeclaredInAssemblyMatching("*BusinessComponents*")
                .And().Not().IsDeclaredInAssemblyMatching("*UserInterface*")
                .Should().HaveMethods(m => CheckAndCollect(m)).Check()
            );

            kaRo.NoClass().Should().HaveCyclicDependencies();
            Measure(() =>

                kaRo.EachClass()
                    .That()
                    .ImplementsInterfaceMatching("IBusinessFacade")
                    .And()
                    .Not()
                    .IsDeclaredInAssemblyMatching("*Tests")
                    .Should()
                    .BeInAssemblyMatching("DCX.KT.BusinessFacadeImpl")
                    .And()
                    .ImplementInterface()
                    .That()
                    .HasMatchingClassName()
                    .Check()
            );


            Measure(() =>

                kaRo.EachClass()
                    .That()
                    .ImplementsInterfaceMatching("ITechnicalFacade")
                    .And()
                    .Not()
                    .HasNameMatching("Mock*")
                    .Should()
                    .BeInAssemblyMatching("DCX.KT.BusinessFacadeImpl")
                    .And()
                    .ImplementInterfaceMatching("IService")
                    .And()
                    .ImplementInterface()
                    .That()
                    .HasMatchingClassName()
                    .Check());

            Measure(() =>

                kaRo.EachClass().Except("Mock*")
                    .That()
                    .HasNameMatching("*DataController")
                    .And()
                    .Not()
                    .IsDeclaredInAssemblyMatching("DCX.KT.DataAccess*")
                    .Should()
                    .BeInAssemblyMatching("DCX.KT.DataControl*")
                    .And()
                    .ImplementInterfaceMatching("IDataController")
                    .And()
                    .ImplementInterface()
                    .That()
                    .HasMatchingClassName()
                    .Check()
            );

            Measure(() =>
                kaRo.EachClass()
                    .That()
                    .ImplementsInterfaceMatching("IRepository")
                    .Should()
                    .HaveNameMatching("*Repository")
                    .And()
                    .BeInAssemblyMatching("DCX.KT.BusinessFacadeImpl")
                    .Check()
            );

            Measure(() =>
                kaRo.EachClass()
                    .That()
                    .Is(t => t.IsPublic)
                    .And()
                    .HasNameMatching("*Repository")
                    .Should()
                    .BeInAssembly(a => a.FullName.Matches("DCX.KT.BusinessFacadeImpl*") || a.FullName.Matches("DCX.KT.TechnicalFacadeImpl*"))
                    .And()
                    .ImplementInterfaceMatching("IRepository")
                    .And()
                    .ImplementInterface()
                    .That()
                    .HasMatchingClassName()
                    .Check()
                );

            Measure(() =>

                kaRo.OnlyAnAssembly().Except("DCX.KT.UnitTests*")
                    .That()
                    .HasNameMatching("*WpfMain")
                    .Should()
                    .ReferenceAssembliesMatching("DevExpress*")
                    .Check()
                );

            Console.WriteLine("Tests passed");
            Console.ReadLine();

        }

        private static void Measure(Action action)
        {
            var sb = new Stopwatch();
            sb.Start();
            action();
            sb.Stop();
            Console.WriteLine("T="+sb.Elapsed.ToString());

        }
    }
}
