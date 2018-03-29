# MarkUnit
Architectual Unittesting

Add architectual constraints to your solution. Enforce strict layers by checking assembly and class dependencies.

Example:


    // We load a solution by specifying the path and a name filter. We could also specify any assembly. 
    var solution = Solution.Create().FromPath("c:\myProjects").Matching("XY.*.dll"); 
    
    // We now declare our architectual rules and check them
    solution.EachClass().That().ImplementsInterface<IBackEndService>().Should().BeInAssemblyMatching("XY.Services").Check();
    solution.NoAssembly().That().Not().HasNameMatching("XY.DataController").Should().ReferenceAssembliesMatching("System.Data").Check();
    ...
    
    
 The project is in it's alpha state and requires tests, review and code improvements.
    

